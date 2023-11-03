using Platform.ReferentialData.DtoModel.LanguageData;

namespace Platform.ReferentialData.DtoModel.SupportService
{
    public class TermsServiceDTO
    {
        public int Id { get; set; }
        public Guid NameLabelCode { get; set; }
        public virtual List<LanguageResourceDTO> NameLanguageResources { get; set; }

    }
}
