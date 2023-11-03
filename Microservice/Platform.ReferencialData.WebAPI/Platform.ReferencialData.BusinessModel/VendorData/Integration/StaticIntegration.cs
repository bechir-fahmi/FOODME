namespace Platform.ReferencialData.BusinessModel;

public class StaticIntegration
{
    public Guid StaticIntegrationId { get; set; }
    public Guid VendorId { get; set; }
    public int ZoneId { get; set; }
    public double Fees { get; set; }
    public int TimeEstimation { get; set; }
    public virtual Vendor VendorModel { get; set; }
    public virtual Zone Zone { get; set; }
}
