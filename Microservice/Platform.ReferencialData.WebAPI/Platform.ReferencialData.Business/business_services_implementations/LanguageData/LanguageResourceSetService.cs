using AutoMapper;
using Platform.GenericRepository;
using Platform.ReferencialData.Business.business_services;
using Platform.ReferencialData.BusinessModel;
using Platform.ReferencialData.GenericRepository;
using Platform.ReferentialData.DataModel;
using Platform.ReferentialData.DataModel.LanguageData;
using Platform.ReferentialData.DtoModel;
using Platform.Shared.Cache;
using Platform.Shared.Enum;
using System.Linq.Expressions;

namespace Platform.ReferencialData.Business.business_services_implementations.LanguageData
{
    public class LanguageResourceSetService : ILanguageResourceSetService
    {
        private readonly IUnitOfWork<LanguageResourceSetEntity> _languageResourceSetRepository;
        private readonly IUnitOfWork<LanguageResourceEntity> _languageResourceRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly string _cacheKey = CacheKey.LanguageResourceCaheKey;

        public LanguageResourceSetService(IUnitOfWork<LanguageResourceSetEntity> languageResourceSetRepository, IMapper mapper, ICacheService cache)
        {
            _languageResourceSetRepository = languageResourceSetRepository;
            _mapper = mapper;
            _cache = cache;
        }
        public void Add(LanguageResourceSetDTO languageResourceSetDTO, bool updateCache = true)
        {
            LanguageResourceSet languageResouBM = _mapper.Map<LanguageResourceSet>(languageResourceSetDTO);
            LanguageResourceSetEntity languageResource = _mapper.Map<LanguageResourceSetEntity>(languageResouBM);
            _languageResourceSetRepository.Repository.Insert(languageResource);
            _languageResourceSetRepository.Save();
            if (updateCache)
            _cache.RemoveData(_cacheKey);
        }

        public LanguageResourceSetDTO Get(Guid id)
        {
            LanguageResourceSetEntity LanguageResource = _languageResourceSetRepository.Repository.Get(x => x.LanguageResourceSetId == id);
            LanguageResourceSet LanguageResourceBM = _mapper.Map<LanguageResourceSet>(LanguageResource);
            LanguageResourceSetDTO LanguageResourceDto = _mapper.Map<LanguageResourceSetDTO>(LanguageResourceBM);
            return LanguageResourceDto;
        }

        public List<LanguageResourceSetDTO> GetLanguageResourcesByCode(Guid Code)
        {
            List<LanguageResourceSetEntity> LanguageResources = _languageResourceSetRepository.Repository.GetAll().Where(x => x.LanguageResourceSetId == Code).ToList();
            List<LanguageResourceSet> LanguageResourceBM = _mapper.Map<List<LanguageResourceSet>>(LanguageResources);
            List<LanguageResourceSetDTO> LanguageResourcesDto = _mapper.Map<List<LanguageResourceSetDTO>>(LanguageResourceBM);
            return LanguageResourcesDto;
        }

        public LanguageResourceSetDTO Get(Expression<Func<LanguageResourceSetEntity, bool>> expression)
        {
            LanguageResourceSetEntity LanguageResource = _languageResourceSetRepository.Repository.Get(expression);
            LanguageResourceSet LanguageResourceBM = _mapper.Map<LanguageResourceSet>(LanguageResource);
            LanguageResourceSetDTO LanguageResourceDto = _mapper.Map<LanguageResourceSetDTO>(LanguageResourceBM);
            return LanguageResourceDto;
        }
        public List<LanguageResourceSetDTO> GetAll()
        {
            var cachedLanguageResources = _cache.GetData<List<LanguageResourceSetDTO>>(_cacheKey);
            if (cachedLanguageResources != null)
            {
                return cachedLanguageResources;
            }
            else
            {
                var languageResourceList = _languageResourceSetRepository.Repository.GetAll();
                var languageResourceBMList = _mapper.Map<IList<LanguageResourceSet>>(languageResourceList);
                var languageResourceDtoList = _mapper.Map<IList<LanguageResourceSetDTO>>(languageResourceBMList);
                // Cache the data
                _cache.SetData(_cacheKey, languageResourceDtoList, DateTimeOffset.UtcNow.AddDays(1));
                return (List<LanguageResourceSetDTO>)languageResourceDtoList;
            }
        }
        public PagedList<LanguageResourceSetDTO> GetAll(PagedParameters pagedParameters)
        {
            var languageResourceDtoList = GetAll();

            return PagedList<LanguageResourceSetDTO>.ToGenericPagedList(languageResourceDtoList, pagedParameters);
        }

        public void Remove(LanguageResourceSetDTO languageResourceDto, bool updateCache = true)
        {
            LanguageResourceSet LanguageResourceBM = _mapper.Map<LanguageResourceSet>(languageResourceDto);
            LanguageResourceSetEntity LanguageResource = _mapper.Map<LanguageResourceSetEntity>(LanguageResourceBM);
            LanguageResource.Status = Shared.Enum.Status.isDeleted;
            _languageResourceSetRepository.Repository.Update(LanguageResource);
            _languageResourceSetRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }

        /// <summary>
        /// UpdateStaticIntegration language resources
        /// </summary>
        /// <param name="refDataDTO"></param>
        public void Update(LanguageResourceSetDTO refDataDTO, bool updateCache = true)
        {
            LanguageResourceSet LanguageResourceBM = _mapper.Map<LanguageResourceSet>(refDataDTO);
            LanguageResourceSetEntity LanguageResource = _mapper.Map<LanguageResourceSetEntity>(LanguageResourceBM);
            _languageResourceSetRepository.Repository.Update(LanguageResource);
            _languageResourceSetRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }

     
        public void Remove(int refData, bool updateCache = true)
        {
            throw new NotImplementedException();
        }

        public List<LanguageResourceSetDTO> GetImageResourcesByCode(Guid Code)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(List<LanguageResourceSetDTO> languageResources, bool updateCache = true)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(Guid labelCode, List<LanguageResourceSetDTO> languageResources, bool updateCache = true)
        {
            throw new NotImplementedException();
        }

        public LanguageResourceSetDTO Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}