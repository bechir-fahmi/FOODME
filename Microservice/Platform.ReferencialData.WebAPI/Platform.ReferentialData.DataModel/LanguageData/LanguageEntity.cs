using Platform.Shared.Enum;

namespace Platform.ReferentialData.DataModel.LanguageData
{
    public class LanguageEntity : ReferentialDataBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public LanguageKey Key { get; set; }
        public bool isDefault { get; set; } = false;
        public virtual List<TagLanguageEntity> Tags { get; set; }
    }
}
