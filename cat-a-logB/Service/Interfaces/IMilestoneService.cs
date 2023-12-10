﻿using cat_a_logB.Data;

namespace cat_a_logB.Service.Interfaces
{
    public interface IMilestoneService
    {
        public void AddMilestone(ProjectMilestone milestone);

        public void RemoveMilestone(ProjectMilestone milestone);

        public void AddMilestones(List<ProjectMilestone> milestones);

        public void RemoveMilestones(List<ProjectMilestone> milestones);

        public List<ProjectMilestone> GetAllMilestones();

        public void ChangeMilestoneColor(int id, string newColor);
    }
}