using Platform.ReferencialData.BusinessModel.LanguageData;
using Platform.Shared.Enum;

namespace Platform.FoodMe.Admin.ViewModels.ViewModels.LanguageData
{
    public class LanguageVM
    {
        public int Id { get; set; }
        public string LanguageName { get; set; }
        public Status Status { get; set; } = Status.isInactive;
        public bool isDefault { get; set; }
        public List<TagLanguage> Tags { get; set; }
    }
}
