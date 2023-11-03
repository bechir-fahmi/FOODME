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
    public class TermsServiceService : ITermsServiceService
    {
        private readonly IUnitOfWork<TermsServiceEntity> _termsServiceRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly ILanguageResourceService _languageResourceService;
        private readonly string _cacheKey = CacheKey.TermsServiceCaheKey;

        public TermsServiceService(IUnitOfWork<TermsServiceEntity> termsServiceRepository, IMapper mapper, ICacheService cache, ILanguageResourceService languageResourceService)
        {
            _termsServiceRepository = termsServiceRepository;
            _mapper = mapper;
            _cache = cache;
            _languageResourceService = languageResourceService;
        }

        public void Add(TermsServiceDTO refDataDTO, bool updateCache = true)
        {
            AddLanguageResources(refDataDTO);
            TermsService refDataBM = _mapper.Map<TermsService>(refDataDTO);
            TermsServiceEntity refData = _mapper.Map<TermsServiceEntity>(refDataBM);
            _termsServiceRepository.Repository.Attach(refData);
            _termsServiceRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }

        public TermsServiceDTO Get(int id)
        {
            var DTO = GetAll().FirstOrDefault(x => x.Id == id);
            return DTO;
        }

        public TermsServiceDTO Get(Expression<Func<TermsServiceEntity, bool>> expression)
        {
            Expression<Func<TermsServiceDTO, bool>> exp = _mapper.Map<Expression<Func<TermsServiceDTO, bool>>>(expression);
            List<TermsServiceDTO> reflist = GetAll();
            TermsServiceDTO refdata = null;
            if (reflist != null && reflist.Count > 0)
            {
                refdata = ((IQueryable<TermsServiceDTO>)reflist).FirstOrDefault(exp);
            }

            return refdata;
        }

        public PagedList<TermsServiceDTO> GetAll(PagedParameters pagedParameters)
        {
            var DtoList = GetAll();

            return PagedList<TermsServiceDTO>.ToGenericPagedList(DtoList, pagedParameters);
        }

        public List<TermsServiceDTO> GetAll()
        {
            var cachedData = _cache.GetData<List<TermsServiceDTO>>(_cacheKey);
            if (cachedData != null)
            {
                return cachedData;
            }
            else
            {
                var refList = _termsServiceRepository.Repository.GetAll();
                var refBMList = _mapper.Map<IList<TermsService>>(refList);
                var refDtoList = _mapper.Map<IList<TermsServiceDTO>>(refBMList);
                GetLanguageResourceList(refDtoList);
                _cache.SetData(_cacheKey, refDtoList, DateTimeOffset.UtcNow.AddDays(1));
                return (List<TermsServiceDTO>)refDtoList;
            }
        }

        public void Remove(int id, bool updateCache = true)
        {
            _termsServiceRepository.Repository.Delete(id);
            _termsServiceRepository.Save();
        }

        public void Update(TermsServiceDTO refDataDTO, bool updateCache = true)
        {
            UpdateLanguageResources(refDataDTO);
            TermsService BM = _mapper.Map<TermsService>(refDataDTO);
            TermsServiceEntity refdata = _mapper.Map<TermsServiceEntity>(BM);
            _termsServiceRepository.Repository.Update(refdata);
            _termsServiceRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }

        #region Language
        private void GetLanguageResourceList(IList<TermsServiceDTO> refList)
        {
            if (refList != null && refList.Count > 0)
            {
                foreach (TermsServiceDTO refdata in refList)
                {
                    refdata.NameLanguageResources = _languageResourceService.GetLanguageResourcesByCode(refdata.NameLabelCode);
                }
            }
        }

        private void AddLanguageResources(TermsServiceDTO refDataDTO)
        {
            List<LanguageResourceDTO> languageResources = new();
            if (refDataDTO.NameLanguageResources != null && refDataDTO.NameLanguageResources.Count > 0)
            {
                languageResources.AddRange(refDataDTO.NameLanguageResources);
            }

            _languageResourceService.AddRange(languageResources);
        }

        private void UpdateLanguageResources(TermsServiceDTO refDataDTO)
        {
            if (refDataDTO.NameLanguageResources != null)
                _languageResourceService.UpdateRange(refDataDTO.NameLabelCode, refDataDTO.NameLanguageResources);
        }

        public void Remove(TermsServiceDTO refDataDTO, bool updateCache = true)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
