

using Platform.ReferencialData.BusinessModel.MealData;
using Platform.ReferencialData.BusinessModel;
using Platform.ReferentialData.DtoModel.LanguageData;
using Platform.Shared.Enum;

namespace Platform.FoodMe.Admin.ViewModels.ViewModels;

public class ZoneVM
{
    public string Name { get; set; }
    public int CountryId { get; set; }
    public int CityId { get; set; }
    public int RegionId { get; set; }
    public int AreaId { get; set; }
    public Status Status { get; set; } = Status.isInactive;
    public LanguageResourceSet LanguageResourceSet { get; set; }
    public List<TagZoneVM> Tags { get; set; }
}
