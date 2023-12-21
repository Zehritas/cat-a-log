using AutoMapper;
using CatAPI.Data;
using CatAPI.Dto;
using CatAPI.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskDataController : Controller
    {
        private readonly ITaskDataService _taskDataService;
        private readonly IMapper _mapper;

        public TaskDataController(ITaskDataService taskDataService, IMapper mapper)
        {
            _taskDataService = taskDataService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetTasks()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            List<TaskDto> tasks = _mapper.Map<List<TaskDto>>(_taskDataService.GetTasks());

            return Ok(tasks);
        }

        [HttpGet("{Id}")]
        public IActionResult GetTask(int Id)
        {
            if (!_taskDataService.TaskExists(Id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            TaskDto task = _mapper.Map<TaskDto>(_taskDataService.GetTask(Id));

            return Ok(task);
        }

        [HttpPut("{Id}")]
        public IActionResult UpdateTask(int Id, [FromBody] TaskDto taskToUpdate)
        {
            if (!_taskDataService.TaskExists(Id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TaskData taskMap = _mapper.Map<TaskData>(taskToUpdate);
            _taskDataService.UpdateTask(taskMap);

            return NoContent();
        }

        [HttpPost]
        public IActionResult CreateTask([FromBody] TaskDto taskToCreate)
        {
            if (taskToCreate == null)
            {
                return BadRequest(ModelState);
            }

            TaskData task = _taskDataService.GetTasks()
               .Where(t => t.Name == taskToCreate.Name).FirstOrDefault();

            if (task != null)
            {
                ModelState.AddModelError("", "Task already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TaskData taskMap = _mapper.Map<TaskData>(taskToCreate);

            if (!_taskDataService.AddTask(taskMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            if (!_taskDataService.TaskExists(id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_taskDataService.RemoveTask(id))
            {
                ModelState.AddModelError("", "something went wrong while removing task");
            }

            return NoContent();
        }
    }
}
