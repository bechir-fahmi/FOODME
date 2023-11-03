using Platform.ReferentialData.BusinessModel.LanguageData;
using Platform.Shared.Enum;

namespace Platform.ReferencialData.BusinessModel.SupportService
{
    public class SuportCategory
    {
        public int Id { get; set; }

        public HelpSupportType HelpSupportType { get; set; }

        public Guid NameLabelCode { get; set; }

        public Guid Image { get; set; }
        public virtual List<LanguageResource> NameLanguageResources { get; set; }
        public virtual List<LanguageResource> ImageFileResources { get; set; }

    }
}
