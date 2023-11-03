using Platform.Shared.Enum;

namespace Platform.ReferencialData.BusinessModel.LocationData
{
    public class Area
    {
        public int Id { get; set; }
        public Guid NameLabelCode { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
        public string AreaName { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public Status Status { get; set; } 

    }
}
