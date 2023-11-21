using System.ComponentModel.DataAnnotations;

namespace cat_a_logB.Data
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public Member Member { get; set; }
    }
}
