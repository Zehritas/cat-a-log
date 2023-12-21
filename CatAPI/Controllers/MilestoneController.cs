using AutoMapper;
using CatAPI.Data;
using CatAPI.Dto;
using CatAPI.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MilestoneController : Controller
    {
        private readonly IMilestoneService _milestoneService;
        private readonly IMapper _mapper;

        public MilestoneController(IMilestoneService milestoneService, IMapper mapper)
        {
            _milestoneService = milestoneService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetMilestones()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<MilestoneDto> milestones = _mapper.Map<List<MilestoneDto>>(_milestoneService.GetMilestones());

            return Ok(milestones);
        }

        [HttpGet("{Id}")]
        public IActionResult GetMilestone(int Id)
        {
            if (!_milestoneService.MilestoneExists(Id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MilestoneDto milestone = _mapper.Map<MilestoneDto>(_milestoneService.GetMilestone(Id));

            return Ok(milestone);
        }

        [HttpPut("{Id}")]
        public IActionResult UpdateMilestone(int Id, [FromBody] MilestoneDto milestoneToUpdate)
        {
            if (!_milestoneService.MilestoneExists(Id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ProjectMilestone milestoneMap = _mapper.Map<ProjectMilestone>(milestoneToUpdate);
            _milestoneService.UpdateMilestone(milestoneMap);

            return NoContent();
        }

        [HttpPost]
        public IActionResult CreateMilestone([FromBody] MilestoneDto milestoneToCreate)
        {
            if (milestoneToCreate == null)
            {
                return BadRequest(ModelState);
            }

            ProjectMilestone milestone = _milestoneService.GetMilestones()
                .Where(m => m.Name == milestoneToCreate.Name).FirstOrDefault();

            if (milestone != null)
            {
                ModelState.AddModelError("", "Milestone already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ProjectMilestone milestoneMap = _mapper.Map<ProjectMilestone>(milestoneToCreate);

            if (!_milestoneService.AddMilestone(milestoneMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpDelete("{Id}")]
        public IActionResult DeleteMilestone(int id)
        {
            if (!_milestoneService.MilestoneExists(id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_milestoneService.RemoveMilestone(id))
            {
                ModelState.AddModelError("", "something went wrong while removing milestone");
            }

            return NoContent();
        }
    }
}
