using Platform.FoodMe.Admin.ViewModels.ViewModels.LanguageData;
using Platform.ReferencialData.BusinessModel;
using Platform.ReferencialData.BusinessModel.KitchenTypeData;
using Platform.ReferencialData.BusinessModel.MealData;
using Platform.ReferentialData.DataModel;
using Platform.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.FoodMe.Admin.ViewModels.ViewModels.MealData
{
    public class MealTypeVM
    {
        public int Id { get; set; }

        public Guid NameLabelCode { get; set; }

        public Guid ImageLabelCode { get; set; }
        public Status Status { get; set; } = Status.isInactive;

        public LanguageResourceSet LanguageResourceSet { get; set; }
        public List<TagMealType> Tags { get; set; }

    }
}
