using Platform.ReferentialData.DtoModel.BrandData;
using ProtoBuf;
using Platform.Shared.Enum;

namespace Platform.ReferentialData.DtoModel;

[ProtoContract]
public class VendorDTO
{
    #region General Informations
    [ProtoMember(1)]
    public Guid VendorId { get; set; }
    [ProtoMember(2)]
    public string Name { get; set; }
    [ProtoMember(3)]
    public string Logo { get; set; }
    [ProtoMember(4)]
    public string AndLink { get; set; }
    [ProtoMember(5)]
    public string IOSLink { get; set; }
    [ProtoMember(6)]
    public string WebLink { get; set; }
    public int? AdminScore { get; set; } = 0;
    #endregion

    #region Data
    #region Kitchen,Food and Meal
    [ProtoMember(7)]
    public virtual ICollection<VendorDeliveryModeDTO> VendorDeliverys { get; set; }
    [ProtoMember(8)]
    public virtual ICollection<VendorFoodTypeDTO> VendorFoodTypes { get; set; }
    [ProtoMember(9)]
    public virtual ICollection<VendorKitchenTypeDTO> VendorKitchenTypes { get; set; }
    [ProtoMember(10)]
    public virtual ICollection<VendorMealTimingDTO> VendorMealTimings { get; set; }
    [ProtoMember(11)]
    public virtual ICollection<VendorMealTypeDTO> VendorMealTypes { get; set; }
    #endregion
    [ProtoMember(12)]
    public string Description { get; set; }
    [ProtoMember(13)]
    public string OtherDescription { get; set; }
    [ProtoMember(14)]
    public bool ShowDescription { get; set; }

    #endregion

    #region Zone
    [ProtoMember(15)]
    public virtual ICollection<VendorDeliveryZoneDTO> Zones { get; set; }
    #endregion

    #region Static Integration
    [ProtoMember(16)]
    public virtual ICollection<StaticIntegrationDTO> StaticIntegrations { get; set; }
    #endregion

    #region Dynamic Integration
    [ProtoMember(17)]
    public virtual ICollection<DynamicIntegrationDTO> DynamicIntegrations { get; set; }
    #endregion

    #region Tags
    [ProtoMember(18)]
    public virtual List<TagVendorDTO> Tags { get; set; }
    #endregion
    
    [ProtoMember(19)]
    public Status Status { get; set; }
    public VendorType Type { get; set; }
}
