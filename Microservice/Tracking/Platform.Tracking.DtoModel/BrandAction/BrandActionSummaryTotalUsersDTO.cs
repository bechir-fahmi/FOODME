using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Tracking.DtoModel.BrandAction
{
    public class BrandActionSummaryTotalUsersDTO
    {
        public Guid BrandModelId { get; set; }
        public long TotalUsers { get; set; }
        public long GoToAppCount { get; set; }
        public long ViewDetailsCount { get; set; }
    }
}
