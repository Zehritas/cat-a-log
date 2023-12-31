﻿using CatAPI.Data;

namespace CatAPI.Service.Interfaces
{
    public interface IMilestoneService
    {
        public bool AddMilestone(ProjectMilestone milestone);

        public bool RemoveMilestone(int id);

        public bool AddMilestones(IEnumerable<ProjectMilestone> milestones);

        public bool RemoveMilestones(IEnumerable<ProjectMilestone> milestones);

        public ProjectMilestone GetMilestone(int Id);

        public IEnumerable<ProjectMilestone> GetMilestones();

        public void ChangeMilestoneColor(int id, string newColor);

        public bool UpdateMilestone(ProjectMilestone milestone);

        public bool MilestoneExists(int id);
    }
}
