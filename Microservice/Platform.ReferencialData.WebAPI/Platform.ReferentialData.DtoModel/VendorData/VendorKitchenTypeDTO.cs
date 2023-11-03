using Platform.ReferentialData.DataModel.KitchenTypeData;
using Platform.ReferentialData.DtoModel.FoodTypeData;
using Platform.ReferentialData.DtoModel.KitchenTypeData;
using ProtoBuf;

namespace Platform.ReferentialData.DtoModel;

[ProtoContract]
public class VendorKitchenTypeDTO
{
    [ProtoMember(1)]
    public int KitchenTypeId { get; set; }
    [ProtoMember(2)]
    public Guid VendorId { get; set; }
    [ProtoMember(3)]
    public virtual KitchenTypeDTO KitchenType { get; set; }
}
