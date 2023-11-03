using Platform.ReferentialData.DataModel.OfferData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Platform.ReferentialData.DtoModel.TagData;

namespace Platform.ReferentialData.DtoModel.OfferData
{
    public class TagOfferDTO
    {
        public int TagId { get; set; }
        public Guid OfferId { get; set; }
        public virtual OfferDTO Offer { get; set; }
        public string value { get; set; }
    }
}
