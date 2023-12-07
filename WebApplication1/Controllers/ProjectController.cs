using Cat_a_logAPI.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cat_a_logAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly IMemberService _memberService;

        public ProjectController(IMemberService memberService)
        {
            _memberService = memberService;
        }
    }
}
