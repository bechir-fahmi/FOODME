using Platform.ReferencialData.BusinessModel;
using Platform.ReferentialData.DtoModel.BrandData;
using Platform.Shared.Enum;
using System.ComponentModel.DataAnnotations;


namespace Platform.ReferentialData.DtoModel;

public class IntegrationMethodDTO
{
    [Required]
    public Guid IntegrationMethodId { get; set; }
    public Guid DynamicIntegrationId { get; set; }
    [Required]
    public bool UseDefaultAuth { get; set; }
    [Required]
    public IntegrationType IntegrationType { get; set; }
    public string EndPoint { get; set; }
    public ContentType Content { get; set; }
    public MethodType MethodType { get; set; }
    public virtual AuthenticationDTO MethodAuthentication { get; set; }
    public virtual DynamicIntegrationDTO DynamicIntegration { get; set; }
    public virtual List<IntegrationParameterDTO> IntegrationParameters { get; set; }
    public Guid AuthenticationId { get; set; }


}