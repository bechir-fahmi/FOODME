using Platform.ReferencialData.BusinessModel.LanguageData;
using Platform.Shared.Enum;

namespace Platform.ReferentialData.BusinessModel.LanguageData
{
    public class Language
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public LanguageKey Key { get; set; }
        public Status Status { get; set; } = Status.isInactive;
        public bool isDefault { get; set; }
        public virtual List<TagLanguage> Tags { get; set; }

    }
}
