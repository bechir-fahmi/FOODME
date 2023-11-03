using Platform.GenericRepository;
using Platform.ReferentialData.DataModel.LocationData;
using Platform.ReferentialData.DtoModel.LocationData;
using Platform.ReferentialData.DtoModel.LocationData.Fillter;

namespace Platform.ReferencialData.Business.business_services.LocationData
{
    public interface IRegionService : IGenericService<RegionDTO, RegionEntity>
    {
        PagedList<RegionDTO> GetAll(PagedParameters pagedParameters);
        PagedList<RegionDTO> GetAllActiveData(PagedParameters pagedParameters);
        List<RegionDTO> GetFilteredData(RegionFillter Fillter);


    }
}
