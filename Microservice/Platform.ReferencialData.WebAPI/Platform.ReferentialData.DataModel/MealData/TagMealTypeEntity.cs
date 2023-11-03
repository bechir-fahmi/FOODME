using Platform.ReferentialData.DataModel.TagData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Platform.ReferentialData.DataModel.MealData
{
    [Table("TagMealType")]

    public class TagMealTypeEntity
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TagId { get; set; }
        public int MealTypeId { get; set; }
        [Required]
        [ForeignKey("MealTypeId")]
        public virtual MealTypeEntity MealType { get; set; }
        public string value { get; set; }
    }
}
