using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Shared.Enum
{
    public enum ReductionConditionType
    {
        OrderMinimum = 1,
        OrderMaximum = 2,
        MaximumDiscount = 3,
        /// <summary>
        /// Cannot have discount reductions total amount 
        /// more than this value for one month
        /// </summary>
        ReachMonthLimit = 4,
    }
}
