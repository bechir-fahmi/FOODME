using Platform.ReferencialData.BusinessModel.TagData;

namespace Platform.ReferencialData.BusinessModel;

public class TagZone
{
    public int TagId { get; set; }
    public int ZoneId { get; set; }
    public virtual Zone Zone { get; set; }
    public string value { get; set; }

}
