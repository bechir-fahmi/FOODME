using Platform.Shared.Enum;
using ProtoBuf;

namespace Platform.ReferentialData.DtoModel.LocationData
{
    [ProtoContract]
    public class LocationDataDTO
    {
        [ProtoMember(1)]
        public virtual List<CountryDTO> CountryList { get; set; }
        [ProtoMember(2)]
        public virtual List<RegionDTO> RegionList { get; set; }
        [ProtoMember(3)]
        public virtual List<CityDTO> CityList { get; set; }
        [ProtoMember(4)]
        public virtual List<AreaDTO> AreaList { get; set; }
        [ProtoMember(5)]
        public POSType POSType { get; set; }
    }
}
