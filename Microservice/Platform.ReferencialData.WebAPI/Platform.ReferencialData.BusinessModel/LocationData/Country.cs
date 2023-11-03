using Platform.Shared.Enum;

namespace Platform.ReferencialData.BusinessModel.LocationData
{
    public class Country
    {
        public int Id { get; set; }
        public Guid NameLabelCode { get; set; }
        public string Code { get; set; }
        public string CountryKey { get; set; }
        public Status Status { get; set; } 

    }
}
