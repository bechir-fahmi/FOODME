using Platform.Shared.Enum;

namespace Platform.FoodMe.Admin.ViewModels.ViewModels.AgeRangeData
{
    public class AgeRangeVM
    {
        public int Id { get; set; }
        public int MaxAge { get; set; }
        public int MinAge { get; set; }
        public Status Status { get; set; } = Status.isInactive;

    }
}
