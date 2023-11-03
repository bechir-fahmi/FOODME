//using Platform.ReferencialData.BusinessModel.POS.LocationData;
using Platform.ReferentialData.BusinessModel.LanguageData;
using Platform.ReferentialData.DataModel;
using Platform.ReferentialData.DataModel.LanguageData;
using Platform.Shared.Enum;

namespace Platform.ReferencialData.BusinessModel.LocationData
{
    public class Region
    {
        public int Id { get; set; }
        public Guid NameLabelCode { get; set; }
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
        public Status Status { get; set; } = Status.isInactive;
        public virtual LanguageResourceSet LanguageResourceSet { get; set; }
        public virtual List<TagRegion> Tags { get; set; }
    }
}
