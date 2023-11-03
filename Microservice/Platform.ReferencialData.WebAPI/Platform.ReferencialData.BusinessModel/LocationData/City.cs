//using Platform.ReferencialData.BusinessModel.POS.LocationData;

using Platform.ReferentialData.BusinessModel.LanguageData;
using Platform.ReferentialData.DataModel.LanguageData;
using Platform.Shared.Enum;

namespace Platform.ReferencialData.BusinessModel.LocationData
{
    public class City
    {
        public int Id { get; set; }
        public Guid NameLabelCode { get; set; }
        public int RegionId { get; set; }
        public virtual Region Region { get; set; }
        public string CityCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public virtual List<LanguageResource> NameLanguageResources { get; set; }
        public Status Status { get; set; } = Status.isInactive;
        public virtual LanguageResourceSet LanguageResourceSet { get; set; }
        public virtual List<TagCity> Tags { get; set; }
    }
}
