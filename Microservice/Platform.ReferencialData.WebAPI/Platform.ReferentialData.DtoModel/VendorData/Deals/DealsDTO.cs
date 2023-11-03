using Platform.Shared.Enum;
using ProtoBuf;

namespace Platform.ReferentialData.DtoModel;

[ProtoContract]
public class DealsDTO
{
    [ProtoMember(1)]
    public Guid Id { get; set; }
    [ProtoMember(2)]
    public string Name { get; set; }
    [ProtoMember(3)]
    public string Logo { get; set; }
    [ProtoMember(4)]
    public string AndLink { get; set; }
    public string IOSLink { get; set; }
    public string WebLink { get; set; }
    public string Description { get; set; }
    public string OtherDescription { get; set; }
    public string Aggregator { get; set; }
    public VendorType Type { get; set; }


    #region Kitchen,Food and Meal
    [ProtoMember(5)]
    public ICollection<VendorDeliveryModeDTO> VendorDeliverys { get; set; }
    [ProtoMember(6)]
    public ICollection<VendorFoodTypeDTO> VendorFoodTypes { get; set; }
    [ProtoMember(7)]
    public ICollection<VendorKitchenTypeDTO> VendorKitchenTypes { get; set; }
    [ProtoMember(8)]
    public ICollection<VendorMealTimingDTO> VendorMealTimings { get; set; }
    [ProtoMember(9)]
    public ICollection<VendorMealTypeDTO> VendorMealTypes { get; set; }
    [ProtoMember(10)]
    public string AggregatorId { get; set; }
    [ProtoMember(11)]
    public string DeliveryZone { get; set; }
    [ProtoMember(12)]
    public string Fees { get; set; }
    [ProtoMember(13)]
    public string MinimumAmmount { get; set; }
    [ProtoMember(14)]
    public string TimeEstimationFrom { get; set; }
    [ProtoMember(15)]
    public string TimeEstimationTo { get; set; }
    [ProtoMember(16)]
    public string brandName { get; set; }
    [ProtoMember(17)]
    public string Rating { get; set; }
    [ProtoMember(18)]
    public string Distance { get; set; }
    [ProtoMember(19)]
    public string MenuName { get; set; }
    [ProtoMember(20)]
    public string MenuImage { get; set; }
    [ProtoMember(21)]
    public string MenuDescription { get; set; }
    [ProtoMember(22)]
    public string MenuPrice { get; set; }
    [ProtoMember(23)]
    public string BrandId { get; set; }

    [ProtoMember(24)]
    public ICollection<DealsDTO> DealsInfos { get; set; }
    #endregion

}
