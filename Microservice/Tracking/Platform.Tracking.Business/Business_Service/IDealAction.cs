using Platform.GenericRepository;
using Platform.Tracking.DtoModel.GetDeals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Tracking.Business.Business_Service
{
    public interface IDealAction
    {
        DealsActionDTO AddDealAction(DealsActionDTO dealsActionDTO, bool updateCache = true);
        PagedList<DealsActionDTO> GetDealsActionPagedList(PagedParameters pagedParameters);
        List<DealsActionDTO> GetDealsActions();
    }
}
