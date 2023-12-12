using Cat_a_logAPI.Data;
using Cat_a_logAPI.Service.Interfaces;

namespace Cat_a_logAPI.Service.Implementation
{
    public class MilestoneService : IMilestoneService
    {
        private readonly Cat_a_logBContext _dbContext;

        public MilestoneService(Cat_a_logBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool AddMilestone(ProjectMilestone milestone)
        {
            _dbContext.ProjectMilestone.Add(milestone);
            return Save();
        }

        public bool RemoveMilestone(ProjectMilestone milestone)
        {
            _dbContext.ProjectMilestone.Remove(milestone);
            return Save();
        }

        public bool AddMilestones(IEnumerable<ProjectMilestone> projectMilestones)
        {
            foreach (ProjectMilestone projectMilestone in projectMilestones)
            {
                _dbContext.ProjectMilestone.Add(projectMilestone);
            }
            return Save();
        }

        public bool RemoveMilestones(IEnumerable<ProjectMilestone> projectMilestones)
        {
            foreach (ProjectMilestone projectMilestone in projectMilestones)
            {
                RemoveMilestone(projectMilestone);
            }
            return Save();
        }

        public ProjectMilestone GetMilestone(int Id)
        {
            return _dbContext.ProjectMilestone.Find(Id);
        }

        public IEnumerable<ProjectMilestone> GetMilestones()
        {
            return _dbContext.ProjectMilestone.ToList();
        }

        public void ChangeMilestoneColor(int id, string color)
        {
            ProjectMilestone milestone = _dbContext.ProjectMilestone.Find(id);
            milestone.Color = color;
            _dbContext.SaveChanges();
        }

        public bool Save()
        {
            var saved = _dbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateMilestone(ProjectMilestone milestone)
        {
            _dbContext.ProjectMilestone.Update(milestone);

            return Save();
        }

        public bool MilestoneExists(int id)
        {
            return _dbContext.ProjectMilestone.Any(m => m.Id == id);
        }
    }
}
