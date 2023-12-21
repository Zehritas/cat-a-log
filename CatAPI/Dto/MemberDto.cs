namespace CatAPI.Dto
{
    public class MemberDto
    {
        public int UserId { get; set; }

        public int TeamId { get; set; }

        public string? Name { get; set; }

        public string? Position { get; set; }

        public float? Efficiency { get; set; }
    }
}
