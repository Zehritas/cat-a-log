using Cat_a_logAPI.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cat_a_logAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskDataController : Controller
    {
        private readonly ITaskDataService _taskDataService;

        public TaskDataController(ITaskDataService taskDataService)
        {
            _taskDataService = taskDataService;
        }

        [HttpGet]
        public IActionResult GetTasks()
        {
            var tasks = _taskDataService.GetAllTasks();

            return Ok(tasks);
        }
    }
}
