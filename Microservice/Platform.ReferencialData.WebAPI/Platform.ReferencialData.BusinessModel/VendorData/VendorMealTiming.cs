
using Platform.ReferencialData.BusinessModel.MealData;

namespace Platform.ReferencialData.BusinessModel;

public class VendorMealTiming
{
    public int MealTimingId { get; set; }
    public Guid VendorId { get; set; }
    public virtual MealTiming MealTiming { get; set; }
}
