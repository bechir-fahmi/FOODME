using Platform.ReferentialData.DataModel.BrandData;
using Platform.Shared.Enum;

namespace Platform.ReferencialData.BusinessModel;

public class IntegrationMethod
{
    public Guid IntegrationMethodId { get; set; }
    public Guid DynamicIntegrationId { get; set; }
    public bool UseDefaultAuth { get; set; }
    public IntegrationType IntegrationType { get; set; }
    public string EndPoint { get; set; }
    public ContentType Content { get; set; }
    public MethodType MethodType { get; set; }
    public virtual DynamicIntegration DynamicIntegration { get; set; }
    public virtual List<IntegrationParameter> IntegrationParameters { get; set; }
    public Guid AuthenticationId { get; set; }
    public virtual AuthenticationBM Authentication { get; set; }


}
