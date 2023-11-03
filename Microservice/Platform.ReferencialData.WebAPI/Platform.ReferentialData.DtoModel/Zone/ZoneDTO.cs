using Platform.ReferentialData.DtoModel.LocationData;
using Platform.Shared.Enum;
using ProtoBuf;

namespace Platform.ReferentialData.DtoModel;

[ProtoContract]
public class ZoneDTO
{
    [ProtoMember(1)]
    public int Id { get; set; }
    [ProtoMember(2)]
    public string Name { get; set; }
    [ProtoMember(3)]
    public int CountryId { get; set; }
    [ProtoMember(4)]
    public int CityId { get; set; }
    [ProtoMember(5)]
    public int RegionId { get; set; }
    [ProtoMember(6)]
    public int AreaId { get; set; }
    [ProtoMember(7)]
    public Status Status { get; set; } = Status.isInactive;
    [ProtoMember(8)]
    public virtual AreaDTO Area { get; set; }
    [ProtoMember(9)]
    public virtual CityDTO City { get; set; }
    [ProtoMember(10)]
    public virtual CountryDTO Country { get; set; }
    [ProtoMember(11)]
    public virtual RegionDTO Region { get; set; }
    [ProtoMember(12)]
    public virtual LanguageResourceSetDTO LanguageResourceSet { get; set; }
    [ProtoMember(13)]
    public virtual List<TagZoneDTO> Tags { get; set; }

}
