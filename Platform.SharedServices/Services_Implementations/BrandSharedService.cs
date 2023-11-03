
using Platform.ReferentialData.DtoModel;
using Platform.Shared.HttpHelpers;
using Platform.Shared.MicroservicesURLs;
using Platform.SharedServices.Services;

namespace Platform.SharedServices.Services_Implementations
{
    public class BrandSharedService : IBrandSharedService
    {
        private readonly IHelper<BrandDTO, BrandDTO, BrandDTO> _helper;
        
        public BrandSharedService(
            IHelper<BrandDTO, BrandDTO, BrandDTO> helper)
        {
            _helper = helper;
        }

        public List<BrandDTO> GetAllBrands()
        {
            string endPoint = $"{Microservice.RefData}/Api/GetAllBrands";
            var brandDTOs = _helper.GetData(endPoint);
            return (List<BrandDTO>)brandDTOs;
        }

        public BrandDTO GetBrandById(int brandId)
        {
            string endPoint = $"{Microservice.RefData}/Api/GetBrandById/{brandId}";
            var brandDTO = _helper.Get(endPoint);
            return brandDTO;
        }

    }
}
