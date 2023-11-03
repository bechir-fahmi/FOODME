using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Platform.ReferentialData.DataModel.TagData;

namespace Platform.ReferentialData.DataModel.OfferData
{
    [Table("TagOffer")]

    public class TagOfferEntity
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TagId { get; set; }
        [ForeignKey("Offer")]
        public Guid OfferId { get; set; }
        [Required]
        [ForeignKey("OfferId")]
        public virtual OfferEntity Offer { get; set; }
        public string value { get; set; }
    }
}
