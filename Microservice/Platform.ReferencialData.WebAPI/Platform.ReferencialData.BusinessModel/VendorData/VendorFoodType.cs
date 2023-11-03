using Platform.ReferencialData.BusinessModel.FoodTypeData;

namespace Platform.ReferencialData.BusinessModel;

public class VendorFoodType
{
    public int FoodTypeId { get; set; }
    public Guid VendorId { get; set; }
    public virtual FoodType FoodType { get; set; }
    public virtual Vendor Api { get; set; }
}
