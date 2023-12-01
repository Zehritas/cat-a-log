//using cat_a_logB.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace cat_a_logB.Data
{
    public class Dependency
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SuccessorTaskId { get; set; }

        public int? PredecessorTaskId { get; set; }

        [Required]
        public DependencyType Type { get; set; }

        [ForeignKey("SuccessorTaskId")]
        public TaskData? SuccessorTask { get; set; }

        [ForeignKey("PredecessorTaskId")]
        public TaskData? PredecessorTask { get; set; }
    }
}
