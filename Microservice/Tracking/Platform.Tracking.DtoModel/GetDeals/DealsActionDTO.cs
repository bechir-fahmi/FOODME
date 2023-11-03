using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Tracking.DtoModel.GetDeals
{
    public class DealsActionDTO
    {
        public int Id { get; set; }
        public Guid BrandId { get; set; }
        public Guid UserId { get; set; }

        public List<AggregatorItemDTO>? Aggregator { get; set; }
        public DateTime TimeOfAction { get; set; }
    }


}
