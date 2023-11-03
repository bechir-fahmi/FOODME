using Platform.GenericRepository;
using Platform.ReferentialData.DataModel.WorkingTime;
using Platform.ReferentialData.DtoModel.WorkingTimeData;
using System.Linq.Expressions;

namespace Platform.ReferencialData.Business.business_services.WorkingTimeData
{
    public interface IExceptionDayWorkingTimeService:IGenericService<ExceptionDayWorkingTimeDTO, ExceptionDayWorkingTimeEntity>
    {
        PagedList<ExceptionDayWorkingTimeDTO> GetAll(PagedParameters pagedParameters);
        List<ExceptionDayWorkingTimeDTO> GetAllByExpression(Expression<Func<ExceptionDayWorkingTimeEntity, bool>> expression);

    }
}
