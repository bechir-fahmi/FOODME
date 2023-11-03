using Platform.FoodMe.Admin.ViewModels.ViewModels.LanguageData;
using Platform.Shared.Enum;

namespace Platform.FoodMe.Admin.ViewModels.ViewModels.SupportServiceData
{
    public class SuportCategoryVM
    {
        public int Id { get; set; }

        public HelpSupportType HelpSupportType { get; set; }

        public Guid NameLabelCode { get; set; }

        public Guid Image { get; set; }
        public List<LanguageResourceVM> NameLanguageResources { get; set; }
        public List<LanguageResourceVM> ImageFileResources { get; set; }

    }
}
