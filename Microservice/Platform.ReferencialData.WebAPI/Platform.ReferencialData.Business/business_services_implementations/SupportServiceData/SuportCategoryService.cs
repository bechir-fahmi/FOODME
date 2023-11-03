using AutoMapper;
using Platform.GenericRepository;
using Platform.ReferencialData.Business.business_services.LanguageData;
using Platform.ReferencialData.Business.business_services.SupportServiceData;
using Platform.ReferencialData.BusinessModel.SupportService;
using Platform.ReferencialData.GenericRepository;
using Platform.ReferentialData.DataModel.SupportService;
using Platform.ReferentialData.DtoModel.LanguageData;
using Platform.ReferentialData.DtoModel.SupportService;
using Platform.Shared.Cache;
using System.Linq.Expressions;

namespace Platform.ReferencialData.Business.business_services_implementations.SupportServiceData
{
    public class SuportCategoryService : ISuportCategoryService
    {
        private readonly IUnitOfWork<SuportCategoryEntity> _suportCategoryRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly ILanguageResourceService _languageResourceService;
        private readonly string _cacheKey = CacheKey.SuportCategoryCaheKey;

        public SuportCategoryService(IUnitOfWork<SuportCategoryEntity> suportCategoryRepository, IMapper mapper, ICacheService cache, ILanguageResourceService languageResourceService)
        {
            _suportCategoryRepository = suportCategoryRepository;
            _mapper = mapper;
            _cache = cache;
            _languageResourceService = languageResourceService;
        }

        public void Add(SuportCategoryDTO refDataDTO, bool updateCache = true)
        {
            AddLanguageResources(refDataDTO);
            SuportCategory refDataBM = _mapper.Map<SuportCategory>(refDataDTO);
            SuportCategoryEntity refData = _mapper.Map<SuportCategoryEntity>(refDataBM);
            _suportCategoryRepository.Repository.Attach(refData);
            _suportCategoryRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }

        public SuportCategoryDTO Get(int id)
        {
            var DTO = GetAll().FirstOrDefault(x => x.Id == id);
            return DTO;
        }

        public SuportCategoryDTO Get(Expression<Func<SuportCategoryEntity, bool>> expression)
        {
            Expression<Func<SuportCategoryDTO, bool>> exp = _mapper.Map<Expression<Func<SuportCategoryDTO, bool>>>(expression);
            List<SuportCategoryDTO> reflist = GetAll();
            SuportCategoryDTO refdata = null;
            if (reflist != null && reflist.Count > 0)
            {
                refdata = ((IQueryable<SuportCategoryDTO>)reflist).FirstOrDefault(exp);
            }

            return refdata;
        }

        public PagedList<SuportCategoryDTO> GetAll(PagedParameters pagedParameters)
        {
            var DtoList = GetAll();

            return PagedList<SuportCategoryDTO>.ToGenericPagedList(DtoList, pagedParameters);
        }

        public List<SuportCategoryDTO> GetAll()
        {
            var cachedData = _cache.GetData<List<SuportCategoryDTO>>(_cacheKey);
            if (cachedData != null)
            {
                return cachedData;
            }
            else
            {
                var refList = _suportCategoryRepository.Repository.GetAll();
                var refBMList = _mapper.Map<IList<SuportCategory>>(refList);
                var refDtoList = _mapper.Map<IList<SuportCategoryDTO>>(refBMList);
                GetLanguageResourceList(refDtoList);
                _cache.SetData(_cacheKey, refDtoList, DateTimeOffset.UtcNow.AddDays(1));
                return (List<SuportCategoryDTO>)refDtoList;
            }
        }

        public void Remove(int id, bool updateCache = true)
        {
            _suportCategoryRepository.Repository.Delete(id);
            _suportCategoryRepository.Save();
        }

        public void Update(SuportCategoryDTO refDataDTO, bool updateCache = true)
        {
            UpdateLanguageResources(refDataDTO);
            SuportCategory BM = _mapper.Map<SuportCategory>(refDataDTO);
            SuportCategoryEntity refdata = _mapper.Map<SuportCategoryEntity>(BM);
            _suportCategoryRepository.Repository.Update(refdata);
            _suportCategoryRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }



        #region Language
        private void GetLanguageResourceList(IList<SuportCategoryDTO> refList)
        {
            if (refList != null && refList.Count > 0)
            {
                foreach (SuportCategoryDTO refdata in refList)
                {
                    refdata.NameLanguageResources = _languageResourceService.GetLanguageResourcesByCode(refdata.NameLabelCode);
                    refdata.ImageFileResources = _languageResourceService.GetImageResourcesByCode(refdata.Image);

                }
            }
        }

        private void AddLanguageResources(SuportCategoryDTO refDataDTO)
        {
            List<LanguageResourceDTO> languageResources = new();
            if (refDataDTO.NameLanguageResources != null && refDataDTO.NameLanguageResources.Count > 0)
            {
                languageResources.AddRange(refDataDTO.NameLanguageResources);
            }
            if (refDataDTO.ImageFileResources != null && refDataDTO.ImageFileResources.Count > 0)
            {
                languageResources.AddRange(refDataDTO.ImageFileResources);
            }

            _languageResourceService.AddRange(languageResources);
        }

        private void UpdateLanguageResources(SuportCategoryDTO refDataDTO)
        {
            if (refDataDTO.NameLanguageResources != null)
                _languageResourceService.UpdateRange(refDataDTO.NameLabelCode, refDataDTO.NameLanguageResources);
            if (refDataDTO.ImageFileResources != null)
                _languageResourceService.UpdateRange(refDataDTO.Image, refDataDTO.ImageFileResources);

        }

        public void Remove(SuportCategoryDTO refDataDTO, bool updateCache = true)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
