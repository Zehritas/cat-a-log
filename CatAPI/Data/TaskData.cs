using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatAPI.Data
{
    public class TaskData
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TeamId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string? PointColor { get; set; }

        public string? Comments { get; set; }

        public int? MilestoneId { get; set; }

        [Required]
        public int Progress { get; set; }

        public double DayProgress { get; set; }

        [Required]
        public double AutoProgress { get; set; }

        [ForeignKey("TeamId")]
        public ProjectTeam Team { get; set; }

        [ForeignKey("MilestoneId")]
        public ProjectMilestone? Milestone { get; set; }

        public List<Dependency> Dependencies { get; set; } = new List<Dependency>();
       
    }
}
