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
            var members = _mapper.Map<List<MemberDto>>(_memberService.GetMembers());

            return Ok(members);
        }

        [HttpGet("{Id}")]
        public IActionResult GetMember(int Id)
        {
            var member = _mapper.Map<MemberDto>(_memberService.GetMember(Id));

            return Ok(member);
        }

        [HttpPut("{Id}")]
        public IActionResult UpdateMember(int Id, [FromBody] MemberDto memberToUpdate)
        {
            var memberMap = _mapper.Map<Member>(memberToUpdate);
            _memberService.UpdateMember(memberMap);

            return NoContent();
        }
    }
}
