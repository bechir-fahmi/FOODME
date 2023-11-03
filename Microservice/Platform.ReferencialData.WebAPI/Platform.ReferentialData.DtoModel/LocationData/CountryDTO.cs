using Platform.Shared.Enum;
using ProtoBuf;

namespace Platform.ReferentialData.DtoModel.LocationData
{
    [ProtoContract]
    public class CountryDTO
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]
        public Guid NameLabelCode { get; set; }
        [ProtoMember(3)]
        public string Code { get; set; }
        [ProtoMember(4)]
        public string CountryKey { get; set; }
        public Status Status { get; set; }

    }
}
