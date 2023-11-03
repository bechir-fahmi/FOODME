using Platform.ReferentialData.DtoModel.FoodTypeData;
using Platform.ReferentialData.DtoModel.MealData;
using ProtoBuf;

namespace Platform.ReferentialData.DtoModel;

[ProtoContract]
public class VendorMealTimingDTO
{
    [ProtoMember(1)]
    public int MealTimingId { get; set; }
    [ProtoMember(2)]
    public Guid VendorId { get; set; }
    [ProtoMember(3)]
    public virtual MealTimingDTO MealTiming { get; set; }
}
