using Platform.FoodMe.Admin.ViewModels.ViewModels.LanguageData;
using Platform.ReferencialData.BusinessModel;
using Platform.Shared.Enum;

namespace Platform.FoodMe.Admin.ViewModels.ViewModels.LocationData
{
    public class RegionVM
    {
        public int Id { get; set; }
        public Guid NameLabelCode { get; set; }
        public int CountryId { get; set; }
        public int? POSRegionId { get; set; }
        public Status Status { get; set; } = Status.isInactive;
        public LanguageResourceSet LanguageResourceSet { get; set; }
        public List<TagRegion> Tags { get; set; }
    }
}
