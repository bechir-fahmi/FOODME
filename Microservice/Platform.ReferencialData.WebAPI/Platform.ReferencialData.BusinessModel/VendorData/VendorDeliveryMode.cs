using Platform.ReferencialData.BusinessModel.DeliveryModeData;

namespace Platform.ReferencialData.BusinessModel;

public class VendorDeliveryMode
{
    public int DeliveryModeId { get; set; }
    public Guid APIId { get; set; }
    public virtual DeliveryMode DeliveryMode { get; set; }
}
