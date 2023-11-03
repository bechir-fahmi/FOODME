using Platform.ReferencialData.BusinessModel.LocationData;
using Platform.ReferencialData.BusinessModel.TagData;

namespace Platform.ReferencialData.BusinessModel;

public class TagCity
{
    public int TagId { get; set; }
    public int CityId { get; set; }
    public virtual City City { get; set; }
    public string value { get; set; }
}
