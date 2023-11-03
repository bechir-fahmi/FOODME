using Platform.FoodMe.Admin.ViewModels.ViewModels.LanguageData;

namespace Platform.FoodMe.Admin.ViewModels.ViewModels.SupportServiceData
{
    public class QuestionAnswerVM
    {
        public int Id { get; set; }
        public Guid QuestionLabelCode { get; set; }
        public Guid AnswerLabelCode { get; set; }

        public int SuportCategoryId { get; set; }
        public List<LanguageResourceVM> QuestionLanguageResources { get; set; }
        public List<LanguageResourceVM> AnswerLanguageResources { get; set; }
    }
}
