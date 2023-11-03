using Platform.ReferentialData.DataModel.TagData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Platform.ReferentialData.DataModel.BrandData;

[Table("TagVendor")]
public class TagVendorEntity
{
    [Required]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TagId { get; set; }
    public Guid VendorId { get; set; }
    [Required]
    [ForeignKey("VendorId")]
    public virtual VendorEntity Vendor { get; set; }
    public string value { get; set; }

}
