using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Platform.ReferentialData.DataModel.LocationData;
using Platform.ReferentialData.DataModel.TagData;

namespace Platform.ReferentialData.DataModel;
[Table("TagZone")]
public class TagZoneEntity
{
    [Required]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TagId { get; set; }
    public int ZoneId { get; set; }
    [Required]
    [ForeignKey("ZoneId")]
    public virtual ZoneEntity Zone { get; set; }
    public string value { get; set; }
}
