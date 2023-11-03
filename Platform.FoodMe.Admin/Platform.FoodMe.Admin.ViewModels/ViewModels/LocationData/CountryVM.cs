using Platform.FoodMe.Admin.ViewModels.ViewModels.LanguageData;

namespace Platform.FoodMe.Admin.ViewModels.ViewModels.LocationData
{
    public class CountryVM
    {
        public int Id { get; set; }
        public Guid NameLabelCode { get; set; }
        public string Code { get; set; }
        public string CountryKey { get; set; }
        public int? POSCountryId { get; set; }
        public List<LanguageResourceVM> LanguageResources { get; set; }

    }
}
