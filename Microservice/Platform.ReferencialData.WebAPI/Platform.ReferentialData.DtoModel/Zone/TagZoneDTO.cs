using Platform.ReferentialData.DataModel;
using Platform.ReferentialData.DtoModel.TagData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.ReferentialData.DtoModel;

public class TagZoneDTO
{
    public int TagId { get; set; }
    public int ZoneId { get; set; }
    public virtual ZoneDTO Zone { get; set; }
    public string value { get; set; }
}
