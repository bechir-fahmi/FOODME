using Platform.GenericRepository;
using Platform.ReferencialData.BusinessModel.WorkingTime;
using Platform.ReferentialData.DataModel.WorkingTime;
using Platform.ReferentialData.DtoModel.WorkingTimeData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Platform.ReferencialData.Business.business_services.WorkingTimeData
{
    public interface IDayWorkingTimeService: IGenericService<DayWorkingTimeDTO, DayWorkingTimeEntity>
    {
        PagedList<DayWorkingTimeDTO> GetAll(PagedParameters pagedParameters);
        List<DayWorkingTimeDTO> GetAllByExpression(Expression<Func<DayWorkingTimeEntity, bool>> expression);

    }

}
