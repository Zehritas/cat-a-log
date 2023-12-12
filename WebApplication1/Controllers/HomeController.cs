using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Cat_a_logAPI.Service.Interfaces;

namespace Cat_a_logAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Controller : Controller
    {
        private readonly IDependencyService _dependencyService;
        public HomeController(IDependencyService dependencyService)
        {
            _dependencyService = dependencyService;
        }
        
    }
}
