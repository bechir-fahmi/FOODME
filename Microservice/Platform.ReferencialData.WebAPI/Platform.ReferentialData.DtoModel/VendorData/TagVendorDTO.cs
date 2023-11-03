using Platform.ReferentialData.DataModel;
using Platform.ReferentialData.DtoModel.TagData;

namespace Platform.ReferentialData.DtoModel.BrandData;

public class TagVendorDTO
{
    public int TagId { get; set; }
    public Guid VendorId { get; set; }
    public virtual VendorDTO Vendor { get; set; }
    public string value { get; set; }
}
