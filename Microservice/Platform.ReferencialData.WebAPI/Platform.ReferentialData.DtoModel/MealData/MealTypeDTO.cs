using Platform.ReferentialData.DataModel;
using Platform.ReferentialData.DtoModel.LanguageData;
using Platform.Shared.Enum;

namespace Platform.ReferentialData.DtoModel.MealData
{
    public class MealTypeDTO
    {
        public int Id { get; set; }

        public Guid NameLabelCode { get; set; }

        public Guid ImageLabelCode { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; } = Status.isInactive;
        public virtual LanguageResourceSetDTO LanguageResourceSet { get; set; }
        public virtual List<TagMealTypeDTO> Tags { get; set; }


    }
}
