
namespace CatAPI.Dto
{
    public class TaskDto
    {
        public int Id { get; set; }

        public int TeamId { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string? PointColor { get; set; }

        public string? Comments { get; set; }

        public int? MilestoneId { get; set; }

        public int Progress { get; set; }

        public double DayProgress { get; set; }

        public double AutoProgress { get; set; }
    }
}
