using Platform.FoodMe.Admin.ViewModels.ViewModels.LanguageData;

namespace Platform.FoodMe.Admin.ViewModels.ViewModels.WorkingTime
{
    public class ClenderWorkingTimeVM
    {
        public int Id { get; set; }

        public Guid Name { get; set; }
        public List<LanguageResourceVM> NameLanguageResources { get; set; }

        public List<DayWorkingTimeVM> UsualDailyWorkingTimes { get; set; }

        public List<ExceptionWeekWorkingTimeVM> ExceptionalWeekyWorkingTimes { get; set; }
  }
}
