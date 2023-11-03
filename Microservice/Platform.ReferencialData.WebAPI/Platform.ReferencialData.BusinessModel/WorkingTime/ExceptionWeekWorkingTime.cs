using Platform.ReferentialData.BusinessModel.LanguageData;

namespace Platform.ReferencialData.BusinessModel.WorkingTime
{
    public class ExceptionWeekWorkingTime
    {
        public int Id { get; set; }
        public Guid NameLabelCode { get; set; }
        public Guid DescriptionLabelCode { get; set; }
        public virtual List<LanguageResource> NameLanguageResources { get; set; }
        public virtual List<LanguageResource> DescriptionLanguageResources { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public virtual List<ExceptionDayWorkingTime> ExceptionalDailyWorkingTimes { get; set; }
        public bool DeActivated { get; set; }
        public int ClenderWorkingTimeId { get; set; }

    }
}
