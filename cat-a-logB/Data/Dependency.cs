//using cat_a_logB.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace cat_a_logB.Data
{
    public class Dependency
    {
        [Key, Column(Order = 1)]
        public int DependentTaskId { get; set; }

        [Key, Column(Order = 2)]
        public int DependeeTaskId { get; set; }

        [ForeignKey("DependentTaskId")]
        public GanttData DependentTask { get; set; }

        [ForeignKey("DependeeTaskId")]
        public GanttData DependeeTask { get; set; }

        public string DependentTaskName { get; set; } // change to id, create task IDs
        public DependencyType Type { get; set; }
    }
}
