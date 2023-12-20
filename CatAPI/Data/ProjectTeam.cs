using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Cat_a_logAPI.Data
{

    public class ProjectTeam
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        public string? Color { get; set; }

        [Required]
        public string? Name { get; set; }

        [ForeignKey("ProjectId")]
        public Project? Project { get; set; }

        public List<TaskData> Tasks { get; set; } = new List<TaskData>();

        public List<Member> TeamMembers { get; set; } = new List<Member>();

        [NotMapped]
        private List<String> members;

        [NotMapped]
        public List<String> Members
        {
            get { return members; }
            set
            {
                if (value != null && value.Distinct().Count() == value.Count)
                {
                    members = value;
                }
                else
                {
                    throw new ArgumentException("Members list must be non-null and contain unique names.");
                }
            }
        }

        public ProjectTeam(string color, string name, List<String> members)
        {
            Color = color;
            Name = name;
            Tasks = new List<TaskData>();
            Members = members;
        }

        public ProjectTeam(string color, string name)
        {
            Color = color;
            Name = name;
            Tasks = new List<TaskData>();
        }
        public ProjectTeam()
        {
        }
        public void LoadTeamTasks(List<TaskData> allTasks)
        {
            Tasks = allTasks.Where(task => task.Team.Name == Name).ToList();
        }
        public static void GetTasksForTeam(List<TaskData> allTasks, ProjectTeam team)
        {
            team.Tasks = allTasks.Where(task => task.Team.Name == team.Name).ToList();
        }
    }
}
