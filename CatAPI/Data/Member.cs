using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatAPI.Data
{
    public class Member
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int TeamId { get; set; }

        public string? Name { get; set; }

        public string? Position { get; set; }

        public float? Efficiency { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        [ForeignKey("TeamId")]
        public ProjectTeam? Team { get; set; }
    }
}
