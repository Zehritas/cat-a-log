namespace Cat_a_logAPI.Dto
{
    public class ProjectMilestoneDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string Color { get; set; }

        public DateTime TargetDate { get; set; } = DateTime.Now;
    }
}
