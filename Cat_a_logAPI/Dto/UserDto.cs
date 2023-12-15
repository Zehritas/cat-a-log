namespace Cat_a_logAPI.Dto
{
    public class UserDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public MemberDto? Member { get; set; }
    }
}
