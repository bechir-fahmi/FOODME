using Platform.ReferencialData.BusinessModel.LocationData;
using Platform.ReferentialData.DtoModel.LanguageData;
using Platform.Shared.Enum;
using ProtoBuf;

namespace Platform.ReferentialData.DtoModel.LocationData
{
    [ProtoContract]
    public class AreaDTO
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]
        public Guid NameLabelCode { get; set; }
        [ProtoMember(3)]
        public int CityId { get; set; }
        [ProtoMember(4)]
        public string AreaName { get; set; }
        [ProtoMember(5)]
        public virtual CityDTO City { get; set; }
        [ProtoMember(6)]
        public decimal Latitude { get; set; }
        [ProtoMember(7)]
        public decimal Longitude { get; set; }
        public Status Status { get; set; } = Status.isInactive;

    }
}
