using ProtoBuf;
namespace Platform.ReferentialData.DtoModel;

[ProtoContract]
public class VendorDeliveryModeDTO
{
    [ProtoMember(1)]
    public int DeliveryModeId { get; set; }
    [ProtoMember(2)]
    public Guid VendorId { get; set; }
}
