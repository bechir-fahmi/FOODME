using AutoMapper;
using Platform.GenericRepository;
using Platform.ReferencialData.Business.business_services.WorkingTimeData;
using Platform.ReferencialData.BusinessModel.WorkingTime;
using Platform.ReferencialData.GenericRepository;
using Platform.ReferentialData.DataModel.WorkingTime;
using Platform.ReferentialData.DtoModel.WorkingTimeData;
using Platform.Shared.Cache;
using System.Linq.Expressions;

namespace Platform.ReferencialData.Business.business_services_implementations.WorkingTimeData
{
    public class ExceptionDayWorkingTimeService : IExceptionDayWorkingTimeService
    {
        private readonly IUnitOfWork<ExceptionDayWorkingTimeEntity> _exceptionDayWorkingTimeRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly string _cacheKey = CacheKey.ExceptionDayWorkingTimeCaheKey;

        public ExceptionDayWorkingTimeService(IUnitOfWork<ExceptionDayWorkingTimeEntity> exceptionDayWorkingTimeRepository, IMapper mapper, ICacheService cache)
        {
            _exceptionDayWorkingTimeRepository = exceptionDayWorkingTimeRepository;
            _mapper = mapper;
            _cache = cache;
        }

        public void Add(ExceptionDayWorkingTimeDTO refDataDTO, bool updateCache = true)
        {
            ExceptionDayWorkingTime refdataBM = _mapper.Map<ExceptionDayWorkingTime>(refDataDTO);
            ExceptionDayWorkingTimeEntity refdata = _mapper.Map<ExceptionDayWorkingTimeEntity>(refdataBM);
            _exceptionDayWorkingTimeRepository.Repository.Attach(refdata);
            _exceptionDayWorkingTimeRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }

        public ExceptionDayWorkingTimeDTO Get(int id)
        {
            var refDataDTO = GetAll().FirstOrDefault(x => x.Id == id);
            return refDataDTO;
        }

        public ExceptionDayWorkingTimeDTO Get(Expression<Func<ExceptionDayWorkingTimeEntity, bool>> expression)
        {
            Expression<Func<ExceptionDayWorkingTimeDTO, bool>> exp = _mapper.Map<Expression<Func<ExceptionDayWorkingTimeDTO, bool>>>(expression);
            List<ExceptionDayWorkingTimeDTO> dayworkingList = GetAll();
            ExceptionDayWorkingTimeDTO dayWorking = null;
            if (dayworkingList != null && dayworkingList.Count > 0)
            {
                dayWorking = ((IQueryable<ExceptionDayWorkingTimeDTO>)dayworkingList).FirstOrDefault(exp);
            }

            return dayWorking;
        }

        public PagedList<ExceptionDayWorkingTimeDTO> GetAll(PagedParameters pagedParameters)
        {
            var refDtoList = GetAll();

            return PagedList<ExceptionDayWorkingTimeDTO>.ToGenericPagedList(refDtoList, pagedParameters);
        }

        public List<ExceptionDayWorkingTimeDTO> GetAll()
        {
/*            var cachedData = _cache.GetData<List<ExceptionDayWorkingTimeDTO>>(_cacheKey);
            if (cachedData != null)
            {
                return cachedData;
            }
            else
            {*/
                var dayList = _exceptionDayWorkingTimeRepository.Repository.GetAll();
                var dayBMList = _mapper.Map<IList<ExceptionDayWorkingTime>>(dayList);
                var dayDtoList = _mapper.Map<IList<ExceptionDayWorkingTimeDTO>>(dayBMList);
          //      _cache.SetData(_cacheKey, dayDtoList, DateTimeOffset.UtcNow.AddDays(1));
                return (List<ExceptionDayWorkingTimeDTO>)dayDtoList;
         //   }
        }

        public List<ExceptionDayWorkingTimeDTO> GetAllByExpression(Expression<Func<ExceptionDayWorkingTimeEntity, bool>> expression)
        {
            IList<ExceptionDayWorkingTimeEntity> dayModeEntity = _exceptionDayWorkingTimeRepository.Repository.GetAll(expression);
            IList<ExceptionDayWorkingTime> dayMode = _mapper.Map<IList<ExceptionDayWorkingTime>>(dayModeEntity);
            List<ExceptionDayWorkingTimeDTO> dayModeDto = (List<ExceptionDayWorkingTimeDTO>)_mapper.Map<IList<ExceptionDayWorkingTimeDTO>>(dayMode);
            return dayModeDto;
        }

        public void Remove(int id, bool updateCache = true)
        {
            _exceptionDayWorkingTimeRepository.Repository.Delete(id);
            _exceptionDayWorkingTimeRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }

        public void Remove(ExceptionDayWorkingTimeDTO refDataDTO, bool updateCache = true)
        {
            throw new NotImplementedException();
        }

        public void Update(ExceptionDayWorkingTimeDTO refDataDTO, bool updateCache = true)
        {
            ExceptionDayWorkingTime dayBM = _mapper.Map<ExceptionDayWorkingTime>(refDataDTO);
            ExceptionDayWorkingTimeEntity day = _mapper.Map<ExceptionDayWorkingTimeEntity>(dayBM);
            _exceptionDayWorkingTimeRepository.Repository.Update(day);
            _exceptionDayWorkingTimeRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }
    }
}
