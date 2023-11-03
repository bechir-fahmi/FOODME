using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Platform.ReferentialData.DataModel.WorkingTime
{
    public class ExceptionDayWorkingTimeEntity
    {
        public int Id { get; set; }
        public DayOfWeek? Day { get; set; }
        public TimeSpan MorningStartTime { get; set; }

        public TimeSpan MorningCloseTime { get; set; }

        public TimeSpan AfterNoonStartTime { get; set; }

        public TimeSpan AfterNoonCloseTime { get; set; }

        public bool IsClosed { get; set; }

        public virtual ExceptionWeekWorkingTimeEntity ExceptionWeekWorkingTimeEntity { get; set; }

    }





}
