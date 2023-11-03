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


    public class ClenderWorkingTimeService : IClenderWorkingTimeService
    {
        private readonly IUnitOfWork<ClenderWorkingTimeEntity> _weeklyWorkingTimeWithExceptionsRepository;
        private readonly IUnitOfWork<ExceptionDayWorkingTimeEntity> _exceptionDayWorkingRepository;
        private readonly ILanguageResourceService _languageResourceService;
        private readonly IDayWorkingTimeService _dayWorkingTimeRepository;
        private readonly IExceptionWeekWorkingTimeService _exceptionWeekWorkingTimeRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly string _cacheKey = CacheKey.ClenderWorkingTimeCacheKey;

        public ClenderWorkingTimeService(IUnitOfWork<ClenderWorkingTimeEntity> weeklyWorkingTimeWithExceptionsRepository, IMapper mapper, ICacheService cache)
        {
            _weeklyWorkingTimeWithExceptionsRepository = weeklyWorkingTimeWithExceptionsRepository;
            _mapper = mapper;
            _cache = cache;
        }

        public List<ClenderWorkingTimeDTO> GetAll()
        {
           /* var cachedData = _cache.GetData<List<ClenderWorkingTimeDTO>>(_cacheKey);
            if (cachedData != null)
            {
                return cachedData;
            }

            else
            {*/
                var weeklyWorkingTimeWithExceptionsList = _weeklyWorkingTimeWithExceptionsRepository.Repository.GetAll(includes: new List<string> {"UsualDailyWorkingTimes", "ExceptionalWeekyWorkingTimes", "AppliedRestaurant", "ExceptionalWeekyWorkingTimes.ExceptionalDailyWorkingTimes"});
               // var weeklyWorkingTimeWithExceptionsList = _weeklyWorkingTimeWithExceptionsRepository.Repository.GetAll();
                var weeklyWorkingTimeWithExceptionsBM = _mapper.Map<List<ClenderWorkingTime>>(weeklyWorkingTimeWithExceptionsList);
                var weeklyWorkingTimeWithExceptionsDTOList = _mapper.Map<List<ClenderWorkingTimeDTO>>(weeklyWorkingTimeWithExceptionsBM);
                GetLanguageResourceList(weeklyWorkingTimeWithExceptionsDTOList);
                //GetUsualDailyWorking(weeklyWorkingTimeWithExceptionsDTOList);
                //GetExceptionalWeekyWorking(weeklyWorkingTimeWithExceptionsDTOList);
                //GetAppliedRestaurant(weeklyWorkingTimeWithExceptionsDTOList);
              //  _cache.SetData(_cacheKey, weeklyWorkingTimeWithExceptionsDTOList, DateTimeOffset.UtcNow.AddDays(1));
                return (List<ClenderWorkingTimeDTO>)weeklyWorkingTimeWithExceptionsDTOList;
           // }
        }





        public PagedList<ClenderWorkingTimeDTO> GetAll(PagedParameters pagedParameters)
        {
            var weeklyWorkingTimeWithExceptionsDTOList = GetAll();

            return PagedList<ClenderWorkingTimeDTO>.ToGenericPagedList(weeklyWorkingTimeWithExceptionsDTOList, pagedParameters);
        }

        public ClenderWorkingTimeDTO Get(int weeklyWorkingTimeWithExceptionsId)
        {
            var weeklyWorkingTimeWithExceptionsDTOList = GetAll();
            return weeklyWorkingTimeWithExceptionsDTOList.FirstOrDefault(x => x.Id == weeklyWorkingTimeWithExceptionsId);
        }

        public ClenderWorkingTimeDTO Get(Expression<Func<ClenderWorkingTimeEntity, bool>> expression)
        {
            var weeklyWorkingTimeWithExceptionsEntity = _weeklyWorkingTimeWithExceptionsRepository.Repository.Get(expression);
            var weeklyWorkingTimeWithExceptionsDTO = _mapper.Map<ClenderWorkingTimeDTO>(weeklyWorkingTimeWithExceptionsEntity);
            return weeklyWorkingTimeWithExceptionsDTO;
        }

        public void Add(ClenderWorkingTimeDTO weeklyWorkingTimeWithExceptions, bool updateCache = true)
        {
            //AddweekLanguageResources(weeklyWorkingTimeWithExceptions);
            ClenderWorkingTime weeklyWorkingTime = _mapper.Map<ClenderWorkingTime>(weeklyWorkingTimeWithExceptions);
            ClenderWorkingTimeEntity weeklyWorkingTimeEntity = _mapper.Map<ClenderWorkingTimeEntity>(weeklyWorkingTime);
            _weeklyWorkingTimeWithExceptionsRepository.Repository.Attach(weeklyWorkingTimeEntity);
            _weeklyWorkingTimeWithExceptionsRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);


        }



        public void Update(ClenderWorkingTimeDTO refDataDTO, bool updateCache = true)
        {
            //UpdateweekLanguageResources(refDataDTO);
            ClenderWorkingTime weeklyWorkingBM = _mapper.Map<ClenderWorkingTime>(refDataDTO);
            ClenderWorkingTimeEntity weeklyWorkingTime = _mapper.Map<ClenderWorkingTimeEntity>(weeklyWorkingBM);
            _weeklyWorkingTimeWithExceptionsRepository.Repository.Update(weeklyWorkingTime);
            _weeklyWorkingTimeWithExceptionsRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }

        public void Remove(int Id, bool updateCache = true)
        {
           
            _weeklyWorkingTimeWithExceptionsRepository.Repository.Delete(Id);
            _weeklyWorkingTimeWithExceptionsRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }

        private void AddweekLanguageResources(ClenderWorkingTimeDTO refDataDTO)
        {
            List<LanguageResourceDTO> languageResources = new();
            if (refDataDTO.NameLanguageResources != null && refDataDTO.NameLanguageResources.Count > 0)
            {
                languageResources.AddRange(refDataDTO.NameLanguageResources);

            }
           

            _languageResourceService.AddRange(languageResources);
        }

        private void UpdateweekLanguageResources(ClenderWorkingTimeDTO weekDTO)
        {
            if (weekDTO.NameLanguageResources != null)
                _languageResourceService.UpdateRange(weekDTO.NameLabelCode, weekDTO.NameLanguageResources);
           
        }

        private void GetLanguageResourceList(IList<ClenderWorkingTimeDTO> weekList)
        {
            if (weekList != null && weekList.Count > 0)
            {
                foreach (ClenderWorkingTimeDTO week in weekList)
                {
                    week.NameLanguageResources = _languageResourceService.GetLanguageResourcesByCode(week.NameLabelCode);
                }
            }
        }

        private void GetUsualDailyWorking(IList<ClenderWorkingTimeDTO> refList)
        {
            if (refList != null && refList.Count > 0)
            {
                foreach (ClenderWorkingTimeDTO clender in refList)
                {
                    clender.UsualDailyWorkingTimes = _dayWorkingTimeRepository.GetAllByExpression(x => x.Id == clender.Id);
                }
            }
        }
        private void GetExceptionalWeekyWorking(IList<ClenderWorkingTimeDTO> refList)
        {
            if (refList != null && refList.Count > 0)
            {
                foreach (ClenderWorkingTimeDTO clender in refList)
                {
                    clender.ExceptionalWeekyWorkingTimes = _exceptionWeekWorkingTimeRepository.GetAllByExpression(x => x.Id == clender.Id);
                }
            }
        }


        public void Remove(ClenderWorkingTimeDTO refDataDTO, bool updateCache = true)
        {
            throw new NotImplementedException();
        }

        #region Option
        #region 2List Dayes And Exception
        //private void GetDaysWork(IList<WeeklyWorkingTimeWithExceptionsDTO> GetDaysWork)
        //{
        //    if (GetDaysWork != null && GetDaysWork.Count > 0)
        //    {
        //        foreach (WeeklyWorkingTimeWithExceptionsDTO WeeklyWorkingTime in GetDaysWork)
        //        {

        //            WeeklyWorkingTime.UsualDailyWorkingTimes = GetDailyWorkingTimes(x => x.VendorId == WeeklyWorkingTime.VendorId);
        //        }
        //    }
        //}

        //private void GetDaysException(IList<WeeklyWorkingTimeWithExceptionsDTO> GetDaysWork)
        //{
        //    if (GetDaysWork != null && GetDaysWork.Count > 0)
        //    {
        //        foreach (WeeklyWorkingTimeWithExceptionsDTO dayWorkingTime in GetDaysWork)
        //        {
        //            dayWorkingTime.ExceptionalDailyWorkingTimes = GetExceptionDayWorkingTimes(x => x.VendorId == dayWorkingTime.VendorId);
        //        }
        //    }
        //}
        //public List<DayWorkingTimeDTO> GetDailyWorkingTimes(Expression<Func<DayWorkingTimeEntity, bool>> value)
        //{
        //    IList<DayWorkingTimeEntity> dayWorkingTimeEntity = _dayWorkingTimeRepository.Repository.GetAll(value);
        //    IList<DayWorkingTime> dayWorkingTimeMode = _mapper.Map<List<DayWorkingTime>>(dayWorkingTimeEntity);
        //    List<DayWorkingTimeDTO> dayWorkingTimeDto = (List<DayWorkingTimeDTO>)_mapper.Map<IList<DayWorkingTimeDTO>>(dayWorkingTimeMode);
        //    return dayWorkingTimeDto;
        //}

        //public List<ExceptionDayWorkingTimeDTO> GetExceptionDayWorkingTimes(Expression<Func<ExceptionDayWorkingTimeEntity, bool>> value)
        //{
        //    IList<ExceptionDayWorkingTimeEntity> exceptionWorkingTimeEntity = _exceptionDayWorkingRepository.Repository.GetAll(value);
        //    IList<ExceptionDayWorkingTime> exceptionWorkingMode = _mapper.Map<IList<ExceptionDayWorkingTime>>(exceptionWorkingTimeEntity);
        //    List<ExceptionDayWorkingTimeDTO> exceptionWorkingDto = (List<ExceptionDayWorkingTimeDTO>)_mapper.Map<IList<DayWorkingTimeDTO>>(exceptionWorkingMode);
        //    return exceptionWorkingDto;
        //}
        #endregion

        #region language Resource
        //private void GetLanguageResourceList(IList<ClenderWorkingTimeDTO> refList)
        //{
        //    if (refList != null && refList.Count > 0)
        //    {
        //        foreach (ClenderWorkingTimeDTO weekWorking in refList)
        //        {
        //            weekWorking.nameImageLanguageRessource = _languageResourceSetService.GetLanguageResourcesByCode(weekWorking.NameLabelCode);
        //        }
        //    }
        //}
        //private void UpdateLanguageResources(ClenderWorkingTimeDTO refDto)
        //{
        //    if (refDto.nameImageLanguageRessource != null)
        //        _languageResourceSetService.UpdateRange(refDto.NameLabelCode, refDto.nameImageLanguageRessource);

        //}
        //private void AddLanguageResources(ClenderWorkingTimeDTO refDataDTO)
        //{
        //    List<LanguageResourceDTO> languageResources = new();
        //    if (refDataDTO.nameImageLanguageRessource != null && refDataDTO.nameImageLanguageRessource.Count > 0)
        //    {
        //        languageResources.AddRange(refDataDTO.nameImageLanguageRessource);

        //    }

        //    _languageResourceSetService.AddRange(languageResources);
        //}
        #endregion
        #endregion


    }
}
