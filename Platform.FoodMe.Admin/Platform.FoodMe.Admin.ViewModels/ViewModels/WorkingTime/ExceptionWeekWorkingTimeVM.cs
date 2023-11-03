using Platform.FoodMe.Admin.ViewModels.ViewModels.LanguageData;

namespace Platform.FoodMe.Admin.ViewModels.ViewModels.WorkingTime
{
    public class ExceptionWeekWorkingTimeVM
    {
        public int Id { get; set; }
        public Guid NameLabelCode { get; set; }
        public Guid DescriptionLabelCode { get; set; }
        public List<LanguageResourceVM> NameLanguageResources { get; set; }
        public List<LanguageResourceVM> DescriptionLanguageResources { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public List<ExceptionDayWorkingTimeVM> ExceptionalDailyWorkingTimes { get; set; }
        public bool DeActivated { get; set; }
        public int ClenderWorkingTimeId { get; set; }
    }
}
