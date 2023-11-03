using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Tracking.DataModel.BrandAction
{
    public class BrandActionSummaryView
    {
        public Guid BrandModelId { get; set; }
        public Guid UserId { get; set; }
        public int TypeOfAction { get; set; }
        public long TypeOfActionCount { get; set; }
    }
}
