using Platform.GenericRepository;
using Platform.ReferentialData.DataModel.LocationData;
using Platform.ReferentialData.DtoModel.LocationData;
using Platform.ReferentialData.DtoModel.LocationData.Fillter;

namespace Platform.ReferencialData.Business.business_services.LocationData
{
    public interface ICityService : IGenericService<CityDTO, CityEntity>
    {
        PagedList<CityDTO> GetAll(PagedParameters pagedParameters);
        PagedList<CityDTO> GetAllActiveData(PagedParameters pagedParameters);

        public List<CityDTO> GetCityByRegionId(int regionId);
        List<CityDTO> GetFilteredData(CityFillter Fillter);

    }
}
