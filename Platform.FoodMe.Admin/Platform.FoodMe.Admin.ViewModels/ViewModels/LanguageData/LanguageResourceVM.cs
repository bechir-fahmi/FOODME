using Platform.Shared.Enum;

namespace Platform.FoodMe.Admin.ViewModels.ViewModels.LanguageData
{
    public class LanguageResourceVM
    {
        public int Id { get; set; }
        public Guid Code { get; set; }
        public string Value { get; set; }
        public string Image { get; set; }
        public LanguageKey LanguageKey { get; set; }
    }
}
