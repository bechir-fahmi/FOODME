using Platform.ReferentialData.DtoModel.LanguageData;
using Platform.Shared.Enum;

namespace Platform.ReferentialData.DtoModel.DeliveryModeData
{
    public class DeliveryModeDTO
    {
        public int Id { get; set; }
        public Guid NameLabelCode { get; set; }
        public Guid ImageLabelCode { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
        public virtual LanguageResourceSetDTO LanguageResourceSet { get; set; }

    }
}
