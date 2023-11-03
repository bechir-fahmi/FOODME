using Platform.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.ReferentialData.DtoModel.MealData
{
    public class MealTimingFilterDTO
    {
        public string Name { get; set; }
        public Status? Status { get; set; }
    }
}
