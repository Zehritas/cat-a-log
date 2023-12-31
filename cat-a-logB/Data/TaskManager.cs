﻿using ApexCharts;
using cat_a_logB.Service.Implementation;
using cat_a_logB.Service.Interfaces;
using Microsoft.AspNetCore.Components;
using static cat_a_logB.Data.ProjectMilestone;
using static System.Runtime.InteropServices.JavaScript.JSType;



namespace cat_a_logB.Data
{
    public class TaskManager
    {
        private readonly IDependencyService dependencyService;
        private readonly ITaskDataService taskDataService;
        private readonly IProjectTeamService projectTeamService;
        private readonly IMilestoneService milestoneService;

        public TaskManager(IDependencyService _dependencyService, ITaskDataService _taskDataService, IProjectTeamService _projectTeamService, IMilestoneService _milestoneService)
        {
            dependencyService = _dependencyService;
            taskDataService = _taskDataService;
            projectTeamService = _projectTeamService;
            milestoneService = _milestoneService;
        }

        public TaskManager() { }

        public IEnumerable<TaskData> project;
        public ApexChart<TaskData> chart;
        public SelectedData<TaskData> selectedData;
        public EventCallback OnClose;
        public ApexChart<ProjectMilestone> mileChart;
        public List<ProjectMilestone> milestones;
        public List<ProjectTeam> teams;
        private TaskData newTask;

        private readonly CalculationData CalculationDataTask = new CalculationData();

        public string errorMessage { get; private set; }
        public string newTaskName { get; private set; }

        public string selectedTeamName { get; private set; }
        public DateTime newTaskStartTime { get; private set; }
        public DateTime newTaskEndTime { get; private set; }

        public string selectedSuccessorTaskName { get; private set; }

        public DependencyType selectedDependencyType { get; private set; }

        public int selectedSuccessorTask { get; private set; }

        public async Task EditComments(List<TaskData> project, ApexChart<TaskData> chart, SelectedData<TaskData> selectedData, EventCallback OnClose, string editedComments)
        {
            if (selectedData != null && selectedData.DataPoint != null
             && selectedData.DataPoint.Items.First().Id is int selectedTaskId)
            {
                TaskData taskToUpdate = project.FirstOrDefault(task => task.Id == selectedTaskId);
                if (taskToUpdate != null)
                {
                    taskToUpdate.Comments = editedComments;
                    taskDataService.ChangeTaskComment(taskToUpdate.Id, editedComments);
                    
                }


                OnClose.InvokeAsync();
                await chart.UpdateSeriesAsync();
            }
            else
            {
                Console.WriteLine("Aaa");
            }
        }

        public async Task EditTaskProgress(ApexChart<ProjectMilestone> mileChart, List<TaskData> project, ApexChart<TaskData> chart, SelectedData<TaskData> selectedData, EventCallback OnClose, List<ProjectMilestone> milestones, int progressValue)
        {
            if (selectedData != null && selectedData.DataPoint != null &&
                selectedData.DataPoint.Items.First().Id is int selectedTaskId)
            {
                TaskData taskToUpdate = project.FirstOrDefault(task => task.Id == selectedTaskId);
                if (taskToUpdate != null)
                {
                    taskToUpdate.Progress = progressValue;

                    if (progressValue == 100)
                    {
                        taskToUpdate.PointColor = "#CCCCCC";
                        foreach (ProjectMilestone milestone in milestones)
                        {
                            TaskCompletionStatus completionStatus = milestone.GetTaskCompletionStatus();
                            if (completionStatus == TaskCompletionStatus.Completed)
                            {
                                milestone.Color = "green";
                                milestoneService.ChangeMilestoneColor(milestone.Id, "green");
                                await mileChart.RenderAsync();
                            }
                        }
                    }
                    else
                    {
                        taskDataService.SyncColorWithTeam(taskToUpdate);
                        //taskToUpdate.PointColor =  taskToUpdate.Team.Color;
                    }
                }
            }
            await chart.UpdateSeriesAsync();
            OnClose.InvokeAsync();
        }


        public async Task EditTaskTime(List<TaskData> project, ApexChart<TaskData> chart, SelectedData<TaskData> selectedData, EventCallback OnClose, DateTime newStartDate, DateTime newEndDate)
        {
            if (newStartDate >= newEndDate)
            {
                errorMessage = "Invalid. Start date must not be higher than or equal to end date.";
                return;
            }

            errorMessage = "";
            if (selectedData != null && selectedData.DataPoint != null &&
                    selectedData.DataPoint.Items.First().Id is int selectedTaskId)
            {


                TaskData taskToUpdate = project.FirstOrDefault(task => task.Id == selectedTaskId);
                if (taskToUpdate != null)
                {
                    taskToUpdate.StartDate = newStartDate;
                    taskToUpdate.EndDate = newEndDate;
                    taskDataService.ChangeTaskStartDate(taskToUpdate.Id, newStartDate);
                    taskDataService.ChangeTaskEndDate(taskToUpdate.Id, newEndDate);
                    await Reschedule(taskToUpdate.Id, project, chart);
                }
            }
            else
            {
                Console.WriteLine("SelectedData is null");
            }
            await chart.UpdateSeriesAsync();
            OnClose.InvokeAsync();

        }

        public async Task EditTaskName(List<TaskData> project, ApexChart<TaskData> chart, SelectedData<TaskData> selectedData, EventCallback OnClose, string newTaskName) // Strictly to edit the name and refresh
        {
            if (string.IsNullOrWhiteSpace(newTaskName))
            {
                errorMessage = "Name cannot be empty.";
                return;
            }

            if (project.Any(task => task.Name == newTaskName))
            {
                errorMessage = "Task name is already in use.";
                return;
            }

            if (!ValidationExtensions.IsValidTaskName(newTaskName))
            {
                errorMessage = "Invalid task name. Only alphanumeric characters and spaces are allowed.";
                return;
            }

            errorMessage = "";
            if (selectedData != null && selectedData.DataPoint != null &&
            selectedData.DataPoint.Items.First().Id is int selectedTaskId)
            {
                TaskData taskToUpdate = project.FirstOrDefault(task => task.Id == selectedTaskId);
                if (taskToUpdate != null)
                {
                    taskToUpdate.Name = newTaskName;
                    taskDataService.ChangeTaskName(taskToUpdate.Id, newTaskName);
                    
                }
            }
            else
            {

            }
            newTaskName = "";
            await chart.UpdateSeriesAsync();
            OnClose.InvokeAsync();
        }

        public async Task Reschedule(int updatedTaskId, List<TaskData> tasks, ApexChart<TaskData> chart) // it should take taskID and it should not assume it is the predecessor
        {
            {
                TaskData updatedTask = tasks.FirstOrDefault(task => task.Id == updatedTaskId);
                foreach (Dependency dependency in updatedTask.Dependencies)
                {
                    TaskData predecessorTask = tasks.FirstOrDefault(task => task.Id == dependency.PredecessorTaskId);
                    TaskData successorTask = tasks.FirstOrDefault(task => task.Id == dependency.SuccessorTaskId);
                    if (successorTask == null)
                    {
                        // Handle the case where the successor task is not found
                        throw new InvalidOperationException($"Task not found for dependency: {dependency.SuccessorTaskId}");
                    }
                    double successorTaskLength = (successorTask.EndDate - successorTask.StartDate).TotalDays;


                    switch (dependency.Type)
                    {
                        case DependencyType.FS:
                            if (predecessorTask.EndDate > successorTask.StartDate)
                            {
                                successorTask.StartDate = predecessorTask.EndDate;
                                successorTask.EndDate = successorTask.StartDate.AddDays(successorTaskLength);

                                taskDataService.ChangeTaskStartDate(successorTask.Id, predecessorTask.EndDate);
                                taskDataService.UpdateTask(successorTask);
                                taskDataService.ChangeTaskEndDate(successorTask.Id, successorTask.StartDate.AddDays(successorTaskLength));
                                

                                taskDataService.UpdateTask(successorTask);
                                await Reschedule(successorTask.Id, tasks, chart);
                                await chart.UpdateSeriesAsync();
                                // StateHasChanged();
                            }
                            break;

                        case DependencyType.SF:
                            if (predecessorTask.EndDate < successorTask.StartDate)
                            {
                                successorTask.StartDate = predecessorTask.EndDate;
                                successorTask.EndDate = successorTask.StartDate.AddDays(successorTaskLength);

                                taskDataService.ChangeTaskStartDate(successorTask.Id, predecessorTask.EndDate);
                                taskDataService.ChangeTaskEndDate(successorTask.Id, successorTask.StartDate.AddDays(successorTaskLength));
                                
                                taskDataService.UpdateTask(successorTask);
                                await Reschedule(successorTask.Id, tasks, chart);
                                await chart.UpdateSeriesAsync();
                                // StateHasChanged();
                            }
                            break;

                        case DependencyType.SS:
                            if (predecessorTask.StartDate > successorTask.StartDate)
                            {
                                successorTask.StartDate = predecessorTask.StartDate;
                                successorTask.EndDate = successorTask.StartDate.AddDays(successorTaskLength);

                                taskDataService.ChangeTaskStartDate(successorTask.Id, predecessorTask.StartDate);
                                taskDataService.ChangeTaskEndDate(successorTask.Id, successorTask.StartDate.AddDays(successorTaskLength));
                                

                                taskDataService.UpdateTask(successorTask);
                                await Reschedule(successorTask.Id, tasks, chart);
                                await chart.UpdateSeriesAsync();
                                // StateHasChanged();
                            }
                            break;

                        case DependencyType.FF:
                            if (predecessorTask.EndDate > successorTask.EndDate)
                            {
                                successorTask.EndDate = predecessorTask.EndDate;
                                successorTask.StartDate = successorTask.EndDate.AddDays(-successorTaskLength);

                                taskDataService.ChangeTaskStartDate(successorTask.Id, successorTask.EndDate.AddDays(-successorTaskLength));
                                taskDataService.ChangeTaskEndDate(successorTask.Id, predecessorTask.EndDate);
                                

                                taskDataService.UpdateTask(successorTask);
                                await Reschedule(successorTask.Id, tasks, chart);
                                await chart.UpdateSeriesAsync();
                                // StateHasChanged();
                            }
                            break;

                        default:
                            // Handle unsupported dependency types
                            throw new ArgumentException("Unsupported DependencyType");
                    }
                }

            }
        }

       public List<T> SortTasksByDependencies<T, U>(List<T> tasks)
       where T : TaskData
       where U : Dependency
        {
            Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
            Dictionary<int, int> inDegree = new Dictionary<int, int>();

            // Build the graph and calculate in-degrees
            foreach (TaskData task in tasks)
            {
                graph[task.Id] = new List<int>();
                inDegree[task.Id] = 0;
            }

            foreach (TaskData task in tasks)
            {
                foreach (Dependency dependency in task.Dependencies)
                {
                    if (graph.ContainsKey(dependency.SuccessorTaskId) && task.Id != dependency.SuccessorTaskId) // Check if the key exists
                    {
                        graph[dependency.SuccessorTaskId].Add(task.Id);
                        inDegree[task.Id]++;
                    }
                    else
                    {
                        // Handle the case where the key doesn't exist (e.g., log or display an error)
                        Console.WriteLine($"Dependency successor task '{dependency.SuccessorTaskId}' not found.");
                    }
                }
            }


            List<T> sortedTasks = new List<T>();
            Queue<int> queue = new Queue<int>(inDegree.Where(entry => entry.Value == 0).Select(entry => entry.Key));

            while (queue.Count > 0)
            {
                int currentTaskId = queue.Dequeue();
                T currentTask = tasks.First(task => (task as TaskData).Id == currentTaskId);

                sortedTasks.Add(currentTask);

                foreach (int dependentTaskId in graph[currentTaskId])
                {
                    inDegree[dependentTaskId]--;
                    if (inDegree[dependentTaskId] == 0)
                    {
                        queue.Enqueue(dependentTaskId);
                    }
                }
            }

            return sortedTasks;
        }

        public async Task<bool> DetectCycleInternal(TaskData currentTask, TaskData startingTask, HashSet<TaskData> visited,
         HashSet<TaskData> currentlyVisiting)
        {
            if (currentlyVisiting.Contains(currentTask))
            {
                // Cycle detected
                // Console.WriteLine("cycle detected");
                return true;
            }

            if (visited.Contains(currentTask))
            {
                // Already visited, no cycle
                return false;
            }

            currentlyVisiting.Add(currentTask);

            foreach (Dependency dependency in currentTask.Dependencies)
            {
                TaskData successorTask = project.FirstOrDefault(t => t.Id == dependency.SuccessorTaskId);

                if (successorTask != null && await DetectCycleInternal(successorTask, startingTask, visited, currentlyVisiting))
                {
                    return true; // Cycle detected in the recursive call
                }
            }

            currentlyVisiting.Remove(currentTask);
            visited.Add(currentTask);

            return false; 
        }

        // Call this function from your AddDependency or wherever appropriate
        private async Task<bool> DetectCycle(TaskData startingTask)
        {
            HashSet<TaskData> visited = new HashSet<TaskData>();
            HashSet<TaskData> currentlyVisiting = new HashSet<TaskData>();

            return await DetectCycleInternal(startingTask, startingTask, visited, currentlyVisiting);
        }

        public async Task RefreshData()
        {
            project = taskDataService.GetTasks();
            teams = projectTeamService.GetTeams();
            milestones = milestoneService.GetMilestones();

        }

        public async Task AddDependency(List<TaskData> project, ApexChart<TaskData> chart, SelectedData<TaskData> selectedData, DependencyType selectedDependencyType, int selectedSuccessorTask)
        {
            if (selectedData != null && selectedData.DataPoint != null &&
                selectedData.DataPoint.Items.First().Id is int selectedTaskId)
            {
                TaskData predecessorTask = project.FirstOrDefault(task => task.Id == selectedTaskId);

                if (predecessorTask != null)
                {
                    DependencyType dependencyType = selectedDependencyType;
                    int selectedSuccessorTaskId = selectedSuccessorTask;
                    // input checks

                    if (selectedSuccessorTaskId == 0)
                    {
                        return;
                    }
                    if (predecessorTask.Dependencies.Any(dep => dep.SuccessorTaskId == selectedSuccessorTaskId))
                    {
                        Console.WriteLine("loxas");
                        // Display an error message, log, or handle the situation appropriately
                        return;
                    }

                    if (await DetectCycle(predecessorTask))
                    {
                        // Cycle detected, handle the situation appropriately (display error, log, etc.)
                        Console.WriteLine("cycle detected");
                        return;
                    }

                    // creeaate
                    Dependency newDependency = new Dependency
                    {
                        PredecessorTaskId = predecessorTask.Id,
                        SuccessorTaskId = selectedSuccessorTaskId,
                        Type = dependencyType
                    };

                    predecessorTask.Dependencies.Add(newDependency);
                    dependencyService.AddDependency(newDependency);
                    

                    List<TaskData> sortedTasks = SortTasksByDependencies<TaskData, Dependency>(project);
                    await Reschedule(predecessorTask.Id, sortedTasks, chart);
                    foreach (TaskData task in sortedTasks)
                    {
                        Console.WriteLine(task.Name);
                    }
                    Console.WriteLine("----------");
                }
            }

            selectedSuccessorTaskName = "";
            selectedDependencyType = DependencyType.FS;

            await RefreshData();
            await chart.UpdateSeriesAsync();
        }

        public async Task AddTask(List<TaskData> project, ApexChart<TaskData> chart, TaskData newTask, List<ProjectTeam> teams, string selectedTeamName)
        {
            try
            {
                if (!newTask.Name.IsTaskNameValid())

                {
                    errorMessage = "Task name is required.";
                    return;
                }
                if (!newTask.Name.IsValidTaskName())
                {
                    errorMessage = "Invalid task name. Only alphanumeric characters and spaces are allowed.";
                    return;
                }

                if (!newTask.StartDate.IsEndDateGreaterThanStartDate(newTask.EndDate))
                {
                    errorMessage = "End date must be greater than start date.";
                    return;
                }

                errorMessage = "";
                ProjectTeam selectedTeam = teams.FirstOrDefault(t => t.Name == selectedTeamName);

                if (selectedTeam != null)
                {
                    TaskData newTaskData = new TaskData()
                    {
                        Name = newTask.Name,
                        StartDate = newTask.StartDate,
                        EndDate = newTask.EndDate,
                        TeamId = selectedTeam.Id,
                        Progress = 0,
                        Comments = newTask.Comments
                    };
                    project.Add(newTaskData);
                    taskDataService.SyncColorWithTeam(newTaskData);
                    newTaskData.AutoProgress = (double)CalculationDataTask.CalculateAutoProgress(newTaskData);

                    taskDataService.AddTask(newTaskData);
                    

                    await RefreshData();
                    await chart.UpdateSeriesAsync();
                }
                else
                {
                    errorMessage = "Selected team not found.";
                    throw new UnexpectedDataScenarioException(errorMessage);
                }
            }
            catch (UnexpectedDataScenarioException ex)
            {
                ExceptionLogger.LogException(ex);
            }
        }

        public async Task RemoveTask(SelectedData<TaskData> selectedData, List<TaskData> project, ApexChart<TaskData> chart)
        {
            if (selectedData != null && selectedData.DataPoint != null &&
            selectedData.DataPoint.Items.First().Id is int selectedTaskId)
            { // Use LINQ to create a new list of tasks
                TaskData taskToRemove = project.FirstOrDefault(task => task.Id == selectedTaskId);

                if (taskToRemove != null)
                {
                    //taskDataService.RemoveTask(taskToRemove.Id);
                    project.Remove(taskToRemove);
                    taskDataService.RemoveTask(taskToRemove.Id);
                }
            }
            await RefreshData();
            selectedData = null; // Reset selected data after removal
            List<TaskData> sortedTasks = SortTasksByDependencies<TaskData, Dependency>(project);

            project = sortedTasks;

            await RefreshData();
            await chart.UpdateSeriesAsync();
        }




    }
}