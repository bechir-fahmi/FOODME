using AutoMapper;
using Platform.ReferencialData.Business.business_services.LocationData;
using Platform.ReferencialData.BusinessModel.LocationData;
using Platform.ReferencialData.GenericRepository;
using Platform.ReferentialData.DataModel.LocationData;
using Platform.ReferentialData.DtoModel.LocationData;
using Platform.Shared.Cache;
using System.Linq.Expressions;

namespace Platform.ReferencialData.Business.business_services_implementations.LocationData
{
    public class CoordinateService : ICoordinateService
    {
       
        private readonly IUnitOfWork<CoordinateEntity> _coordinateRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly string _cacheKey = CacheKey.CoordinateCacheKey;

        public CoordinateService(IUnitOfWork<CoordinateEntity> coordinateRepository, IMapper mapper, ICacheService cache)
        {
            _coordinateRepository = coordinateRepository;
            _mapper = mapper;
            _cache = cache;
        }

        public void Add(CoordinateDTO refDataDTO, bool updateCache = true)
        {
            Coordinate CountryBM = _mapper.Map<Coordinate>(refDataDTO);
            CoordinateEntity Country = _mapper.Map<CoordinateEntity>(CountryBM);
            _coordinateRepository.Repository.Attach(Country);
            _coordinateRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }

        public CoordinateDTO Get(int id)
        {
            var countryDto = GetAll().FirstOrDefault(x => x.Id == id);
            return countryDto;

        }

        public CoordinateDTO Get(Expression<Func<CoordinateEntity, bool>> expression)
        {
            CoordinateEntity country = _coordinateRepository.Repository.Get(expression);
            Coordinate countryBM = _mapper.Map<Coordinate>(country);
            CoordinateDTO countryDTO = _mapper.Map<CoordinateDTO>(countryBM);
            return countryDTO;
        }

        public List<CoordinateDTO> GetAll()
        {
            var cachedData = _cache.GetData<List<CoordinateDTO>>(_cacheKey);
            if (cachedData != null)
            {
                return cachedData;
            }
            else
            {
                var dayList = _coordinateRepository.Repository.GetAll();
                var dayBMList = _mapper.Map<IList<Coordinate>>(dayList);
                var dayDtoList = _mapper.Map<IList<CoordinateDTO>>(dayBMList);
                _cache.SetData(_cacheKey, dayDtoList, DateTimeOffset.UtcNow.AddDays(1));
                return (List<CoordinateDTO>)dayDtoList;
            }
        }

        public List<CoordinateDTO> GetAllByExpression(Expression<Func<CoordinateEntity, bool>> expression)
        {
            IList<CoordinateEntity> deliveryModeEntity = _coordinateRepository.Repository.GetAll(expression);
            IList<Coordinate> deliveryMode = _mapper.Map<IList<Coordinate>>(deliveryModeEntity);
            List<CoordinateDTO> deliveryModeDto = (List<CoordinateDTO>)_mapper.Map<IList<CoordinateDTO>>(deliveryMode);
            return deliveryModeDto;
        }

        public void Remove(CoordinateDTO refDataDTO, bool updateCache = true)
        {
            Coordinate dayBM = _mapper.Map<Coordinate>(refDataDTO);
            CoordinateEntity day = _mapper.Map<CoordinateEntity>(dayBM);
            day.Status = Shared.Enum.Status.isDeleted;
            _coordinateRepository.Repository.Update(day);
            _coordinateRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }

        public void Remove(int refData, bool updateCache = true)
        {
            throw new NotImplementedException();
        }

        public void Update(CoordinateDTO refDataDTO, bool updateCache = true)
        {
            Coordinate dayBM = _mapper.Map<Coordinate>(refDataDTO);
            CoordinateEntity day = _mapper.Map<CoordinateEntity>(dayBM);
            _coordinateRepository.Repository.Update(day);
            _coordinateRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }
    }
}
