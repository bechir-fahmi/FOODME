using Platform.ReferentialData.DataModel.LocationData;
using Platform.ReferentialData.DtoModel.LocationData;
using Platform.ReferentialData.DtoModel.TagData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.ReferentialData.DtoModel;

public class TagCityDTO
{
    public int TagId { get; set; }
    public int CityId { get; set; }
    public virtual CityDTO City { get; set; }
    public string value { get; set; }
}
