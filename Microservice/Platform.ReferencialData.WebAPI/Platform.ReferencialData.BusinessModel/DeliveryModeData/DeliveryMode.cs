using Platform.ReferentialData.BusinessModel.LanguageData;
using Platform.ReferentialData.DataModel.LanguageData;
using Platform.Shared.Enum;

namespace Platform.ReferencialData.BusinessModel.DeliveryModeData
{
    public class DeliveryMode
    {
        public int Id { get; set; }

        public Guid NameLabelCode { get; set; }

        public Guid ImageLabelCode { get; set; }
        public string Name { get; set; }

        public virtual List<LanguageResource> nameImageLanguageRessource { get; set; }
        public Status Status { get; set; }
    }
}
