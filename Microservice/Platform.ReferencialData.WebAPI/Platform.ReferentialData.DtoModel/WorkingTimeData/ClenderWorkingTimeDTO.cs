using Platform.ReferentialData.DtoModel.LanguageData;

namespace Platform.ReferentialData.DtoModel.WorkingTimeData
{

    public class ClenderWorkingTimeDTO
    {
        public int Id { get; set; }

        public Guid NameLabelCode { get; set; }
        public virtual List<LanguageResourceDTO> NameLanguageResources { get; set; }

        public virtual List<DayWorkingTimeDTO> UsualDailyWorkingTimes { get; set; }

        public virtual List<ExceptionWeekWorkingTimeDTO> ExceptionalWeekyWorkingTimes { get; set; }

    }
}
