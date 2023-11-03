using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.ReferentialData.DataModel.WorkingTime
{
    public class DayWorkingTimeEntity
    {
        public int Id { get; set; }

        public DayOfWeek Day { get; set; }

        public TimeSpan MorningStartTime { get; set; }

        public TimeSpan MorningCloseTime { get; set; }

        public TimeSpan AfterNoonStartTime { get; set; }

        public TimeSpan AfterNoonCloseTime { get; set; }

        public bool IsClosed { get; set; }

      
        public virtual ClenderWorkingTimeEntity ClenderWorkingTimeEntity { get; set; }
    }
}
