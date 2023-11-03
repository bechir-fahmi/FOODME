using Platform.ReferencialData.BusinessModel.TagData;

namespace Platform.ReferencialData.BusinessModel.KitchenTypeData
{
    public class TagKitchenType
    {
        public int TagId { get; set; }
        public int KitchenTypeId { get; set; }
        public virtual KitchenType KitchenType { get; set; }
        public string value { get; set; }
    }
}
