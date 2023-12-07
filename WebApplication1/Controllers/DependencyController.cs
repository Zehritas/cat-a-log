using Cat_a_logAPI.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Cat_a_logAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DependencyController : Controller
    {
        private readonly IDependencyService _dependencyService;

        public DependencyController(IDependencyService dependencyService)
        {
            _dependencyService = dependencyService;
        }

        [HttpGet]
        public IActionResult GetDependencies() 
        {
            var dependencies = _dependencyService.GetDependencies();

            return Ok(dependencies);
        }
    }
}
