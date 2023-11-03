namespace Platform.ReferencialData.BusinessModel;

public class VendorDeliveryZone
{
    public int ZoneId { get; set; }
    public Guid VendorId { get; set; }
    public virtual Zone Zone { get; set; }
 }
