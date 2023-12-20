using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cat_a_logB.Data
{
    public class TaskData : IComparable<TaskData>
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
        public int Progress { get; set; } // Add a Progress property

        public double DayProgress { get; set; }

        [Required]
        public double AutoProgress { get; set; }

        [ForeignKey("TeamId")]
        public ProjectTeam Team { get; set; }

        [ForeignKey("MilestoneId")]
        public ProjectMilestone? Milestone { get; set; }

        public List<Dependency> Dependencies { get; set; } = new List<Dependency>();
        public int CompareTo(TaskData other)
        {
            return this.Progress.CompareTo(other.Progress);
        }

        public TaskData()
        {
            StartDate = DateTime.Now.Date;
            EndDate = DateTime.Now.Date;
            PointColor = "#000000";
            Progress = 0;
            Comments = "";
        }
        public TaskData(string name, DateTime startDate, DateTime endDate, ProjectTeam team, int progress, string comments)
        {
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            Team = team;
            Progress = progress;
            PointColor = team.Color; // Assuming 'color' is a property of the ProjectTeam class
            Comments = comments;
        }


    }
}
