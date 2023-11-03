using Platform.ReferentialData.DataModel.LanguageData;
using Platform.Shared.Enum;
using ProtoBuf;

namespace Platform.ReferentialData.DtoModel.LanguageData
{
    [ProtoContract()]
    public class LanguageResourceDTO
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]
        public Guid Code { get; set; }
        [ProtoMember(3)]
        public string Value { get; set; }
        [ProtoMember(4)]
        public string Image { get; set; }
        [ProtoMember(5)]
        public LanguageKey LanguageKey { get; set; }
        [ProtoMember(6)]
        public virtual LanguageResourceSetDTO LanguageResourceSet { get; set; }
        [ProtoMember(7)]
        public Guid LanguageResourceSetId { get; set; }
        [ProtoMember(8)]
        public virtual LanguageDTO Language { get; set; }

    }
}
