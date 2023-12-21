namespace CatAPI.Dto
{
    public class MilestoneDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string Color { get; set; }

        public DateTime TargetDate { get; set; } = DateTime.Now;
    }
}
