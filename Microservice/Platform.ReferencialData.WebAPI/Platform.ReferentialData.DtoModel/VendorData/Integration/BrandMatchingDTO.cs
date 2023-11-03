using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.ReferentialData.DtoModel.BrandData.Integration;

public class BrandMatchingDTO
{
    public Guid AggregatorId { get; set; }  
    public int DistantBrandId { get; set; }  
    public Guid LocalBrandId { get; set; }  
}
