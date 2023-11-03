using Platform.ReferentialData.DataModel.TagData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Platform.ReferentialData.DataModel.MealData
{
    [Table("TagMealTiming")]

    public class TagMealTimingEntity
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TagId { get; set; }
        public int MealTimingId { get; set; }
        [Required]
        [ForeignKey("MealTimingId")]
        public virtual MealTimingEntity MealTiming { get; set; }
        public string value { get; set; }
    }
}
