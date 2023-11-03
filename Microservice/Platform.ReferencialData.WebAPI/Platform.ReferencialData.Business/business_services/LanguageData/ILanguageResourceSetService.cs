using Platform.GenericRepository;
using Platform.ReferentialData.DataModel;
using Platform.ReferentialData.DtoModel;

namespace Platform.ReferencialData.Business.business_services;

public interface ILanguageResourceSetService : IGenericService<LanguageResourceSetDTO, LanguageResourceSetEntity>
{
    public PagedList<LanguageResourceSetDTO> GetAll(PagedParameters pagedParameters);


    public List<LanguageResourceSetDTO> GetLanguageResourcesByCode(Guid Code);



    public List<LanguageResourceSetDTO> GetImageResourcesByCode(Guid Code);

    public void RemoveRange(List<LanguageResourceSetDTO> languageResources, bool updateCache = true);
    /// <summary>
    /// UpdateStaticIntegration the language resources list for a specific labelCode 
    /// by the values giving in language resources list
    /// </summary>
    /// <param name="labelCode"></param>
    /// <param name="languageResources"></param>
    void UpdateRange(Guid labelCode, List<LanguageResourceSetDTO> languageResources, bool updateCache = true);

}

