using Platform.GenericRepository;
using Platform.ReferentialData.DataModel.MealData;
using Platform.ReferentialData.DtoModel.MealData;


namespace Platform.ReferencialData.Business.business_services.MealData
{
    public interface IMealTypeService : IGenericService<MealTypeDTO, MealTypeEntity>
    {
        PagedList<MealTypeDTO> GetAll(PagedParameters pagedParameters);
        PagedList<MealTypeDTO> GetAllActiveData(PagedParameters pagedParameters);

        PagedList<MealTypeDTO> Get(string tag,PagedParameters pagedParameters);
        List<MealTypeDTO> GetFilteredData(MealTypeFilterDTO Fillter);

    }
}
