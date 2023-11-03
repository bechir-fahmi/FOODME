using Platform.ReferencialData.BusinessModel.TagData;

namespace Platform.ReferencialData.BusinessModel.MealData
{
    public class TagMealType
    {
        public int TagId { get; set; }
        public int MealTypeId { get; set; }
        public virtual MealType MealType { get; set; }
        public string value { get; set; }
    }
}
