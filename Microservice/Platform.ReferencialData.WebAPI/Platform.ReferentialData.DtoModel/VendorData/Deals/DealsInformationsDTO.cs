using Platform.ReferentialData.DtoModel.BrandData;
using ProtoBuf;

namespace Platform.ReferentialData.DtoModel;

public class DealsInformationsDTO
{
    [ProtoMember(1)]
    public string DeliveryZone { get; set; }
    [ProtoMember(2)]
    public string Fees { get; set; }
    [ProtoMember(3)]
    public string TimeEstimation { get; set; }
    [ProtoMember(4)]
    public string brandName { get; set; }
    [ProtoMember(5)]
    public string Rating { get; set; }
    [ProtoMember(6)]
    public string Distance { get; set; }
    [ProtoMember(7)]
    public string MenuName { get; set; }
    [ProtoMember(8)]
    public string MenuImage { get; set; }
    [ProtoMember(9)]
    public string MenuDescription { get; set; }
    [ProtoMember(10)]
    public string MenuPrice { get; set; }
    [ProtoMember(11)]
    public string BrandId { get; set; }
    [ProtoMember(12)]
    public string AggregatorId { get; set; }
    [ProtoMember(13)]
    public string Aggregator { get; set; }

}
