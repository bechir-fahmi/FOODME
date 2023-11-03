using Platform.ReferentialData.DtoModel.FoodTypeData;
using Platform.Shared.Enum;

namespace Platform.ReferentialData.DtoModel.LanguageData
{
    public class LanguageDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public Status Status { get; set; } = Status.isInactive;
        public bool isDefault { get; set; }
        public virtual List<TagLanguageDTO> Tags { get; set; }

    }
}
