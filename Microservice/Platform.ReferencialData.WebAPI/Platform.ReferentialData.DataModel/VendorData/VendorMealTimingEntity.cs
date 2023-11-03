using Platform.ReferentialData.DataModel.MealData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Platform.ReferentialData.DataModel;

[Table("VendorMealTiming")]
public class VendorMealTimingEntity
{
    [Required]
    [ForeignKey("MealTimingId")]
    public int MealTimingId { get; set; }
    [Required]
    [ForeignKey("VendorId")]
    public virtual VendorEntity VendorModel { get; set; }
    public Guid VendorId { get; set; }
    public virtual MealTimingEntity MealTiming { get; set; }

}
