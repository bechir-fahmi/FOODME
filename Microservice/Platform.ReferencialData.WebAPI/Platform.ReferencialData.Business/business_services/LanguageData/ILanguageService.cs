using Platform.GenericRepository;
using Platform.ReferentialData.DataModel.LanguageData;
using Platform.ReferentialData.DtoModel.LanguageData;

namespace Platform.ReferencialData.Business.business_services.LanguageData
{
    public interface ILanguageService : IGenericService<LanguageDTO, LanguageEntity>
    {
        PagedList<LanguageDTO> GetAll(PagedParameters pagedParameters);
        PagedList<LanguageDTO> GetAllActiveData(PagedParameters pagedParameters);

    }
}
