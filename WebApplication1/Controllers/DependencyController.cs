using AutoMapper;
using Cat_a_logAPI.Data;
using Cat_a_logAPI.Dto;
using Cat_a_logAPI.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cat_a_logAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DependencyController : Controller
    {
        private readonly IDependencyService _dependencyService;
        private readonly IMapper _mapper;

        public DependencyController(IDependencyService dependencyService, IMapper mapper)
        {
            _dependencyService = dependencyService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetDependencies() 
        {
            var dependencies = _mapper.Map<List<DependencyDto>>(_dependencyService.GetDependencies());

            return Ok(dependencies);
        }

        [HttpGet("{Id}")]
        public IActionResult GetDependency(int Id)
        {
            var dependency = _mapper.Map<DependencyDto>(_dependencyService.GetDependency(Id));

            return Ok(dependency);
        }

        [HttpPut("{Id}")]
        public IActionResult UpdateDependency(int Id, [FromBody]DependencyDto dependencyToUpdate)
        {
            var dependencyMap = _mapper.Map<Dependency>(dependencyToUpdate);
            _dependencyService.UpdateDependency(dependencyMap);

            return NoContent();
        }

        [HttpPost]
        public IActionResult CreateDependency([FromBody]DependencyDto dependencyToCreate) 
        {
            if(dependencyToCreate == null)
            {
                return BadRequest(ModelState);
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dependencyMap = _mapper.Map<Dependency>(dependencyToCreate);

            if(!_dependencyService.AddDependency(dependencyMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpDelete("{Id}")]
        public IActionResult DeleteDependency(int Id)
        {
            if(!_dependencyService.DependencyExists(Id))
            {
                return NotFound();
            }

            var dependencyToDelete = _dependencyService.GetDependency(Id);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _dependencyService.RemoveDependency(dependencyToDelete);

            return NoContent();
        }
    }
}
