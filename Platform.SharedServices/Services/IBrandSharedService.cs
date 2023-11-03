using Platform.ReferentialData.DtoModel;

namespace Platform.SharedServices.Services
{
    public interface IBrandSharedService
    {
        public List<BrandDTO> GetAllBrands();
        public BrandDTO GetBrandById(int brandId);
    }
}
