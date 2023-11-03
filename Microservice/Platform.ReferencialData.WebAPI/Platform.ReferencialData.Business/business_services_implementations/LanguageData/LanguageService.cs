using AutoMapper;
using Platform.GenericRepository;
using Platform.ReferencialData.Business.business_services.LanguageData;
using Platform.ReferencialData.GenericRepository;
using Platform.ReferentialData.DataModel;
using Platform.ReferentialData.DataModel.LanguageData;
using Platform.ReferentialData.DtoModel.LanguageData;
using Platform.Shared.Cache;
using Platform.Shared.Enum;
using System.Linq.Expressions;
using Language = Platform.ReferentialData.BusinessModel.LanguageData.Language;

namespace Platform.ReferencialData.Business.business_services_implementations.LanguageData
{
    public class LanguageService : ILanguageService
    {
        private readonly IUnitOfWork<LanguageEntity> _languageRepository;
        private readonly IUnitOfWork<TagLanguageEntity> _taglanguageRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly string _cacheKey = CacheKey.LanguageCaheKey;

        public LanguageService(IUnitOfWork<LanguageEntity> languageRepository,IUnitOfWork<TagLanguageEntity> taglanguageRepository, 
            IMapper mapper,
            ICacheService cache)
        {
            _languageRepository = languageRepository;
            _taglanguageRepository = taglanguageRepository;
            _mapper = mapper;
            _cache = cache;
        }
        public List<LanguageDTO> GetAll()
        {
            var cachedData = _cache.GetData<List<LanguageDTO>>(_cacheKey);
            if (cachedData != null)
            {
                return cachedData;
            }
            else
            {
                var languageEntities = _languageRepository.Repository.GetAll(includes: new List<string>(){ "Tags"});
                var languageBMList = _mapper.Map<List<Language>>(languageEntities);
                var languageDtoList = _mapper.Map<List<LanguageDTO>>(languageBMList);
                _cache.SetData(_cacheKey, languageDtoList, DateTimeOffset.UtcNow.AddDays(1));
                return languageDtoList;
            }
        }

        public PagedList<LanguageDTO> GetAll(PagedParameters pagedParameters)
        {
            var LanguageDtoList = GetAll();
            return PagedList<LanguageDTO>.ToGenericPagedList(LanguageDtoList, pagedParameters);
        }
        public PagedList<LanguageDTO> GetAllActiveData(PagedParameters pagedParameters)
        {
            var LanguageDtoList = GetAll().Where(x => x.Status == Status.isActive).ToList();
            return PagedList<LanguageDTO>.ToGenericPagedList(LanguageDtoList, pagedParameters);
        }

        public LanguageDTO Get(int id)
        {
            var languageList = GetAll().FirstOrDefault(x => x.Id == id);
            return languageList;
        }
        public LanguageDTO GetDefault()
        {
            var language = Get(x=>x.isDefault==true);
            return language;
        }

        public LanguageDTO Get(Expression<Func<LanguageEntity, bool>> expression)
        {
            LanguageEntity languageEntity = _languageRepository.Repository.Get(expression);
            Language languageBM = _mapper.Map<Language>(languageEntity);
            LanguageDTO languageDTO = _mapper.Map<LanguageDTO>(languageBM);
            return languageDTO;
        }
        public void deleteOldTags(int idLanguage, List<TagLanguageEntity> tags)
        {
            var oldTags = _taglanguageRepository.Repository.GetAll(x => x.LanguageId == idLanguage);
            if (oldTags.Count >= 1)
            {
                _taglanguageRepository.Repository.DeleteRange(oldTags);
                _taglanguageRepository.Save();
            }
        }
        public void Update(LanguageDTO language, bool updateCache = true)
        {
            var languageBM = _mapper.Map<Language>(language);
            var languageEntity = _mapper.Map<LanguageEntity>(languageBM);
             deleteOldTags(languageEntity.Id, languageEntity.Tags);
            _languageRepository.Repository.Update(languageEntity);
            _languageRepository.Save();
     
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }
            
        public void Remove(LanguageDTO language, bool updateCache = true)
        {
            var languageBM = _mapper.Map<Language>(language);
            var languageEntity = _mapper.Map<LanguageEntity>(languageBM);
            languageEntity.Status = Shared.Enum.Status.isDeleted;
            _languageRepository.Repository.Update(languageEntity);
            _languageRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }
   
        public void Add(LanguageDTO refDataDTO, bool updateCache = true)
        {
            Language LanguageBM = _mapper.Map<Language>(refDataDTO);
            LanguageEntity Language = _mapper.Map<LanguageEntity>(LanguageBM);
            _languageRepository.Repository.Insert(Language);
            _languageRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }

        public void Remove(int refData, bool updateCache = true)
        {
            throw new NotImplementedException();
        }
    }
}
