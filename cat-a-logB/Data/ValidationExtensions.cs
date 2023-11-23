namespace cat_a_logB.Data
{
    public static class ValidationExtensions
    {
        public static bool IsTaskNameValid(this string taskName)
        {
            return !string.IsNullOrWhiteSpace(taskName);
        }

        public static bool IsEndDateGreaterThanStartDate(this DateTime startDate, DateTime endDate)
        {
            return startDate < endDate;
        }

    }
}
