using Platform.ReferencialData.BusinessModel;
using Platform.ReferentialData.DataModel.LanguageData;
using Platform.Shared.Enum;

namespace Platform.ReferentialData.BusinessModel.LanguageData
{
    public class LanguageResource
    {

        public int Id { get; set; }
        public Guid Code { get; set; }
        public string Value { get; set; }
        public string Image { get; set; }
        public LanguageKey LanguageKey { get; set; }
        public virtual Language Language { get; set; }
        public virtual LanguageResourceSet LanguageResourceSet { get; set; }
        public Guid LanguageResourceSetId { get; set; }
    }
}
