using Platform.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.ReferentialData.DtoModel.FoodTypeData
{
    public class FoodTypeFilterDTO
    {
        public string Name { get; set; }
        public Status? Status { get; set; }
    }
}
