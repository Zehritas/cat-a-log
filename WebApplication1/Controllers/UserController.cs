using Cat_a_logAPI.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cat_a_logAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
    }
}
