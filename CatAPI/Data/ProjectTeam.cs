using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CatAPI.Data
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
    }
}
