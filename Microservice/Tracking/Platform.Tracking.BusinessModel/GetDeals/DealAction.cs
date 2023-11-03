using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Tracking.BusinessModel.GetDeals
{
    public class DealAction
    {
        public int Id { get; set; }
        public Guid BrandId { get; set; }
        public Guid UserId { get; set; }
        public List<AggregatorItem>? Aggregator { get; set; }
        public DateTime TimeOfAction { get; set; }
    }

}
