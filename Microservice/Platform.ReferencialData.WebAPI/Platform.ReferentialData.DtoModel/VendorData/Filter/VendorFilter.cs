namespace Platform.ReferentialData.DtoModel;

public class VendorFilter
{
    public string Name { get; set; }
    public List<int> DeliveryModeIds { get; set; }
    public List<int> FoodTypeIds { get; set; }
    public List<int> KitchenTypeIds { get; set; }
    public List<int> MealTimingIds { get; set; }
    public List<int> MealTypeIds { get; set; }
    public string RegionName { get; set; }
    public string ZoneName { get; set; }
    public string CountryName { get; set; }
    public string CityCode { get; set; }
    public double? Longitude { get; set; }
    public double? Latitude { get; set; }

}
