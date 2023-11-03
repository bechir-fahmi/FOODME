using Platform.ReferentialData.DataModel.LocationData;
using Platform.ReferentialData.DtoModel.LocationData;
using System.Linq.Expressions;

namespace Platform.ReferencialData.Business.business_services.LocationData
{
    public interface ICoordinateService: IGenericService<CoordinateDTO, CoordinateEntity>
    {
        List<CoordinateDTO> GetAllByExpression(Expression<Func<CoordinateEntity, bool>> expression);

    }
}
