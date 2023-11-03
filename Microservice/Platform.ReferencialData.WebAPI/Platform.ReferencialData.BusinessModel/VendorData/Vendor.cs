using Platform.Shared.Enum;

namespace Platform.ReferencialData.BusinessModel;

public class Vendor
{
    public Guid VendorId { get; set; }
    public string Name { get; set; }
    public string Logo { get; set; }
    public string AndLink { get; set; }
    public string IOSLink { get; set; }
    public string WebLink { get; set; }
    public int? AdminScore { get; set; } = 0;
    public virtual ICollection<VendorDeliveryMode> VendorDeliverys { get; set; }
    public virtual ICollection<VendorFoodType> VendorFoodTypes { get; set; }
    public virtual ICollection<VendorKitchenType> VendorKitchenTypes { get; set; }
    public virtual ICollection<VendorMealTiming> VendorMealTimings { get; set; }
    public virtual ICollection<VendorMealType> VendorMealTypes { get; set; }
    public string Description { get; set; }
    public string OtherDescription { get; set; } 
    public virtual ICollection<VendorDeliveryZone> Zones { get; set; }
    public virtual ICollection<StaticIntegration> StaticIntegrations { get; set; }
    public virtual ICollection<DynamicIntegration> DynamicIntegrations { get; set; }
    public Status Status { get; set; } = Status.isInactive;
    public VendorType Type { get; set; }
}