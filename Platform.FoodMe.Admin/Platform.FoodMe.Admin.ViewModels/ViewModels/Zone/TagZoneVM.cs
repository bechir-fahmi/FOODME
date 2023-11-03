using Platform.ReferencialData.BusinessModel;
using Platform.ReferentialData.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.FoodMe.Admin.ViewModels.ViewModels;

public class TagZoneVM
{
    public int IdTag { get; set; }
    public int ZoneId { get; set; }
    public Zone Zone { get; set; }
    public string Tag { get; set; }
}
