using AutoMapper;
using CatAPI.Data;
using CatAPI.Dto;
using CatAPI.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public ProjectController(IProjectService projectService, IMapper mapper)
        {
            _projectService = projectService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetProjects()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<ProjectDto> projects = _mapper.Map<List<ProjectDto>>(_projectService.GetProjects());

            return Ok(projects);
        }

        [HttpGet("{Id}")]
        public IActionResult GetProject(int Id)
        {
            if (!_projectService.ProjectExists(Id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ProjectDto project = _mapper.Map<ProjectDto>(_projectService.GetProject(Id));

            return Ok(project);
        }

        [HttpPut("{Id}")]
        public IActionResult UpdateProject(int Id, [FromBody] ProjectDto projectToUpdate)
        {
            if (!_projectService.ProjectExists(Id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Project projectMap = _mapper.Map<Project>(projectToUpdate);
            _projectService.UpdateProject(projectMap);

            return NoContent();
        }

        [HttpPost]
        public IActionResult CreateProject([FromBody] ProjectDto projectToCreate)
        {
            if (projectToCreate == null)
            {
                return BadRequest(ModelState);
            }

            Project project = _projectService.GetProjects()
               .Where(p => p.Name == projectToCreate.Name).FirstOrDefault();

            if (project != null)
            {
                ModelState.AddModelError("", "Project already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Project projectMap = _mapper.Map<Project>(projectToCreate);

            if (!_projectService.AddProject(projectMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMember(int id)
        {
            if (!_projectService.ProjectExists(id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_projectService.RemoveProject(id))
            {
                ModelState.AddModelError("", "something went wrong while removing project");
            }

            return NoContent();
        }
    }
}
