using Platform.ReferentialData.DataModel.FoodTypeData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Platform.ReferentialData.DataModel;

[Table("VendorFoodType")]
public class VendorFoodTypeEntity
{
    [Required]
    [ForeignKey("FoodTypeId")]
    public int FoodTypeId { get; set; }
    [Required]
    [ForeignKey("VendorId")]
    public virtual VendorEntity VendorModel { get; set; }
    public Guid VendorId { get; set; }
    public virtual FoodTypeEntity FoodType { get; set; }
}
