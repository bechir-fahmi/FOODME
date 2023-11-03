using Platform.Shared.Enum;

namespace Platform.ReferentialData.DataModel
{
    public class ReferentialDataBase
    {
        public Status Status { get; set; } = Status.isInactive;
        public DateTime? CreationTime { get; set; }
        public int? CreatorUserId { get; set; }

        public int? DeleterUserId { get; set; }

        public DateTime? DeletionTime { get; set; }

        public int? LastModifierUserId { get; set; }

        public DateTime? LastModificationTime { get; set; }
    }
}
