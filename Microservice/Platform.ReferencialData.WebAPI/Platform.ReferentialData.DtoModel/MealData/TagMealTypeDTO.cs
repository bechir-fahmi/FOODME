using Platform.ReferencialData.BusinessModel.MealData;
using Platform.ReferentialData.DtoModel.TagData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.ReferentialData.DtoModel.MealData
{
    public class TagMealTypeDTO
    {
        public int TagId { get; set; }
        public int MealTypeId { get; set; }
        public virtual MealTypeDTO MealType { get; set; }
        public string value { get; set; }
    }
}
