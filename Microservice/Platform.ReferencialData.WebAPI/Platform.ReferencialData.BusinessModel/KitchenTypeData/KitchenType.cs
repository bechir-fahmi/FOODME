using Platform.Shared.Enum;
using Platform.ReferentialData.BusinessModel.LanguageData;
using Platform.ReferentialData.DataModel;

namespace Platform.ReferencialData.BusinessModel.KitchenTypeData
{
    public class KitchenType
    {
        public int Id { get; set; }

        public Guid NameLabelCode { get; set; }

        public Guid ImageLabelCode { get; set; }
        public string Name { get; set; }

        public virtual LanguageResourceSet LanguageResourceSet { get; set; }
        public Status Status { get; set; } = Status.isInactive;
        public virtual List<TagKitchenType> Tags { get; set; }

    }
}
