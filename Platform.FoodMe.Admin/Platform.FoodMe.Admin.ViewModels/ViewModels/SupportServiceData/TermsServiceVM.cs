using Platform.FoodMe.Admin.ViewModels.ViewModels.LanguageData;

namespace Platform.FoodMe.Admin.ViewModels.ViewModels.SupportServiceData
{
    public class TermsServiceVM
    {
        public int Id { get; set; }
        public Guid NameLabelCode { get; set; }
        public List<LanguageResourceVM> NameLanguageResources { get; set; }
    }
}
