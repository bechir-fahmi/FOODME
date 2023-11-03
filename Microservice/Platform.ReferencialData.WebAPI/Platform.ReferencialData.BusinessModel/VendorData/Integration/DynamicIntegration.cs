namespace Platform.ReferencialData.BusinessModel;

public class DynamicIntegration
{
    public Guid DynamicIntegrationId { get; set; }
    public Guid VendorId { get; set; }
    public string URi { get; set; }
    public string Port { get; set; }
    public string http { get; set; }
    public Guid AuthenticationId { get; set; }
    public virtual AuthenticationBM Authentication { get; set; }
    public virtual List<IntegrationMethod> IntegrationMethod { get; set; }
}
