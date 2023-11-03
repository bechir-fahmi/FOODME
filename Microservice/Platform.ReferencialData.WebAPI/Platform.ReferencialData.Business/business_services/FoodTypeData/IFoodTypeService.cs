using Platform.GenericRepository;
using Platform.ReferentialData.DataModel.FoodTypeData;
using Platform.ReferentialData.DtoModel.FoodTypeData;

namespace Platform.ReferencialData.Business.business_services.FoodTypeData
{
    public interface IFoodTypeService : IGenericService<FoodTypeDTO, FoodTypeEntity>
    {
        PagedList<FoodTypeDTO> GetAll(PagedParameters pagedParameters);
        PagedList<FoodTypeDTO> GetAllActiveData(PagedParameters pagedParameters);
        List<FoodTypeDTO> GetFilteredData(FoodTypeFilterDTO Fillter);
        PagedList<FoodTypeDTO> Get(string tag, PagedParameters pagedParameters);


    }
}