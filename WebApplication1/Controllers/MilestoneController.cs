using Cat_a_logAPI.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cat_a_logAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MilestoneController : Controller
    {
        private readonly IMilestoneService _milestoneService;

        public MilestoneController(IMilestoneService milestoneService)
        {
            _milestoneService = milestoneService;
        }

        [HttpGet]
        public IActionResult GetMilestones()
        {
            var milestones = _milestoneService.GetAllMilestones();

            return Ok(milestones);
        }
    }
}
