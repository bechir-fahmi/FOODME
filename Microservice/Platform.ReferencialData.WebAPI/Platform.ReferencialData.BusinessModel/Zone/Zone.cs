using Platform.ReferencialData.BusinessModel.LocationData;
using Platform.Shared.Enum;

namespace Platform.ReferencialData.BusinessModel;

public class Zone
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CountryId { get; set; }
    public int CityId { get; set; }
    public int RegionId { get; set; }
    public int AreaId { get; set; }
    public Status Status { get; set; } = Status.isInactive;
    public virtual LanguageResourceSet LanguageResourceSet { get; set; }
    public virtual Area Area { get; set; }
    public virtual City City { get; set; }
    public virtual Country Country { get; set; }
    public virtual Region Region { get; set; }
    public virtual List<TagZone> Tags { get; set; }

}
