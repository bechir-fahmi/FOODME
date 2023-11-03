using Platform.ReferentialData.BusinessModel.LanguageData;

namespace Platform.ReferencialData.BusinessModel;

public class LanguageResourceSet
{
    public Guid LanguageResourceSetId { get; set; }
    public virtual List<LanguageResource> LanguageRessource { get; set; }
}
