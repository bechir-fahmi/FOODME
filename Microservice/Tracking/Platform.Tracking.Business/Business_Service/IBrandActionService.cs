using Platform.GenericRepository;
using Platform.Shared.Enum;
using Platform.Tracking.DataModel.BrandAction;
using Platform.Tracking.DtoModel.BrandAction;
using Platform.Tracking.DtoModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Tracking.Business.Business_Service
{
    public interface IBrandActionService
    {
        BrandActionDTO AddBrandAction(BrandActionDTO brandActionDTO, TypeOfAction typeOfAction, bool updateCache = true);

        List<BrandActionDTO> GetAllBrandActions();

        PagedList<BrandActionDTO> GetAll(PagedParameters pagedParameters);

        Dictionary<string, int> GetAllActionsByBrand(TypeOfAction typeOfAction);
        Dictionary<Guid, int> GetAllActionsByBrandId(TypeOfAction typeOfAction);

        Dictionary<string, int> GetAllActionsByPeriodOfTime(TypeOfAction typeOfAction, DateTime startTime, DateTime endTime);

        Dictionary<string, int> GetAllBrandActionsByPeriodOfTime(Guid BrandModelId, TypeOfAction typeOfAction, DateTime startTime, DateTime endTime);

    }
}
