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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = _mapper.Map<List<UserDto>>(_userService.GetUsers());

            return Ok(users);
        }

        [HttpGet("{Id}")]
        public IActionResult GetUser(int Id)
        {
            if (!_userService.UserExists(Id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _mapper.Map<UserDto>(_userService.GetUser(Id));

            return Ok(user);
        }

        [HttpPut("{Id}")]
        public IActionResult UpdateUser(int Id, [FromBody] UserDto userToUpdate)
        {
            if (!_userService.UserExists(Id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userMap = _mapper.Map<User>(userToUpdate);
            _userService.UpdateUser(userMap);

            return NoContent();
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserDto userToCreate)
        {
            if (userToCreate == null)
            {
                return BadRequest(ModelState);
            }

            var user = _userService.GetUsers()
               .Where(u => u.Name == userToCreate.Name).FirstOrDefault();

            if (user != null)
            {
                ModelState.AddModelError("", "User already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userMap = _mapper.Map<User>(userToCreate);

            if (!_userService.AddUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            if (!_userService.UserExists(id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_userService.RemoveUser(id))
            {
                ModelState.AddModelError("", "something went wrong while removing user");
            }

            return NoContent();
        }
    }
}
