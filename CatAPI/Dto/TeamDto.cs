namespace CatAPI.Dto
{
    public class TeamDto
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public string? Color { get; set; }

        public string? Name { get; set; }
    }
}
