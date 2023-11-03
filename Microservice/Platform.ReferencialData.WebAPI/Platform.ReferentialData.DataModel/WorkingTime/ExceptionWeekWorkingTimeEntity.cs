using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.ReferentialData.DataModel.WorkingTime
{
    public class ExceptionWeekWorkingTimeEntity
    {
        public int Id { get; set; }
        public Guid NameLabelCode { get; set; }
        public Guid DescriptionLabelCode { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public virtual List<ExceptionDayWorkingTimeEntity> ExceptionalDailyWorkingTimes { get; set; }
        public bool DeActivated { get; set; }
       
        public virtual ClenderWorkingTimeEntity ClenderWorkingTimeEntity { get; set; }

    }
}
