using Platform.Shared.Enum;

namespace Platform.ReferentialData.DtoModel.AgeRangeData
{
    public class AgeRangeDTO
    {
        public int Id { get; set; }
        public int MaxAge { get; set; }
        public int MinAge { get; set; }
        public Status Status { get; set; } = Status.isInactive;
    }
}
