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
    public class DayWorkingTimeService : IDayWorkingTimeService
    {
        private readonly IUnitOfWork<DayWorkingTimeEntity> _dayWorkingTimeRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly string _cacheKey = CacheKey.DayWorkingTimeCaheKey;

        public DayWorkingTimeService(IUnitOfWork<DayWorkingTimeEntity> dayWorkingTimeRepository, IMapper mapper, ICacheService cache)
        {
            _dayWorkingTimeRepository = dayWorkingTimeRepository;
            _mapper = mapper;
            _cache = cache;
        }

        public void Add(DayWorkingTimeDTO refDataDTO, bool updateCache = true)
        {
            DayWorkingTime refdataBM = _mapper.Map<DayWorkingTime>(refDataDTO);
            DayWorkingTimeEntity refdata = _mapper.Map<DayWorkingTimeEntity>(refdataBM);
            _dayWorkingTimeRepository.Repository.Attach(refdata);
            _dayWorkingTimeRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }

        public DayWorkingTimeDTO Get(int id)
        {
            var refDataDTO = GetAll().FirstOrDefault(x => x.Id == id);
            return refDataDTO;
        }

        public DayWorkingTimeDTO Get(Expression<Func<DayWorkingTimeEntity, bool>> expression)
        {
            Expression<Func<DayWorkingTimeDTO, bool>> exp = _mapper.Map<Expression<Func<DayWorkingTimeDTO, bool>>>(expression);
            List<DayWorkingTimeDTO> dayworkingList = GetAll();
            DayWorkingTimeDTO dayWorking = null;
            if (dayworkingList != null && dayworkingList.Count > 0)
            {
                dayWorking = ((IQueryable<DayWorkingTimeDTO>)dayworkingList).FirstOrDefault(exp);
            }

            return dayWorking;
        }

        public PagedList<DayWorkingTimeDTO> GetAll(PagedParameters pagedParameters)
        {
            var refDtoList = GetAll();

            return PagedList<DayWorkingTimeDTO>.ToGenericPagedList(refDtoList, pagedParameters);
        }

        public List<DayWorkingTimeDTO> GetAll()
        {
/*            var cachedData = _cache.GetData<List<DayWorkingTimeDTO>>(_cacheKey);
            if (cachedData != null)
            {
                return cachedData;
            }
            else
            {*/
                var dayList = _dayWorkingTimeRepository.Repository.GetAll();
                var dayBMList = _mapper.Map<IList<DayWorkingTime>>(dayList);
                var dayDtoList = _mapper.Map<IList<DayWorkingTimeDTO>>(dayBMList);
             //   _cache.SetData(_cacheKey, dayDtoList, DateTimeOffset.UtcNow.AddDays(1));
                return (List<DayWorkingTimeDTO>)dayDtoList;
        //    }
        }

        public List<DayWorkingTimeDTO> GetAllByExpression(Expression<Func<DayWorkingTimeEntity, bool>> expression)
        {
            IList<DayWorkingTimeEntity> dayModeEntity = _dayWorkingTimeRepository.Repository.GetAll(expression);
            IList<DayWorkingTime> dayMode = _mapper.Map<IList<DayWorkingTime>>(dayModeEntity);
            List<DayWorkingTimeDTO> dayModeDto = (List<DayWorkingTimeDTO>)_mapper.Map<IList<DayWorkingTimeDTO>>(dayMode);
            return dayModeDto;
        }
       

        public void Remove(int id, bool updateCache = true)
        {
            _dayWorkingTimeRepository.Repository.Delete(id);
            _dayWorkingTimeRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }

        public void Remove(DayWorkingTimeDTO refDataDTO, bool updateCache = true)
        {
            throw new NotImplementedException();
        }

        public void Update(DayWorkingTimeDTO refDataDTO, bool updateCache = true)
        {
            DayWorkingTime dayBM = _mapper.Map<DayWorkingTime>(refDataDTO);
            DayWorkingTimeEntity day = _mapper.Map<DayWorkingTimeEntity>(dayBM);
            _dayWorkingTimeRepository.Repository.Update(day);
            _dayWorkingTimeRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }
    }
}
