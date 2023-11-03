using Platform.ReferencialData.BusinessModel.FoodTypeData;
using Platform.ReferentialData.DataModel.TagData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Platform.ReferentialData.DtoModel.TagData;

namespace Platform.ReferentialData.DtoModel.FoodTypeData
{
    public class TagFoodTypeDTO
    {
        public int TagId { get; set; }
        public int FoodTypeId { get; set; }
        public virtual FoodTypeDTO FoodType { get; set; }
        public string value { get; set; }
    }
}
