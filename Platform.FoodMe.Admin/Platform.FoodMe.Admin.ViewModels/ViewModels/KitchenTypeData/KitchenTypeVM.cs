using Platform.FoodMe.Admin.ViewModels.ViewModels.LanguageData;
using Platform.ReferencialData.BusinessModel;
using Platform.ReferencialData.BusinessModel.KitchenTypeData;
using Platform.ReferentialData.DataModel;
using Platform.Shared.Enum;

namespace Platform.FoodMe.Admin.ViewModels.ViewModels.KitchenTypeData
{
    public class KitchenTypeVM
    {
        public int Id { get; set; }

        public Guid NameLabelCode { get; set; }

        public Guid ImageLabelCode { get; set; }
        public Status Status { get; set; } = Status.isInactive;
        public LanguageResourceSet LanguageResourceSet { get; set; }
        public List<TagKitchenType> Tags { get; set; }
    }
}
