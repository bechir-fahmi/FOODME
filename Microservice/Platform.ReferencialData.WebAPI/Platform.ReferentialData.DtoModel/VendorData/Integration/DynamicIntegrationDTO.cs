namespace Platform.ReferentialData.DtoModel.BrandData;

public class DynamicIntegrationDTO
{
    public Guid DynamicIntegrationId { get; set; }
    public Guid VendorId { get; set; }
    public string URi { get; set; }
    public string Port { get; set; }
    public string http { get; set; }
    public Guid AuthenticationId { get; set; }
    public Guid? BrandEntityId { get; set; }
    public Guid? AggregatorEntityId { get; set; }
    public virtual AuthenticationDTO Authentication { get; set; }
    public virtual List<IntegrationMethodDTO> IntegrationMethod { get; set; }
}
