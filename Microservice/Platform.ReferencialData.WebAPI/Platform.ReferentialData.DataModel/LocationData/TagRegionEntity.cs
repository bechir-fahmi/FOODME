using Platform.ReferentialData.DataModel.LocationData;
using Platform.ReferentialData.DataModel.TagData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Platform.ReferentialData.DataModel;
[Table("TagRegion")]

public class TagRegionEntity
{
    [Required]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TagId { get; set; }
    public int RegionId { get; set; }
    [ForeignKey("RegionId")]
    public virtual RegionEntity Region { get; set; }
    public string value { get; set; }
}
