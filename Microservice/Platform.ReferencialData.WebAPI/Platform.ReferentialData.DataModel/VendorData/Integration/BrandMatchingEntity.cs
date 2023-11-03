using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Platform.ReferentialData.DataModel.BrandData.Integration;
[Table("BrandMatching")]
public class BrandMatchingEntity
{
    [Required]
    [ForeignKey("VendorId")]
    public Guid AggregatorId { get; set; } 
    [Required]
    [ForeignKey("VendorId")]
    public int DistantBrandId { get; set; }
    public Guid LocalBrandId { get; set; }
}

