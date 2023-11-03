using Platform.GenericRepository;
using Platform.ReferentialData.DataModel.MealData;
using Platform.ReferentialData.DtoModel.MealData;

namespace Platform.ReferencialData.Business.business_services.MealData
{
    public interface IMealTimingService : IGenericService<MealTimingDTO, MealTimingEntity>
    {
        PagedList<MealTimingDTO> GetAll(PagedParameters pagedParameters);
        PagedList<MealTimingDTO> GetAllActiveData(PagedParameters pagedParameters);

        List<MealTimingDTO> GetFilteredData(MealTimingFilterDTO Fillter);
        PagedList<MealTimingDTO> Get(string tag, PagedParameters pagedParameters);

    }
}
