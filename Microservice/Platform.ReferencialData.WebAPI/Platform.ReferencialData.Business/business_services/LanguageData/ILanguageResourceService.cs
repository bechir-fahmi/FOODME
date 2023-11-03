using Platform.GenericRepository;
using Platform.ReferentialData.DataModel.LanguageData;
using Platform.ReferentialData.DtoModel.LanguageData;

namespace Platform.ReferencialData.Business.business_services.LanguageData
{
    public interface ILanguageResourceService : IGenericService<LanguageResourceDTO, LanguageResourceEntity>
    {
        public PagedList<LanguageResourceDTO> GetAll(PagedParameters pagedParameters);

        public void AddRange(List<LanguageResourceDTO> refDataDTO, bool updateCache = true);

        public List<LanguageResourceDTO> GetLanguageResourcesByCode(Guid Code);



        public List<LanguageResourceDTO> GetImageResourcesByCode(Guid Code);

        public void RemoveRange(List<LanguageResourceDTO> languageResources, bool updateCache = true);
        /// <summary>
        /// UpdateStaticIntegration the language resources list for a specific labelCode 
        /// by the values giving in language resources list
        /// </summary>
        /// <param name="labelCode"></param>
        /// <param name="languageResources"></param>
        void UpdateRange(Guid labelCode, List<LanguageResourceDTO> languageResources, bool updateCache = true);
        void deleteOldLanguageResources(Guid idLanguageResourceSet, List<LanguageResourceEntity> languageRessources);


    }
}
