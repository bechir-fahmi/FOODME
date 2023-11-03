using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.ReferencialData.BusinessModel.BrandData.Integration;

public class BrandMatching
{
    public Guid AggregatorId { get; set; }
    public int DistantBrandId { get; set; }
    public Guid LocalBrandId { get; set; }
}
