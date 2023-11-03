using Platform.ReferencialData.BusinessModel.TagData;
using Platform.ReferentialData.DataModel.FoodTypeData;
using Platform.ReferentialData.DataModel.TagData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.ReferencialData.BusinessModel.FoodTypeData
{
    public class TagFoodType
    {
        public int TagId { get; set; }
        public int FoodTypeId { get; set; }
        public virtual FoodType FoodType { get; set; }
        public string value { get; set; }
    }
}
