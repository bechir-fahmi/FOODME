using Platform.ReferentialData.DataModel.LocationData;
using Platform.ReferentialData.DtoModel.LocationData;
using Platform.ReferentialData.DtoModel.TagData;

namespace Platform.ReferentialData.DtoModel;

public class TagRegionDTO
{
    public int TagId { get; set; }
    public int RegionId { get; set; }
    public virtual RegionDTO Region { get; set; }
    public string value { get; set; }
}
