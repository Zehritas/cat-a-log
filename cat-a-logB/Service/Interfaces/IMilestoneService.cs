using cat_a_logB.Data;

namespace cat_a_logB.Service.Interfaces
{
    public interface IMilestoneService
    {
        public bool AddMilestone(ProjectMilestone milestone);

        public bool RemoveMilestone(int id);

        public bool AddMilestones(List<ProjectMilestone> milestones);

        public bool RemoveMilestones(List<ProjectMilestone> milestones);

        public ProjectMilestone GetMilestone(int Id);

        public List<ProjectMilestone> GetMilestones();

        public void ChangeMilestoneColor(int id, string newColor);

        public bool UpdateMilestone(ProjectMilestone milestone);

        public bool MilestoneExists(int id);
    }
}
