using Platform.ReferentialData.DtoModel.FoodTypeData;
using ProtoBuf;

namespace Platform.ReferentialData.DtoModel;

[ProtoContract]
public class VendorFoodTypeDTO
{
    [ProtoMember(1)]
    public int FoodTypeId { get; set; }
    [ProtoMember(2)]
    public Guid VendorId { get; set; }
    [ProtoMember(3)]
    public virtual FoodTypeDTO FoodType { get; set;}
}
