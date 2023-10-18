public class ProjectTeam
{
   public string Color { get; set; }
   public string Name { get; set; }
   public List<GanttData> Tasks { get; set; }

   private List<string> members;
   public List<String> Members
   {
      get { return members; }
      set
      {
         if (value != null && value.Distinct().Count() == value.Count)
         {
            members = value;
         }
         else
         {
            throw new ArgumentException("Members list must be non-null and contain unique names.");
         }
      }
   }

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
   public void LoadTeamTasks(List<GanttData> allTasks)
   {
      Tasks = allTasks.Where(task => task.Team.Name == Name).ToList();
   }
   public static void GetTasksForTeam(List<GanttData> allTasks, ProjectTeam team)
   {
      team.Tasks = allTasks.Where(task => task.Team.Name == team.Name).ToList();
   }
}
