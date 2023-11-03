using AutoMapper;
using Platform.GenericRepository;
using Platform.ReferencialData.Business.business_services.LanguageData;
using Platform.ReferencialData.Business.business_services.WorkingTimeData;
using Platform.ReferencialData.BusinessModel.WorkingTime;
using Platform.ReferencialData.GenericRepository;
using Platform.ReferentialData.DataModel.WorkingTime;
using Platform.ReferentialData.DtoModel.LanguageData;
using Platform.ReferentialData.DtoModel.WorkingTimeData;
using Platform.Shared.Cache;
using System.Linq.Expressions;

namespace Platform.ReferencialData.Business.business_services_implementations.WorkingTimeData
{
    public class ExceptionWeekWorkingTimeService : IExceptionWeekWorkingTimeService
    {
        private readonly IUnitOfWork<ExceptionWeekWorkingTimeEntity> _exceptionWeekWorkingTimeRepository;
        private readonly IExceptionDayWorkingTimeService _exceptionDayWorkingTimeRepository;
        private readonly ILanguageResourceService _languageResourceService;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly string _cacheKey = CacheKey.ExceptionWeekWorkingTimeCaheKey;

        public ExceptionWeekWorkingTimeService(ILanguageResourceService languageResourceService, IMapper mapper, ICacheService cache, IUnitOfWork<ExceptionWeekWorkingTimeEntity> exceptionWeekWorkingTimeRepository)
        {
            _languageResourceService = languageResourceService;
            _mapper = mapper;
            _cache = cache;
            _exceptionWeekWorkingTimeRepository = exceptionWeekWorkingTimeRepository;
        }

        public void Add(ExceptionWeekWorkingTimeDTO refDataDTO, bool updateCache = true)
        {
            AddweekLanguageResources(refDataDTO);
            ExceptionWeekWorkingTime refdataBM = _mapper.Map<ExceptionWeekWorkingTime>(refDataDTO);
            ExceptionWeekWorkingTimeEntity refdata = _mapper.Map<ExceptionWeekWorkingTimeEntity>(refdataBM);
            _exceptionWeekWorkingTimeRepository.Repository.Attach(refdata);
            _exceptionWeekWorkingTimeRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }

        public ExceptionWeekWorkingTimeDTO Get(int id)
        {
            var refDataDTO = GetAll().FirstOrDefault(x => x.Id == id);
            return refDataDTO;
        }

        public ExceptionWeekWorkingTimeDTO Get(Expression<Func<ExceptionWeekWorkingTimeEntity, bool>> expression)
        {
            Expression<Func<ExceptionWeekWorkingTimeDTO, bool>> exp = _mapper.Map<Expression<Func<ExceptionWeekWorkingTimeDTO, bool>>>(expression);
            List<ExceptionWeekWorkingTimeDTO> weekworkingList = GetAll();
            ExceptionWeekWorkingTimeDTO weekWorking = null;
            if (weekworkingList != null && weekworkingList.Count > 0)
            {
                weekWorking = ((IQueryable<ExceptionWeekWorkingTimeDTO>)weekworkingList).FirstOrDefault(exp);
            }

            return weekWorking;
        }

        public PagedList<ExceptionWeekWorkingTimeDTO> GetAll(PagedParameters pagedParameters)
        {
            var refDtoList = GetAll();

            return PagedList<ExceptionWeekWorkingTimeDTO>.ToGenericPagedList(refDtoList, pagedParameters);
        }

        public List<ExceptionWeekWorkingTimeDTO> GetAll()
        {
            var cachedData = _cache.GetData<List<ExceptionWeekWorkingTimeDTO>>(_cacheKey);
            if (cachedData != null)
            {
                return cachedData;
            }
            else
            {
                var weekList = _exceptionWeekWorkingTimeRepository.Repository.GetAll();
                var weekBMList = _mapper.Map<IList<ExceptionWeekWorkingTime>>(weekList);
                var weekDtoList = _mapper.Map<IList<ExceptionWeekWorkingTimeDTO>>(weekBMList);
                GetLanguageResourceList(weekDtoList);
                GetExceptionDayWorking(weekDtoList);
                _cache.SetData(_cacheKey, weekDtoList, DateTimeOffset.UtcNow.AddDays(1));
                return (List<ExceptionWeekWorkingTimeDTO>)weekDtoList;
            }
        }

        public void Remove(int id, bool updateCache = true)
        {
            _exceptionWeekWorkingTimeRepository.Repository.Delete(id);
            _exceptionWeekWorkingTimeRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }

        public void Update(ExceptionWeekWorkingTimeDTO refDataDTO, bool updateCache = true)
        {
            UpdateweekLanguageResources(refDataDTO);
            ExceptionWeekWorkingTime dayBM = _mapper.Map<ExceptionWeekWorkingTime>(refDataDTO);
            ExceptionWeekWorkingTimeEntity day = _mapper.Map<ExceptionWeekWorkingTimeEntity>(dayBM);
            _exceptionWeekWorkingTimeRepository.Repository.Update(day);
            _exceptionWeekWorkingTimeRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }


        private void AddweekLanguageResources(ExceptionWeekWorkingTimeDTO refDataDTO)
        {
            List<LanguageResourceDTO> languageResources = new();
            if (refDataDTO.NameLanguageResources != null && refDataDTO.NameLanguageResources.Count > 0)
            {
                languageResources.AddRange(refDataDTO.NameLanguageResources);

            }
            if (refDataDTO.DescriptionLanguageResources != null && refDataDTO.DescriptionLanguageResources.Count > 0)
            {
                languageResources.AddRange(refDataDTO.DescriptionLanguageResources);

            }
           
            _languageResourceService.AddRange(languageResources);
        }

        private void UpdateweekLanguageResources(ExceptionWeekWorkingTimeDTO weekDTO)
        {
            if (weekDTO.NameLanguageResources != null)
                _languageResourceService.UpdateRange(weekDTO.NameLabelCode, weekDTO.NameLanguageResources);
            if (weekDTO.DescriptionLanguageResources != null)
                _languageResourceService.UpdateRange(weekDTO.DescriptionLabelCode, weekDTO.DescriptionLanguageResources);
        }

        private void GetLanguageResourceList(IList<ExceptionWeekWorkingTimeDTO> weekList)
        {
            if (weekList != null && weekList.Count > 0)
            {
                foreach (ExceptionWeekWorkingTimeDTO week in weekList)
                {
                    week.NameLanguageResources = _languageResourceService.GetLanguageResourcesByCode(week.NameLabelCode);
                    week.DescriptionLanguageResources = _languageResourceService.GetLanguageResourcesByCode(week.DescriptionLabelCode);
                }
            }
        }

        private void GetExceptionDayWorking(IList<ExceptionWeekWorkingTimeDTO> ExceptionDayWorkingList)
        {
            if (ExceptionDayWorkingList != null && ExceptionDayWorkingList.Count > 0)
            {
                foreach (ExceptionWeekWorkingTimeDTO exceptionWeek in ExceptionDayWorkingList)
                {
                    exceptionWeek.ExceptionalDailyWorkingTimes = _exceptionDayWorkingTimeRepository.GetAllByExpression(x => x.Id == exceptionWeek.Id);
                }
            }
        }

        public List<ExceptionWeekWorkingTimeDTO> GetAllByExpression(Expression<Func<ExceptionWeekWorkingTimeEntity, bool>> expression)
        {
            IList<ExceptionWeekWorkingTimeEntity> dayModeEntity = _exceptionWeekWorkingTimeRepository.Repository.GetAll(expression);
            IList<ExceptionWeekWorkingTime> dayMode = _mapper.Map<IList<ExceptionWeekWorkingTime>>(dayModeEntity);
            List<ExceptionWeekWorkingTimeDTO> dayModeDto = (List<ExceptionWeekWorkingTimeDTO>)_mapper.Map<IList<ExceptionWeekWorkingTimeDTO>>(dayMode);
            return dayModeDto;
        }

        public void Remove(ExceptionWeekWorkingTimeDTO refDataDTO, bool updateCache = true)
        {
            throw new NotImplementedException();
        }
    }
}
