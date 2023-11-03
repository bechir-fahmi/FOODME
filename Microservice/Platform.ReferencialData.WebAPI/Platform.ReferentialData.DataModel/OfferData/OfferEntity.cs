using Platform.ReferentialData.DataModel.MealData;
using Platform.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.ReferentialData.DataModel.OfferData
{
    public class OfferEntity : ReferentialDataBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public VendorType VendorType { get; set; }
        public string VendorId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string BrandOfferLink { get; set; }
        public string FacebookOfferLink { get; set; }
        public string TwitterOfferLink { get; set; }
        public string InstagramOfferLink { get; set; }
        public virtual LanguageResourceSetEntity LanguageResourceSet { get; set; }
        public virtual List<TagOfferEntity> Tags { get; set; }
    }

}
