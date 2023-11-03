using Platform.FoodMe.Admin.ViewModels.ViewModels.LanguageData;
using Platform.ReferencialData.BusinessModel;
using Platform.ReferentialData.DtoModel;
using Platform.Shared.Enum;

namespace Platform.FoodMe.Admin.ViewModels.ViewModels.LocationData
{
    public class CityVM
    {
        public int Id { get; set; }
        public Guid NameLabelCode { get; set; }
        public int RegionId { get; set; }
        public string CityCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int? POSCityId { get; set; }
        public Status Status { get; set; } = Status.isInactive;
        public LanguageResourceSet LanguageResourceSet { get; set; }
        public List<TagCity> Tags { get; set; }
    }
}
