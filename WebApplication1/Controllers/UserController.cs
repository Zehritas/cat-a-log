using AutoMapper;
using Cat_a_logAPI.Data;
using Cat_a_logAPI.Dto;
using Cat_a_logAPI.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cat_a_logAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _mapper.Map<List<UserDto>>(_userService.GetUsers());

            return Ok(users);
        }

        [HttpGet("{Id}")]
        public IActionResult GetUser(int Id)
        {
            var user = _mapper.Map<UserDto>(_userService.GetUser(Id));

            return Ok(user);
        }

        [HttpPut("{Id}")]
        public IActionResult UpdateUser(int Id, [FromBody] UserDto userToUpdate)
        {
            var userMap = _mapper.Map<User>(userToUpdate);
            _userService.UpdateUser(userMap);

            return NoContent();
        }
    }
}
