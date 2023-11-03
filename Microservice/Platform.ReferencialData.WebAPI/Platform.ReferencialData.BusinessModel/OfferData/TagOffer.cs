using Platform.ReferencialData.BusinessModel.TagData;

namespace Platform.ReferencialData.BusinessModel.OfferData
{
    public class TagOffer
    {
        public int TagId { get; set; }
        public Guid OfferId { get; set; }
        public virtual Offer Offer { get; set; }
        public string value { get; set; }
    }
}
