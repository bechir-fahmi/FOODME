using Platform.ReferencialData.BusinessModel;
using ProtoBuf;

namespace Platform.ReferentialData.DtoModel;

[ProtoContract]
public class StaticIntegrationDTO
{
    [ProtoMember(1)]
    public Guid StaticIntegrationId { get; set; }
    [ProtoMember(2)]
    public Guid VendorId { get; set; }
    [ProtoMember(3)]
    public int ZoneId { get; set; }
    [ProtoMember(4)]
    public string Fees { get; set; }
    [ProtoMember(5)]
    public string TimeEstimation { get; set; }
    [ProtoMember(6)]
    public virtual VendorDTO VendorModel { get; set; }
    [ProtoMember(7)]
    public virtual ZoneDTO Zone { get; set; }
}
