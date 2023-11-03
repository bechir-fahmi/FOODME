using Platform.ReferentialData.DataModel.KitchenTypeData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Platform.ReferentialData.DataModel;

[Table("VendorKitchenType")]
public class VendorKitchenTypeEntity
{
    [Required]
    [ForeignKey("KitchenTypeId")]
    public int KitchenTypeId { get; set; }
    [Required]
    [ForeignKey("VendorId")]
    public virtual VendorEntity VendorModel { get; set; }
    public Guid VendorId { get; set; }
    public virtual KitchenTypeEntity KitchenType { get; set; }
}
