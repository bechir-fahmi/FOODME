using Platform.GenericRepository;
using Platform.ReferentialData.DataModel.AgeRangeData;
using Platform.ReferentialData.DtoModel.AgeRangeData;

namespace Platform.ReferencialData.Business.business_services.AgeRangeData
{
    public interface IAgeRangeService : IGenericService<AgeRangeDTO, AgeRangeEntity>
    {
        PagedList<AgeRangeDTO> GetAll(PagedParameters pagedParameters);
        PagedList<AgeRangeDTO> GetAllActiveData(PagedParameters pagedParameters);


    }
}