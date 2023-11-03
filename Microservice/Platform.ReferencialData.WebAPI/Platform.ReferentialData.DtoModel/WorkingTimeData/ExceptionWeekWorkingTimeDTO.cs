using Platform.ReferentialData.DtoModel.LanguageData;

namespace Platform.ReferentialData.DtoModel.WorkingTimeData
{
    public class ExceptionWeekWorkingTimeDTO
    {
        public int Id { get; set; }
        public Guid NameLabelCode { get; set; }
        public Guid DescriptionLabelCode { get; set; }
        public virtual List<LanguageResourceDTO> NameLanguageResources { get; set; }
        public virtual List<LanguageResourceDTO> DescriptionLanguageResources { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public virtual List<ExceptionDayWorkingTimeDTO> ExceptionalDailyWorkingTimes { get; set; }
        public bool DeActivated { get; set; }
        public int ClenderWorkingTimeId { get; set; }
    }
}
