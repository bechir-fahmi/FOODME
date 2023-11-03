using ProtoBuf;

namespace Platform.ReferentialData.DtoModel;

public class VendorDataDTO
{
    #region Data
    [ProtoMember(1)]
    public virtual  ICollection<VendorDeliveryModeDTO> VendorDeliverys { get; set; }
    [ProtoMember(2)]
    public virtual ICollection<VendorFoodTypeDTO> VendorFoodTypes { get; set; }
    [ProtoMember(3)]
    public virtual ICollection<VendorKitchenTypeDTO> VendorKitchenTypes { get; set; }
    [ProtoMember(4)]
    public virtual ICollection<VendorMealTimingDTO> VendorMealTimings { get; set; }
    [ProtoMember(5)]
    public virtual ICollection<VendorMealTypeDTO> VendorMealTypes { get; set; }
    [ProtoMember(6)]
    public string Description { get; set; }
    [ProtoMember(7)]
    public string OtherDescription { get; set; }
    #endregion


}
