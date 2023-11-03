using Platform.GenericRepository;
using Platform.ReferentialData.DataModel.WorkingTime;
using Platform.ReferentialData.DtoModel.WorkingTimeData;

namespace Platform.ReferencialData.Business.business_services.WorkingTimeData
{

    public interface IClenderWorkingTimeService : IGenericService<ClenderWorkingTimeDTO, ClenderWorkingTimeEntity>
    {
        PagedList<ClenderWorkingTimeDTO> GetAll(PagedParameters pagedParameters);

    }

}
