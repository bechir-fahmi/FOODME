using Platform.ReferentialData.DataModel.LocationData;
using Platform.ReferentialData.DataModel.TagData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Platform.ReferentialData.DataModel;
[Table("TagCity")]
public class TagCityEntity
{
    [Required]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TagId { get; set; }
    public int CityId { get; set; }
    [Required]
    [ForeignKey("CityId")]
    public virtual CityEntity City { get; set; }
    public string value { get; set; }
}
