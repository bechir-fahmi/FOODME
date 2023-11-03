using Platform.ReferencialData.BusinessModel.TagData;
using Platform.ReferentialData.BusinessModel.LanguageData;

namespace Platform.ReferencialData.BusinessModel.LanguageData;

public class TagLanguage
{
    public int TagId { get; set; }
    public int LanguageId { get; set; }
    public virtual Language Language { get; set; }
    public string value { get; set; }
}
