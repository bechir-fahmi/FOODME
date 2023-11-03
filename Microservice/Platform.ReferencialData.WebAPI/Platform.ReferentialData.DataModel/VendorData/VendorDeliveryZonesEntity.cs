using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Platform.ReferentialData.DataModel;

[Table("VendorDeliveryZones")]
public class VendorDeliveryZonesEntity
{
    [Required]
    [ForeignKey("Id")]
    public int ZoneId { get; set; }
    public virtual ZoneEntity Zone { get; set; }
    [Required]
    [ForeignKey("VendorId")]
    public virtual VendorEntity VendorModel { get; set; }
    public Guid VendorId { get; set; }
}
