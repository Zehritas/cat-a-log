using AutoMapper;
using CatAPI.Data;
using CatAPI.Dto;
using CatAPI.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : Controller
    {
        private readonly IProjectTeamService _teamService;
        private readonly IMapper _mapper;


        public TeamController(IProjectTeamService teamService, IMapper mapper)
        {
            _teamService = teamService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetTeams() 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            List<TeamDto> teams = _mapper.Map<List<TeamDto>>(_teamService.GetTeams());
            return Ok(teams);
        }

        [HttpGet("{Id}")]
        public IActionResult GetTeam(int Id)
        {
            if (!_teamService.TeamExists(Id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TeamDto team = _mapper.Map<TeamDto>(_teamService.GetTeam(Id));

            return Ok(team);
        }

        [HttpPut("{Id}")]
        public IActionResult UpdateTeam(int Id, [FromBody] TeamDto teamToUpdate)
        {
            if (!_teamService.TeamExists(Id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ProjectTeam teamMap = _mapper.Map<ProjectTeam>(teamToUpdate);
            _teamService.UpdateTeam(teamMap);

            return NoContent();
        }

        [HttpPost]
        public IActionResult CreateTeam([FromBody] TeamDto teamToCreate)
        {
            if (teamToCreate == null)
            {
                return BadRequest(ModelState);
            }

            ProjectTeam? team = _teamService.GetTeams()
               .Where(t => t.Name == teamToCreate.Name).FirstOrDefault();

            if (team != null)
            {
                ModelState.AddModelError("", "Team already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ProjectTeam teamMap = _mapper.Map<ProjectTeam>(teamToCreate);

            if (!_teamService.AddTeam(teamMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTeam(int id)
        {
            if (!_teamService.TeamExists(id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_teamService.RemoveTeam(id))
            {
                ModelState.AddModelError("", "something went wrong while removing team");
            }

            return NoContent();
        }
    }
}
