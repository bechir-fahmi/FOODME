using AutoMapper;
using Platform.GenericRepository;
using Platform.ReferencialData.Business.business_services.LanguageData;
using Platform.ReferencialData.Business.business_services.LocationData;
using Platform.ReferencialData.BusinessModel.LocationData;
using Platform.ReferencialData.GenericRepository;
using Platform.ReferentialData.DataModel.LocationData;
using Platform.ReferentialData.DtoModel.LocationData;
using Platform.ReferentialData.DtoModel.LocationData.Fillter;
using Platform.Shared.Cache;
using Platform.Shared.Enum;
using System.Linq.Expressions;

namespace Platform.ReferencialData.Business.business_services_implementations.LocationData
{
    public class AreaService : IAreaService
    {
        private readonly IUnitOfWork<AreaEntity> _areaRepository;
        private readonly IMapper _mapper;
        private readonly ILanguageResourceService _languageResourceService;
        private readonly ICoordinateService _CoordinateResourceService;
        private readonly ICacheService _cache;
        private readonly string _cacheKey = CacheKey.AreaCacheKey;
        public AreaService(IUnitOfWork<AreaEntity> areaRepository,
            IMapper mapper,
            ILanguageResourceService languageResourceService,
            ICacheService cache,
            ICoordinateService coordinateResourceService)
        {
            _areaRepository = areaRepository;
            _mapper = mapper;
            _languageResourceService = languageResourceService;
            _cache = cache;
            _CoordinateResourceService = coordinateResourceService;
        }

        public List<AreaDTO> GetAll()
        {
            var cachedData = _cache.GetData<List<AreaDTO>>(_cacheKey);
            if (cachedData != null)
            {
                return cachedData;
            }
            else
            {
                var areaList = _areaRepository.Repository.GetAll();
                var areaBMList = _mapper.Map<List<Area>>(areaList);
                var areaDtoList = _mapper.Map<List<AreaDTO>>(areaBMList);
                _cache.SetData(_cacheKey, areaDtoList, DateTimeOffset.UtcNow.AddDays(1));
                return areaDtoList;
            }
        }
        public PagedList<AreaDTO> GetAll(PagedParameters pagedParameters)
        {
            var areaDtoList = GetAll();

            return PagedList<AreaDTO>.ToGenericPagedList(areaDtoList, pagedParameters);
        }
        public PagedList<AreaDTO> GetAllActiveData(PagedParameters pagedParameters)
        {
            var areaDtoList = GetAll().Where(x => x.Status == Status.isActive).ToList();

            return PagedList<AreaDTO>.ToGenericPagedList(areaDtoList, pagedParameters);
        }
        public List<AreaDTO> GetAreaByCityId(int cityId)
        {
            List<AreaDTO> areaList = GetAll().Where(x => x.CityId == cityId).ToList();
            return areaList;
        }
        public AreaDTO Get(int areaId)
        {
            var areaDtoList = GetAll();
            return areaDtoList.FirstOrDefault(x => x.Id == areaId);
        }   
        public AreaDTO GetAreaByName(string areaIName)
        {
            var areaDtoList = GetAll();
            return areaDtoList.FirstOrDefault(x => x.AreaName == areaIName);
        }
        public AreaDTO Get(Expression<Func<AreaEntity, bool>> expression)
        {
            var areaEntity = _areaRepository.Repository.Get(expression);
            var areaBM = _mapper.Map<Area>(areaEntity);
            var areaDto = _mapper.Map<AreaDTO>(areaBM);
            return areaDto;
        }
        public void Add(AreaDTO area, bool updateCache = true)
        {
            Area areaBM = _mapper.Map<Area>(area);
            AreaEntity areaEntity = _mapper.Map<AreaEntity>(areaBM);
            _areaRepository.Repository.Attach(areaEntity);
            _areaRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }
        public void Update(AreaDTO area, bool updateCache = true)
        {
            var areaBM = _mapper.Map<Area>(area);
            var areaEntity = _mapper.Map<AreaEntity>(areaBM);
            _areaRepository.Repository.Update(areaEntity);
            _areaRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }
        public void Remove(AreaDTO area, bool updateCache = true)
        {
            var areaBM = _mapper.Map<Area>(area);
            var areaEntity = _mapper.Map<AreaEntity>(areaBM);
            areaEntity.Status = Shared.Enum.Status.isDeleted;
            _areaRepository.Repository.Update(areaEntity);
            _areaRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }
        #region Language
        #endregion
        public List<AreaDTO> GetFilteredData(AreaFillter fillter)
        {
            var refDataDtoList = GetAll();

           /* refDataDtoList = refDataDtoList.Where(x => (!fillter.VendorId.HasValue || x.VendorId == fillter.VendorId.Value)
                                                      && (string.IsNullOrEmpty(fillter.AreaCode) || x.AreaCode.Trim().Contains(fillter.AreaCode.Trim()))).ToList();
           */ return refDataDtoList;
        }

        public void Remove(int refData, bool updateCache = true)
        {
            throw new NotImplementedException();
        }
    }
}
