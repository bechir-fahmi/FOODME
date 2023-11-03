using Platform.ReferentialData.DataModel.TagData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Platform.ReferentialData.DataModel.FoodTypeData
{
    [Table("TagFoodType")]
    public class TagFoodTypeEntity
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TagId { get; set; }
        public int FoodTypeId { get; set; }
        [Required]
        [ForeignKey("FoodTypeId")]
        public virtual FoodTypeEntity FoodType { get; set; }
        public string value { get; set; }
    }
}
