using Platform.ReferencialData.BusinessModel.MealData;

namespace Platform.ReferencialData.BusinessModel;

public class VendorMealType
{
    public int MealTypeId { get; set; }
    public Guid VendorId { get; set; }
    public virtual MealType MealType { get; set; }
}
