using Platform.GenericRepository;
using Platform.ReferentialData.DataModel.WorkingTime;
using Platform.ReferentialData.DtoModel.WorkingTimeData;
using System.Linq.Expressions;

namespace Platform.ReferencialData.Business.business_services.WorkingTimeData
{
    public interface IExceptionWeekWorkingTimeService : IGenericService<ExceptionWeekWorkingTimeDTO, ExceptionWeekWorkingTimeEntity>
    {
        PagedList<ExceptionWeekWorkingTimeDTO> GetAll(PagedParameters pagedParameters);
        List<ExceptionWeekWorkingTimeDTO> GetAllByExpression(Expression<Func<ExceptionWeekWorkingTimeEntity, bool>> expression);

    }
}
