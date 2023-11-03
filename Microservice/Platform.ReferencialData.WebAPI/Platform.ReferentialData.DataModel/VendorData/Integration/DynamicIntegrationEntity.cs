using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Platform.ReferentialData.DataModel.BrandData;

[Table("DynamicIntegration")]
public class DynamicIntegrationEntity 
{
    [Key]
    [Required]
    public Guid DynamicIntegrationId { get; set; }
    [Required]
    public Guid VendorId { get; set; }
    [Required]
    public string URi { get; set; }
    public string Port { get; set; }
    public string http { get; set; }
    [ForeignKey("VendorId")]
    public virtual VendorEntity VendorModel { get; set; } 
    [ForeignKey("AuthenticationId")]
    public Guid AuthenticationId { get; set; }
    public virtual AuthenticationEntity Authentication { get; set; }
    public virtual List<IntegrationMethodEntity> IntegrationMethod { get; set; }

}
