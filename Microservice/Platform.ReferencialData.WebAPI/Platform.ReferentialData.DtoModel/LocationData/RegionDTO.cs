using Platform.ReferentialData.DtoModel.LanguageData;
using Platform.Shared.Enum;
using ProtoBuf;

namespace Platform.ReferentialData.DtoModel.LocationData
{
    [ProtoContract]
    public class RegionDTO
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]
        public Guid NameLabelCode { get; set; }
        [ProtoMember(3)]
        public int CountryId { get; set; }
        [ProtoMember(4)]
        public virtual CountryDTO Country { get; set; }
        [ProtoMember(5)]
        public Status Status { get; set; } = Status.isInactive;
        [ProtoMember(6)]
        public virtual LanguageResourceSetDTO LanguageResourceSet { get; set; }
        [ProtoMember(7)]
        public virtual List<TagRegionDTO> Tags { get; set; }

    }
}
