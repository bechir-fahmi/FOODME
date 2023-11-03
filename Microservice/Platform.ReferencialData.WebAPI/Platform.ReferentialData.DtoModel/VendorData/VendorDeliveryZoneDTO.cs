using ProtoBuf;

namespace Platform.ReferentialData.DtoModel;

[ProtoContract]
public class VendorDeliveryZoneDTO
{
    [ProtoMember(1)]
    public int ZoneId { get; set; }
    [ProtoMember(2)]
    public Guid VendorId { get; set; }
    [ProtoMember(3)]
    public virtual ZoneDTO Zone { get; set; }
}
