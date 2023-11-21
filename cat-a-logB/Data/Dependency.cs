//using cat_a_logB.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace cat_a_logB.Data
{
    //[PrimaryKey(nameof(DependentTaskId), nameof(DependeeTaskId))]
    public class Dependency
    {
        [Key]
        public int Id { get; set; }
        public int DependentTaskId { get; set; }

        public int DependeeTaskId { get; set; }

        [ForeignKey("DependentTaskId")]
        public TaskData? DependentTask { get; set; }

        [ForeignKey("DependeeTaskId")]
        public TaskData? DependeeTask { get; set; }

        public string DependentTaskName { get; set; } // change to id, create task IDs
        public DependencyType Type { get; set; }
    }
}
