using Platform.ReferencialData.BusinessModel.KitchenTypeData;
using Platform.ReferentialData.DataModel;
using Platform.ReferentialData.DtoModel.BrandData;
using Platform.ReferentialData.DtoModel.LanguageData;
using Platform.Shared.Enum;

namespace Platform.ReferentialData.DtoModel.KitchenTypeData
{
    public class KitchenTypeDTO
    {
        public int Id { get; set; }

        public Guid NameLabelCode { get; set; }

        public Guid ImageLabelCode { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; } = Status.isInactive;
        public virtual LanguageResourceSetDTO LanguageResourceSet { get; set; }
        public virtual List<TagKitchenTypeDTO> Tags { get; set; }
     


    }
}
