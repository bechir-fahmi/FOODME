using Platform.ReferentialData.DataModel.TagData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Platform.ReferentialData.DataModel.KitchenTypeData
{
    [Table("TagKitchenType")]
    public class TagKitchenTypeEntity
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TagId { get; set; }
        public int KitchenTypeId { get; set; }
        [Required]
        [ForeignKey("KitchenTypeId")]
        public virtual KitchenTypeEntity KitchenType { get; set; }
        public string value { get; set; }
    }
}
