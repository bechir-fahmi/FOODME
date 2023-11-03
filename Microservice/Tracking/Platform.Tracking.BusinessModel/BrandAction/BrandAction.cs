using Platform.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Tracking.BusinessModel.BrandAction
{
    public class BrandAction
    {
        public int Id { get; set; }
        public Guid BrandModelId { get; set; }
        public Guid UserId { get; set; }
        public DateTime TimeOfAction { get; set; }
        public TypeOfAction TypeOfAction { get; set; }
        public string? BrandName { get; set; }

    }
}
