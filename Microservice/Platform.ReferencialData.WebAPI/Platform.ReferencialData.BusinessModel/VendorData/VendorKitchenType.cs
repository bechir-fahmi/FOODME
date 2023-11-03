using Platform.ReferencialData.BusinessModel.KitchenTypeData;

namespace Platform.ReferencialData.BusinessModel;

public class VendorKitchenType
{
    public int KitchenTypeId { get; set; }
    public Guid VendorId { get; set; }
    public virtual KitchenType KitchenType { get; set; }
}
