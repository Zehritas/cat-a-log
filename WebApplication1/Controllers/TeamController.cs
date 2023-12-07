using Cat_a_logAPI.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cat_a_logAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : Controller
    {
        private readonly IProjectTeamService _teamService;

        public TeamController(IProjectTeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet]
        public IActionResult GetTeams() 
        {
            var teams = _teamService.GetAllTeams();
            return Ok(teams);
        }
    }
}
