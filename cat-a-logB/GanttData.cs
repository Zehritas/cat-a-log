namespace cat_a_logB
{
    public class GanttData
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public GanttData()
        {
            StartDate = DateTime.Now.Date;
            EndDate = DateTime.Now.Date;
        }
    }
}