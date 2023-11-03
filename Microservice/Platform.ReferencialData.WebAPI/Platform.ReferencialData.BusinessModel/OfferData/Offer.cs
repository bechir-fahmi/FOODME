using Platform.ReferentialData.DataModel.OfferData;
using Platform.ReferentialData.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Platform.Shared.Enum;

namespace Platform.ReferencialData.BusinessModel.OfferData
{
    public class Offer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public VendorType VendorType { get; set; }
        public string VendorId { get; set; }
        public string VendorName { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string BrandOfferLink { get; set; }
        public string FacebookOfferLink { get; set; }
        public string TwitterOfferLink { get; set; }
        public string InstagramOfferLink { get; set; }
        public Status Status { get; set; } = Status.isActive;
        public virtual LanguageResourceSet LanguageResourceSet { get; set; }
        public virtual List<TagOffer> Tags { get; set; }
    }
}
