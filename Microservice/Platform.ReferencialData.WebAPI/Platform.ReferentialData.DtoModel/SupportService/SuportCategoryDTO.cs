using Platform.ReferentialData.DtoModel.LanguageData;
using Platform.Shared.Enum;

namespace Platform.ReferentialData.DtoModel.SupportService
{
    public class SuportCategoryDTO
    {
        public int Id { get; set; }

        public HelpSupportType HelpSupportType { get; set; }

        public Guid NameLabelCode { get; set; }

        public Guid Image { get; set; }
        public virtual List<LanguageResourceDTO> NameLanguageResources { get; set; }
        public virtual List<LanguageResourceDTO> ImageFileResources { get; set; }


    }
}
