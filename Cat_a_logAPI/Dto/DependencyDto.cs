namespace Cat_a_logAPI.Dto
{
    public class DependencyDto
    {
        public int Id { get; set; }

        public int SuccessorTaskId { get; set; }

        public int? PredecessorTaskId { get; set; }

        public DependencyType Type { get; set; }
    }
}
