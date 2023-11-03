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
    public interface IBrandActionSummaryService
    {
        List<BrandActionSummaryView> getBrandActionSummaryViews();
        Task<ResponseDTO> AddOrUpdateBrandActionSummary(List<BrandActionSummaryView> BrandActionSummaryViews);
        List<BrandActionSummaryDTO> GetAllBrandActionSummary();
        List<BrandActionSummaryTotalUsersDTO> GetAllBrandActionSummaryTotalUsers();
    }
}
