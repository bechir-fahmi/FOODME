using Platform.ReferencialData.BusinessModel.LocationData;
using Platform.ReferencialData.BusinessModel.TagData;

namespace Platform.ReferencialData.BusinessModel;

public class TagRegion
{
    public int TagId { get; set; }
    public int RegionId { get; set; }
    public virtual Region Region { get; set; }
    public string value { get; set; }
}
