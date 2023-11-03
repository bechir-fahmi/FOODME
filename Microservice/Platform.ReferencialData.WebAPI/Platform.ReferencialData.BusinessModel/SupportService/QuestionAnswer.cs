using Platform.ReferentialData.BusinessModel.LanguageData;

namespace Platform.ReferencialData.BusinessModel.SupportService
{
    public class QuestionAnswer
    {
        public int Id { get; set; }
        public Guid QuestionLabelCode { get; set; }
        public Guid AnswerLabelCode { get; set; }

        public int SuportCategoryId { get; set; }
        public virtual List<LanguageResource> QuestionLanguageResources { get; set; }
        public virtual List<LanguageResource> AnswerLanguageResources { get; set; }
    }
}
