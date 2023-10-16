public class ProjectTeam
{
   public string Color { get; set; }
   public string Name { get; set; }
   public List<GanttData> Tasks { get; set; }
   public List<String> Members { get; set; }

   public ProjectTeam(string color, string name, List<string> members)
   {
      Color = color;
      Name = name;
      Tasks = new List<GanttData>();
      Members = members;
   }

   public ProjectTeam(string color, string name)
   {
      Color = color;
      Name = name;
      Tasks = new List<GanttData>();
   }
   public ProjectTeam()
   {
   }

   public static void GetTasksForTeam(List<GanttData> allTasks, ProjectTeam team)
   {
      team.Tasks = allTasks.Where(task => task.Team.Name == team.Name).ToList();
   }
}
