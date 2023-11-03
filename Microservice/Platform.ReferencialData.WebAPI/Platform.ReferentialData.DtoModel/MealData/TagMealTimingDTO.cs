using Platform.ReferentialData.DataModel.MealData;
using Platform.ReferentialData.DtoModel.TagData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.ReferentialData.DtoModel.MealData
{
    public class TagMealTimingDTO
    {
        public int TagId { get; set; }
        public int MealTimingId { get; set; }
        public virtual MealTimingDTO MealTiming { get; set; }
        public string value { get; set; }
    }
}
