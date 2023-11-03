using Platform.ReferentialData.BusinessModel.LanguageData;

namespace Platform.ReferencialData.BusinessModel.WorkingTime
{
    public class ClenderWorkingTime
    {
        public int Id { get; set; }

        public Guid Name { get; set; }
        public virtual List<LanguageResource> NameLanguageResources { get; set; }

        public virtual List<DayWorkingTime> UsualDailyWorkingTimes { get; set; }

        public virtual List<ExceptionWeekWorkingTime> ExceptionalWeekyWorkingTimes { get; set; }

    }
}
