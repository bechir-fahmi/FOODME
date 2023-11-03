namespace Platform.ReferentialData.DtoModel;

public class AddressToSearchDTO
    {
        public string CountryName { get; set; }
        public string RegionName { get; set; }
        public string ZoneName { get; set; }
        public string CityCode { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public bool IsUpdated { get; set; }
    }

