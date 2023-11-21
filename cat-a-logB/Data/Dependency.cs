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
        public int SuccessorTaskId { get; set; }

        public int DependeeTaskId { get; set; }

        [ForeignKey("SuccessorTaskId")]
        public TaskData? SuccessorTask { get; set; }

        [ForeignKey("DependeeTaskId")]
        public TaskData? DependeeTask { get; set; }

        public string SuccessorTaskName { get; set; } // change to id, create task IDs
        public DependencyType Type { get; set; }
    }
}
