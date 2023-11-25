using cat_a_logB.Data;

namespace cat_a_logB.Service
{
    public interface IMilestoneService
    {
        public void AddMilestone(ProjectMilestone milestone);

        public void RemoveMilestone(ProjectMilestone milestone);

        public void AddMilestones(List<ProjectMilestone> milestones);

        public void RemoveMilestones(List<ProjectMilestone> milestones);
    }
}
