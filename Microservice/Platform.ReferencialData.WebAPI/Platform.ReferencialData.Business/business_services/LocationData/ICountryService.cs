using Platform.GenericRepository;
using Platform.ReferentialData.DataModel.LocationData;
using Platform.ReferentialData.DtoModel.LocationData;
using Platform.ReferentialData.DtoModel.LocationData.Fillter;

namespace Platform.ReferencialData.Business.business_services.LocationData
{
    public interface ICountryService : IGenericService<CountryDTO, CountryEntity>
    {
        PagedList<CountryDTO> GetAll(PagedParameters pagedParameters);
        PagedList<CountryDTO> GetAllActiveData(PagedParameters pagedParameters);
        List<CountryDTO> GetFilteredData(CountryFillter Fillter);

    }
}
