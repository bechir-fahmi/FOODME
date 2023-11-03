using Platform.ReferencialData.BusinessModel.TagData;

namespace Platform.ReferencialData.BusinessModel.MealData
{
    public class TagMealTiming
    {
        public int TagId { get; set; }
        public int MealTimingId { get; set; }
        public virtual MealTiming MealTiming { get; set; }
        public string value { get; set; }
    }
}
