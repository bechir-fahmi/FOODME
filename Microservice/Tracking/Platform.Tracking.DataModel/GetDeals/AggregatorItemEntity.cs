using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Tracking.DataModel.GetDeals
{
    [Table("AggregatorItem")]
    public class AggregatorItemEntity
    {
        public int Id { get; set; }
        public Guid AggregatorItemId { get; set; }
        public string? Name { get; set; }
    }
}
   