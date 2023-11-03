using AutoMapper;
using Platform.GenericRepository;
using Platform.ReferencialData.Business.business_services.LanguageData;
using Platform.ReferencialData.GenericRepository;
using Platform.ReferentialData.BusinessModel.LanguageData;
using Platform.ReferentialData.DataModel.LanguageData;
using Platform.ReferentialData.DtoModel.LanguageData;
using Platform.Shared.Cache;
using Platform.Shared.Enum;
using Platform.Shared.Images;
using Platform.Shared.MicroservicesURLs;
using System.Linq.Expressions;

namespace Platform.ReferencialData.Business.business_services_implementations.LanguageData
{
    public class LanguageResourceService : ILanguageResourceService
    {
        private readonly IUnitOfWork<LanguageResourceEntity> _languageResourceRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly string _cacheKey = CacheKey.LanguageResourceCaheKey;

        public LanguageResourceService(IUnitOfWork<LanguageResourceEntity> languageResouRepository, IMapper mapper, ICacheService cache)
        {
            _languageResourceRepository = languageResouRepository;
            _mapper = mapper;
            _cache = cache;
        }
        public void Add(LanguageResourceDTO languageResourceDTO, bool updateCache = true)
        {
            LanguageResource languageResouBM = _mapper.Map<LanguageResource>(languageResourceDTO);
            LanguageResourceEntity languageResource = _mapper.Map<LanguageResourceEntity>(languageResouBM);
            _languageResourceRepository.Repository.Insert(languageResource);
            _languageResourceRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }
        public void AddRange(List<LanguageResourceDTO> refDataDTO, bool updateCache = true)
        {
            foreach (LanguageResourceDTO langResource in refDataDTO)
            {
                var languageResource = Get(x => x.Code == langResource.Code && x.LanguageKey == langResource.LanguageKey);
                if (languageResource != null)
                {
                    if (languageResource.Value != langResource.Value)
                    {
                        languageResource.Value = langResource.Value;
                    } if (languageResource.Image != langResource.Image)
                    {
                        languageResource.Image = langResource.Image;
                    }
                    Update(languageResource);

                }
                else
                {
                    Add(langResource);
                }
            }
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }
        public LanguageResourceDTO Get(int id)
        {
            LanguageResourceEntity LanguageResource = _languageResourceRepository.Repository.Get(x => x.Id == id);
            LanguageResource LanguageResourceBM = _mapper.Map<LanguageResource>(LanguageResource);
            LanguageResourceDTO LanguageResourceDto = _mapper.Map<LanguageResourceDTO>(LanguageResourceBM);
            return LanguageResourceDto;
        }
        public List<LanguageResourceDTO> GetLanguageResourcesByCode(Guid Code)
        {
            List<LanguageResourceEntity> LanguageResources = _languageResourceRepository.Repository.GetAll().Where(x => x.Code == Code).ToList();
            List<LanguageResource> LanguageResourceBM = _mapper.Map<List<LanguageResource>>(LanguageResources);
            List<LanguageResourceDTO> LanguageResourcesDto = _mapper.Map<List<LanguageResourceDTO>>(LanguageResourceBM);
            return LanguageResourcesDto;
        }
        public List<LanguageResourceDTO> GetImageResourcesByCode(Guid Code)
        {
            List<LanguageResourceEntity> LanguageResources = _languageResourceRepository.Repository.GetAll().Where(x => x.Code == Code).ToList();
            List<LanguageResource> LanguageResourceBM = _mapper.Map<List<LanguageResource>>(LanguageResources);
            foreach (LanguageResource languageResource in LanguageResourceBM)
            {
                languageResource.Value = $"{Microservice.AdminAPI}/Images/{languageResource.Value}";
            }
            List<LanguageResourceDTO> LanguageResourcesDto = _mapper.Map<List<LanguageResourceDTO>>(LanguageResourceBM);
            return LanguageResourcesDto;
        }
        public LanguageResourceDTO Get(Expression<Func<LanguageResourceEntity, bool>> expression)
        {
            LanguageResourceEntity LanguageResource = _languageResourceRepository.Repository.Get(expression);
            LanguageResource LanguageResourceBM = _mapper.Map<LanguageResource>(LanguageResource);
            LanguageResourceDTO LanguageResourceDto = _mapper.Map<LanguageResourceDTO>(LanguageResourceBM);
            return LanguageResourceDto;
        }
        public List<LanguageResourceDTO> GetAll()
        {
            var cachedLanguageResources = _cache.GetData<List<LanguageResourceDTO>>(_cacheKey);
            if (cachedLanguageResources != null)
            {
                return cachedLanguageResources;
            }
            else
            {
                var languageResourceList = _languageResourceRepository.Repository.GetAll();
                var languageResourceBMList = _mapper.Map<IList<LanguageResource>>(languageResourceList);
                var languageResourceDtoList = _mapper.Map<IList<LanguageResourceDTO>>(languageResourceBMList);
                _cache.SetData(_cacheKey, languageResourceDtoList, DateTimeOffset.UtcNow.AddDays(1));
                return (List<LanguageResourceDTO>)languageResourceDtoList;
            }
        }

        public PagedList<LanguageResourceDTO> GetAll(PagedParameters pagedParameters)
        {
            var languageResourceDtoList = GetAll();

            return PagedList<LanguageResourceDTO>.ToGenericPagedList(languageResourceDtoList, pagedParameters);
        }

        public void Remove(LanguageResourceDTO languageResourceDto, bool updateCache = true)
        {
            LanguageResource LanguageResourceBM = _mapper.Map<LanguageResource>(languageResourceDto);
            LanguageResourceEntity LanguageResource = _mapper.Map<LanguageResourceEntity>(LanguageResourceBM);
            LanguageResource.Status = Shared.Enum.Status.isDeleted;
            _languageResourceRepository.Repository.Update(LanguageResource);
            _languageResourceRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }

        public void RemoveRange(List<LanguageResourceDTO> languageResources, bool updateCache = true)
        {
            List<LanguageResource> LanguageResourceBMList = _mapper.Map<List<LanguageResource>>(languageResources);
            List<LanguageResourceEntity> LanguageResourceEntityList = _mapper.Map<List<LanguageResourceEntity>>(LanguageResourceBMList);
            _languageResourceRepository.Repository.DeleteRange(LanguageResourceEntityList);
            _languageResourceRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }

        public void Update(LanguageResourceDTO refDataDTO, bool updateCache = true)
        {
            LanguageResource LanguageResourceBM = _mapper.Map<LanguageResource>(refDataDTO);
            LanguageResourceEntity LanguageResource = _mapper.Map<LanguageResourceEntity>(LanguageResourceBM);
            _languageResourceRepository.Repository.Update(LanguageResource);
            _languageResourceRepository.Save();
            if (updateCache)
            _cache.RemoveData(_cacheKey);
        }

        public void UpdateRange(Guid labelCode, List<LanguageResourceDTO> languageResources, bool updateCache = true)
        {
            foreach (LanguageResourceDTO langResource in languageResources)
            {
                var languageResource = Get(x => x.Code == labelCode && x.LanguageKey == langResource.LanguageKey);
                if (languageResource != null)
                {
                    if (languageResource.Value != langResource.Value)
                    {
                        languageResource.Value = langResource.Value;
                    }
                    if (languageResource.Image!= langResource.Image)
                    {
                        languageResource.Image= langResource.Image;
                    }

                    Update(languageResource);
                }
                else
                {
                    langResource.Code = labelCode;
                    Add(langResource);
                }
            }
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }

        public void Remove(int refData, bool updateCache = true)
        {
            throw new NotImplementedException();
        }
        public void deleteOldLanguageResources(Guid idLanguageResourceSet, List<LanguageResourceEntity> languageRessources)
        {
            var oldLanguageResources = _languageResourceRepository.Repository.GetAll(x => x.LanguageResourceSetId == idLanguageResourceSet);
            if (oldLanguageResources.Count >= 1)
            {
                foreach (var LanguageRessource in oldLanguageResources)
                {
                    if (languageRessources[0].Image != LanguageRessource.Image)
                    {
                        if (!string.IsNullOrEmpty(LanguageRessource.Image))
                        {
                            ImageHelper.DeleteImage(ImageHelper.SaveUrl, LanguageRessource.Image);
                        }
                    }
                    
                }
                _languageResourceRepository.Repository.DeleteRange(oldLanguageResources);
                _languageResourceRepository.Save();
            }
        }
    }
}
