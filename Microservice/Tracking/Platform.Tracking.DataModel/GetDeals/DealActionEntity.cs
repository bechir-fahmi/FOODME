using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Tracking.DataModel.GetDeals
{
    public class DealActionEntity
    {
        public int Id { get; set; }
        public Guid BrandId { get; set; }
        public Guid UserId { get; set; }
        public List<AggregatorItemEntity>? Aggregator { get; set; }
        public DateTime TimeOfAction { get; set; }
    }


}
