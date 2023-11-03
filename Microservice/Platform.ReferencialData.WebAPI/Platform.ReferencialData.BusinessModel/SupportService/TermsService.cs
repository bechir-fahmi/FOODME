using Platform.ReferentialData.BusinessModel.LanguageData;

namespace Platform.ReferencialData.BusinessModel.SupportService
{
    public class TermsService
    {
        public int Id { get; set; }
        public Guid NameLabelCode { get; set; }
        public virtual List<LanguageResource> NameLanguageResources { get; set; }
    }
}
