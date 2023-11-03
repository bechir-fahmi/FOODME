namespace Platform.ReferencialData.BusinessModel.WorkingTime
{
    public class DayWorkingTime
    {
        public int Id { get; set; }

        public DayOfWeek Day { get; set; }

        public TimeSpan MorningStartTime { get; set; }

        public TimeSpan MorningCloseTime { get; set; }

        public TimeSpan AfterNoonStartTime { get; set; }

        public TimeSpan AfterNoonCloseTime { get; set; }

        public bool IsClosed { get; set; }

        public int ClenderWorkingTimeId { get; set; }

    }
    
}