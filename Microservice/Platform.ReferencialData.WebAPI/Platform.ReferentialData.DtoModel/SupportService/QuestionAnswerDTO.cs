using Platform.ReferentialData.DtoModel.LanguageData;

namespace Platform.ReferentialData.DtoModel.SupportService
{
    public class QuestionAnswerDTO
    {
        public int Id { get; set; }
        public Guid QuestionLabelCode { get; set; }
        public Guid AnswerLabelCode { get; set; }

        public int SuportCategoryId { get; set; }
        public virtual List<LanguageResourceDTO> QuestionLanguageResources { get; set; }
        public virtual List<LanguageResourceDTO> AnswerLanguageResources { get; set; }

    }
}
