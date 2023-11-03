using Platform.ReferentialData.DataModel.MealData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Platform.ReferentialData.DataModel;

[Table("VendorMealType")]
public class VendorMealTypeEntity
{
    [Required]
    [ForeignKey("MealTypeId")]
    public int MealTypeId { get; set; }
    [Required]
    [ForeignKey("VendorId")]
    public virtual VendorEntity VendorModel { get; set; }
    public Guid VendorId { get; set; }
    public virtual MealTypeEntity MealType { get; set; }

}
