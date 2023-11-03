using Platform.ReferentialData.DtoModel.FoodTypeData;
using Platform.ReferentialData.DtoModel.MealData;
using ProtoBuf;

namespace Platform.ReferentialData.DtoModel;

[ProtoContract]
public class VendorMealTypeDTO
{
    [ProtoMember(1)]
    public int MealTypeId { get; set; }
    [ProtoMember(2)]
    public Guid VendorId { get; set; }
    [ProtoMember(3)]
    public virtual MealTypeDTO MealType { get; set; }
}
