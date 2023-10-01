namespace cat_a_logB
{
    public class GanttData
    {
        public string Name { get; set; } // Task name
        public DateTime StartDate { get; set; } // Start date and time

        public DateTime EndDate { get; set; } // End date and time

        // Constructors to ensure proper initialization
        public GanttData()
        {
            StartDate = DateTime.Now.Date; // dcfhxertfšįųyūu9i0 Initialize StartDate to today's date at midnight
            EndDate = DateTime.Now.Date;   // Initialize EndDate to today's date at midnight
        }

        public GanttData(string name, DateTime startDate, DateTime endDate)
        {
            Name = name;
            StartDate = startDate.Date; // Set the time component to midnight
            EndDate = endDate.Date;     // Set the time component to midnight
        }
    }
}
