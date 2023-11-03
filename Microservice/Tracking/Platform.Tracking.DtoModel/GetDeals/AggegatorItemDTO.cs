using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Tracking.DtoModel.GetDeals
{
    public class AggregatorItemDTO
    {
        public int Id { get; set; }
        public Guid AggregatorItemId { get; set; }
        public string? Name { get; set; }
    }
}

