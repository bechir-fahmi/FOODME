using ProtoBuf;

namespace Platform.ReferentialData.DtoModel;
[ProtoContract]
public class BrandMenuDTO
{
    [ProtoMember(1)]
    public Guid BrandMenuId { get; set; }
    [ProtoMember(2)]
    public string Name { get; set; }
    [ProtoMember(3)]
    public string Image { get; set; }
    [ProtoMember(4)]
    public string Description { get; set; }
    [ProtoMember(5)]
    public double Price { get; set; }

}
