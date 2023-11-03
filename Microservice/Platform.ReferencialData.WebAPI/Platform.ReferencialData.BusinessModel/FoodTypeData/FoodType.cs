using Platform.ReferentialData.BusinessModel.LanguageData;
using Platform.Shared.Enum;

namespace Platform.ReferencialData.BusinessModel.FoodTypeData
{
    public class FoodType
    {
        public int Id { get; set; }

        public Guid NameLabelCode { get; set; }

        public Guid ImageLabelCode { get; set; }
        public string Name { get; set; }
        public virtual LanguageResourceSet  LanguageResourceSet{ get; set; }
        public Status Status { get; set; } = Status.isInactive;
        public virtual List<TagFoodType> Tags { get; set; }
    }
}
