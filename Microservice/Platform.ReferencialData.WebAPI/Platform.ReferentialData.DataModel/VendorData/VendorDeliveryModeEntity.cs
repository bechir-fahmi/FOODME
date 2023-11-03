using Platform.ReferentialData.DataModel.DeliveryModeData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Platform.ReferentialData.DataModel;

[Table("VendorDeliveryMode")]
public class VendorDeliveryModeEntity
{
    [Required]
    [ForeignKey("DeliveryModeId")]
    public int DeliveryModeId { get; set; }
    [Required]
    [ForeignKey("VendorId")]
    public virtual VendorEntity VendorModel { get; set; }
    public Guid VendorId { get; set; }
    public virtual DeliveryModeEntity DeliveryMode { get; set; }
}
