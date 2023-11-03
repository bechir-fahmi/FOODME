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
    public class QuestionAnswerService : IQuestionAnswerService
    {

        private readonly IUnitOfWork<QuestionAnswerEntity> _questionAnswerRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly ILanguageResourceService _languageResourceService;
        private readonly string _cacheKey = CacheKey.QuestionAnswerCaheKey;

        public QuestionAnswerService(IUnitOfWork<QuestionAnswerEntity> questionAnswerRepository, IMapper mapper, ICacheService cache, ILanguageResourceService languageResourceService)
        {
            _questionAnswerRepository = questionAnswerRepository;
            _mapper = mapper;
            _cache = cache;
            _languageResourceService = languageResourceService;
        }

        public void Add(QuestionAnswerDTO refDataDTO, bool updateCache = true)
        {
            AddLanguageResources(refDataDTO);
            QuestionAnswer refDataBM = _mapper.Map<QuestionAnswer>(refDataDTO);
            QuestionAnswerEntity refData = _mapper.Map<QuestionAnswerEntity>(refDataBM);
            _questionAnswerRepository.Repository.Attach(refData);
            _questionAnswerRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }

        public QuestionAnswerDTO Get(int id)
        {
            var DTO = GetAll().FirstOrDefault(x => x.Id == id);
            return DTO;
        }

        public QuestionAnswerDTO Get(Expression<Func<QuestionAnswerEntity, bool>> expression)
        {
            Expression<Func<QuestionAnswerDTO, bool>> exp = _mapper.Map<Expression<Func<QuestionAnswerDTO, bool>>>(expression);
            List<QuestionAnswerDTO> questionList = GetAll();
            QuestionAnswerDTO question = null;
            if (questionList != null && questionList.Count > 0)
            {
                question = ((IQueryable<QuestionAnswerDTO>)questionList).FirstOrDefault(exp);
            }

            return question;
        }

        public PagedList<QuestionAnswerDTO> GetAll(PagedParameters pagedParameters)
        {
            var DtoList = GetAll();

            return PagedList<QuestionAnswerDTO>.ToGenericPagedList(DtoList, pagedParameters);
        }

        public List<QuestionAnswerDTO> GetAll()
        {
            var cachedData = _cache.GetData<List<QuestionAnswerDTO>>(_cacheKey);
            if (cachedData != null)
            {
                return cachedData;
            }
            else
            {
                var kitchenList = _questionAnswerRepository.Repository.GetAll();
                var kitchenBMList = _mapper.Map<IList<QuestionAnswer>>(kitchenList);
                var kitchenDtoList = _mapper.Map<IList<QuestionAnswerDTO>>(kitchenBMList);
                GetLanguageResourceList(kitchenDtoList);
                _cache.SetData(_cacheKey, kitchenDtoList, DateTimeOffset.UtcNow.AddDays(1));
                return (List<QuestionAnswerDTO>)kitchenDtoList;
            }
        }

        public void Remove(int id, bool updateCache = true)
        {
           _questionAnswerRepository.Repository.Delete(id);
            _questionAnswerRepository.Save();
        }

        public void Update(QuestionAnswerDTO refDataDTO, bool updateCache = true)
        {
            UpdateLanguageResources(refDataDTO);
            QuestionAnswer BM = _mapper.Map<QuestionAnswer>(refDataDTO);
            QuestionAnswerEntity refdata = _mapper.Map<QuestionAnswerEntity>(BM);
            _questionAnswerRepository.Repository.Update(refdata);
            _questionAnswerRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }

        #region Language


        private void GetLanguageResourceList(IList<QuestionAnswerDTO> questioniList)
        {
            if (questioniList != null && questioniList.Count > 0)
            {
                foreach (QuestionAnswerDTO question in questioniList)
                {
                    question.QuestionLanguageResources = _languageResourceService.GetLanguageResourcesByCode(question.QuestionLabelCode);
                    question.AnswerLanguageResources = _languageResourceService.GetLanguageResourcesByCode(question.AnswerLabelCode);
                }
            }
        }

        private void AddLanguageResources(QuestionAnswerDTO refDataDTO)
        {
            List<LanguageResourceDTO> languageResources = new();
            if (refDataDTO.QuestionLanguageResources != null && refDataDTO.QuestionLanguageResources.Count > 0)
            {
                languageResources.AddRange(refDataDTO.QuestionLanguageResources);
            }
            if (refDataDTO.AnswerLanguageResources != null && refDataDTO.AnswerLanguageResources.Count > 0)
            {
                languageResources.AddRange(refDataDTO.AnswerLanguageResources);
            }
            _languageResourceService.AddRange(languageResources);
        }

        private void UpdateLanguageResources(QuestionAnswerDTO refDataDTO)
        {
            if (refDataDTO.QuestionLanguageResources != null)
                _languageResourceService.UpdateRange(refDataDTO.QuestionLabelCode, refDataDTO.QuestionLanguageResources);
            if (refDataDTO.AnswerLanguageResources != null)
                _languageResourceService.UpdateRange(refDataDTO.AnswerLabelCode, refDataDTO.AnswerLanguageResources);
        }

        public void Remove(QuestionAnswerDTO refDataDTO, bool updateCache = true)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
