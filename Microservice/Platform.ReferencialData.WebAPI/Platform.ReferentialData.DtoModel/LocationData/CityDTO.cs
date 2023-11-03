using Platform.ReferentialData.DtoModel.LanguageData;
using Platform.Shared.Enum;
using ProtoBuf;

namespace Platform.ReferentialData.DtoModel.LocationData
{
    [ProtoContract]
    public class CityDTO
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]
        public Guid NameLabelCode { get; set; }
        [ProtoMember(3)]
        public int RegionId { get; set; }
        [ProtoMember(4)]
        public string CityCode { get; set; }
        [ProtoMember(5)]
        public double Latitude { get; set; }
        [ProtoMember(6)]
        public double Longitude { get; set; }
        [ProtoMember(7)]
        public Status Status { get; set; } = Status.isInactive;
        [ProtoMember(8)]
        public virtual LanguageResourceSetDTO LanguageResourceSet { get; set; }
        [ProtoMember(9)]
        public virtual List<TagCityDTO> Tags { get; set; }
        [ProtoMember(10)]
        public virtual RegionDTO region { get; set; }
    }
 
}
