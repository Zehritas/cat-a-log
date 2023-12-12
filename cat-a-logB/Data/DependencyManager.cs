using ApexCharts;
using cat_a_logB.Service;
using Microsoft.AspNetCore.Components;

//namespace cat_a_logB.Data
//{
    //public class DependencyManager
    //{
    //    private readonly IDependencyService dependencyService;
    //    private readonly ITaskDataService taskDataService;
    //    private readonly IProjectTeamService projectTeamService;
    //    private readonly IMilestoneService milestoneService;

    //    public DependencyManager(IDependencyService _dependencyService, ITaskDataService _taskDataService, IProjectTeamService _projectTeamService, IMilestoneService _milestoneService)
    //    {
    //        dependencyService = _dependencyService;
    //        taskDataService = _taskDataService;
    //        projectTeamService = _projectTeamService;
    //        milestoneService = _milestoneService;
    //    }

    //    public DependencyManager() { }

    //    public List<T> SortTasksByDependencies<T, U>(List<T> tasks)
    //    where T : TaskData
    //    where U : Dependency
    //    {
    //        var graph = new Dictionary<int, List<int>>();
    //        var inDegree = new Dictionary<int, int>();

    //        // Build the graph and calculate in-degrees
    //        foreach (var task in tasks)
    //        {
    //            graph[task.Id] = new List<int>();
    //            inDegree[task.Id] = 0;
    //        }

    //        foreach (var task in tasks)
    //        {
    //            foreach (var dependency in task.Dependencies)
    //            {
    //                if (graph.ContainsKey(dependency.SuccessorTaskId) && task.Id != dependency.SuccessorTaskId) // Check if the key exists
    //                {
    //                    graph[dependency.SuccessorTaskId].Add(task.Id);
    //                    inDegree[task.Id]++;
    //                }
    //                else
    //                {
    //                    // Handle the case where the key doesn't exist (e.g., log or display an error)
    //                    Console.WriteLine($"Dependency successor task '{dependency.SuccessorTaskId}' not found.");
    //                }
    //            }
    //        }


    //        var sortedTasks = new List<T>();
    //        var queue = new Queue<int>(inDegree.Where(entry => entry.Value == 0).Select(entry => entry.Key));

    //        while (queue.Count > 0)
    //        {
    //            var currentTaskId = queue.Dequeue();
    //            var currentTask = tasks.First(task => (task as TaskData).Id == currentTaskId);

    //            sortedTasks.Add(currentTask);

    //            foreach (var dependentTaskId in graph[currentTaskId])
    //            {
    //                inDegree[dependentTaskId]--;
    //                if (inDegree[dependentTaskId] == 0)
    //                {
    //                    queue.Enqueue(dependentTaskId);
    //                }
    //            }
    //        }

    //        return sortedTasks;
    //    }


    //    async void DeleteDependency(List<Dependency> dependencies, Dependency dependency)
    //    {
    //        DependencyService.RemoveDependency(dependency);
    //        await RefreshData();
    //    }
    //    private async Task<bool> DetectCycleInternal(TaskData currentTask, TaskData startingTask, HashSet<TaskData> visited,
    //    HashSet<TaskData> currentlyVisiting)
    //    {
    //        if (currentlyVisiting.Contains(currentTask))
    //        {
    //            // Cycle detected
    //            // Console.WriteLine("cycle detected");
    //            return true;
    //        }

    //        if (visited.Contains(currentTask))
    //        {
    //            // Already visited, no cycle
    //            return false;
    //        }

    //        currentlyVisiting.Add(currentTask);

    //        foreach (var dependency in currentTask.Dependencies)
    //        {
    //            TaskData successorTask = project.FirstOrDefault(t => t.Id == dependency.SuccessorTaskId);

    //            if (successorTask != null && await DetectCycleInternal(successorTask, startingTask, visited, currentlyVisiting))
    //            {
    //                return true; // Cycle detected in the recursive call
    //            }
    //        }

    //        currentlyVisiting.Remove(currentTask);
    //        visited.Add(currentTask);

    //        return false; // No cycle detected
    //    }

    //    // Call this function from your AddDependency or wherever appropriate
    //    private async Task<bool> DetectCycle(TaskData startingTask)
    //    {
    //        HashSet<TaskData> visited = new HashSet<TaskData>();
    //        HashSet<TaskData> currentlyVisiting = new HashSet<TaskData>();

    //        return await DetectCycleInternal(startingTask, startingTask, visited, currentlyVisiting);
    //    }



//    }
//}
