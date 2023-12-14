using AutoMapper;
using Cat_a_logAPI.Data;
using Cat_a_logAPI.Dto;
using Cat_a_logAPI.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cat_a_logAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;
        private readonly IMapper _mapper;

        public MemberController(IMemberService memberService, IMapper mapper)
        {
            _memberService = memberService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetMembers()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var members = _mapper.Map<List<MemberDto>>(_memberService.GetMembers());

            return Ok(members);
        }

        [HttpGet("{userId}/{teamId}")]
        public IActionResult GetMember(int userId, int teamId)
        {
            if(!_memberService.MemberExists(userId, teamId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var member = _mapper.Map<MemberDto>(_memberService.GetMember(userId, teamId));

            return Ok(member);
        }

        [HttpPut("{userId}/{teamId}")]
        public IActionResult UpdateMember(int userId, int teamId, [FromBody] MemberDto memberToUpdate)
        {
            if(!_memberService.MemberExists(userId, teamId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var memberMap = _mapper.Map<Member>(memberToUpdate);
            _memberService.UpdateMember(memberMap);

            return NoContent();
        }

        [HttpPost]
        public IActionResult CreateMember([FromBody] MemberDto memberToCreate)
        {
            if (memberToCreate == null)
            {
                return BadRequest(ModelState);
            }

            var member = _memberService.GetMembers()
                .Where(m => m.Name == memberToCreate.Name).FirstOrDefault();

            if (member != null)
            {
                ModelState.AddModelError("", "Member already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var memberMap = _mapper.Map<Member>(memberToCreate);

            if (!_memberService.AddMember(memberMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpDelete("{userId, teamId}")]
        public IActionResult DeleteMember(int userId, int teamId)
        {
            if (!_memberService.MemberExists(userId, teamId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!_memberService.RemoveMember(userId, teamId))
            {
                ModelState.AddModelError("", "something went wrong while removing member");
            }

            return NoContent();
        }
    }
}
