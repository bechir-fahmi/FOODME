using Platform.ReferencialData.BusinessModel.TagData;

namespace Platform.ReferencialData.BusinessModel.BrandData
{
    public class TagVendor
    {
        public int TagId { get; set; }
        public Guid VendorId { get; set; }
        public virtual Vendor Vendor { get; set; }
        public string value { get; set; }
    }
}
