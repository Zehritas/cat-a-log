using System.ComponentModel.DataAnnotations;

namespace Cat_a_logAPI.Data
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public Member? Member { get; set; }
    }
}
