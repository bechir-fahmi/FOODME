using Platform.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.ReferencialData.BusinessModel.AgeRangeData
{
    public class AgeRange
    {
        public int Id { get; set; }
        public int MaxAge { get; set; }
        public int MinAge { get; set; }
        public Status Status { get; set; } = Status.isInactive;
    }
}
