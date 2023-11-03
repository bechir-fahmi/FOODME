using Platform.ReferentialData.DataModel.BrandData;
using Platform.Shared.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Platform.ReferentialData.DataModel;

[Table("IntegrationMethod")]
public class IntegrationMethodEntity
{
    [Key]
    [Required]
    public Guid IntegrationMethodId { get; set; }
    [Required]
    [ForeignKey("DynamicIntegrationId")]
    public Guid DynamicIntegrationId { get; set; }
    [Required]
    public bool UseDefaultAuth { get; set; }
    [Required]
    public IntegrationType IntegrationType { get; set; }
    [Required]
    public string EndPoint { get; set; }
    [Required]
    public ContentType Content { get; set; }
    [Required]
    public MethodType MethodType { get; set; }
    public virtual DynamicIntegrationEntity DynamicIntegration { get; set; }
    public virtual List<IntegrationParameterEntity> IntegrationParameters { get; set; }
    [Required]
    [ForeignKey("AuthenticationId")]
    public Guid AuthenticationId { get; set; }
    public virtual AuthenticationEntity Authentication { get; set; }
}
