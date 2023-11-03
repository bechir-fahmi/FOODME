namespace Platform.ReferentialData.DataModel.WorkingTime
{
    public class ClenderWorkingTimeEntity
    {
        public int Id { get; set; }
        public Guid NameLabelCode { get; set; }
        public virtual List<DayWorkingTimeEntity> UsualDailyWorkingTimes { get; set; }
        public virtual List<ExceptionWeekWorkingTimeEntity> ExceptionalWeekyWorkingTimes { get; set; }
    }
}
