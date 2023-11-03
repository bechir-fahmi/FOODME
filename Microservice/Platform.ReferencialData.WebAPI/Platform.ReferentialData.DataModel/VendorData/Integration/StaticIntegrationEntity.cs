using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Platform.ReferentialData.DataModel.BrandData.IntegrationBrand;

[Table("StaticIntegration")]
public class StaticIntegrationEntity : ReferentialDataBase
{
    [Key]
    [Required]
    public Guid StaticIntegrationId { get; set; }
    [Required]
    [ForeignKey("VendorId")]
    public Guid VendorId { get; set; }
    [Required]
    [ForeignKey("VendorId")]
    public int ZoneId { get; set; }
    [Required]
    public double Fees { get; set; }
    [Required]
    public int TimeEstimation { get; set; }
    public virtual VendorEntity VendorModel { get; set; }
    public virtual ZoneEntity Zone { get; set; }
}
