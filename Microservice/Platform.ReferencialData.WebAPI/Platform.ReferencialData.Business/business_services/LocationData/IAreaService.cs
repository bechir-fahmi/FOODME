using Platform.GenericRepository;
using Platform.ReferentialData.DataModel.LocationData;
using Platform.ReferentialData.DtoModel.LocationData;
using Platform.ReferentialData.DtoModel.LocationData.Fillter;

namespace Platform.ReferencialData.Business.business_services.LocationData
{
    public interface IAreaService : IGenericService<AreaDTO, AreaEntity>
    {
        PagedList<AreaDTO> GetAll(PagedParameters pagedParameters);
        PagedList<AreaDTO> GetAllActiveData(PagedParameters pagedParameters);

        public List<AreaDTO> GetAreaByCityId(int cityId);
        public AreaDTO GetAreaByName(string areaIName);
        List<AreaDTO> GetFilteredData(AreaFillter Fillter);

    }
}
