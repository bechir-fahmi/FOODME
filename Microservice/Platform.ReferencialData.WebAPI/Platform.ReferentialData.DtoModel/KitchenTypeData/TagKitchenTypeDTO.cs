using Platform.ReferencialData.BusinessModel.KitchenTypeData;
using Platform.ReferentialData.DtoModel.TagData;

namespace Platform.ReferentialData.DtoModel.KitchenTypeData
{
    public class TagKitchenTypeDTO
    {
        public int TagId { get; set; }
        public int KitchenTypeId { get; set; }
        public virtual KitchenTypeDTO KitchenType { get; set; }
        public string value { get; set; }
    }
}
