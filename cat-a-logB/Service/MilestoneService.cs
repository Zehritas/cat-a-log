﻿using cat_a_logB.Data;

namespace cat_a_logB.Service
{
    public class MilestoneService : IMilestoneService
    {
        private readonly cat_a_logBContext _dbContext;

        public MilestoneService(cat_a_logBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddMilestone(ProjectMilestone milestone)
        {
            _dbContext.ProjectMilestone.Add(milestone);
            _dbContext.SaveChanges();
        }

        public void RemoveMilestone(ProjectMilestone milestone)
        {
            _dbContext.ProjectMilestone.Remove(milestone);
            _dbContext.SaveChanges();
        }

        public void AddMilestones(List<ProjectMilestone> projectMilestones)
        {
            foreach(ProjectMilestone projectMilestone in projectMilestones)
            {
                _dbContext.ProjectMilestone.Add(projectMilestone);
            }
            _dbContext.SaveChanges();
        }

        public void RemoveMilestones(List<ProjectMilestone> projectMilestones)
        {
            foreach (ProjectMilestone projectMilestone in projectMilestones)
            {
                _dbContext.ProjectMilestone.Remove(projectMilestone);
            }
            _dbContext.SaveChanges();
        }
    }
}
