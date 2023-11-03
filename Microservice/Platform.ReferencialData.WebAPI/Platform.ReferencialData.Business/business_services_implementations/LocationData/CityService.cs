using AutoMapper;
using Platform.GenericRepository;
using Platform.ReferencialData.Business.business_services.LanguageData;
using Platform.ReferencialData.Business.business_services.LocationData;
using Platform.ReferencialData.BusinessModel.LocationData;
using Platform.ReferencialData.GenericRepository;
using Platform.ReferentialData.DataModel;
using Platform.ReferentialData.DataModel.LocationData;
using Platform.ReferentialData.DtoModel.LocationData;
using Platform.ReferentialData.DtoModel.LocationData.Fillter;
using Platform.Shared.Cache;
using Platform.Shared.Enum;
using System.Linq.Expressions;

namespace Platform.ReferencialData.Business.business_services_implementations.LocationData
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWork<CityEntity> _cityRepository;
        private readonly IUnitOfWork<TagCityEntity> _tagCityRepository;
        private readonly IMapper _mapper;
        private readonly ILanguageResourceService _languageResourceService;
        private readonly ICacheService _cache;
        private readonly string _cacheKey = CacheKey.CityCacheKey;
        public CityService(IUnitOfWork<CityEntity> cityRepository,IUnitOfWork<TagCityEntity> tagCityRepository,
            IMapper mapper,
            ILanguageResourceService languageResourceService,
            ICacheService cache)
        {
            _cityRepository = cityRepository;
            _tagCityRepository = tagCityRepository;
            _mapper = mapper;
            _languageResourceService = languageResourceService;
            _cache = cache;
        }

        public List<CityDTO> GetAll()
        {
            var cachedData = _cache.GetData<List<CityDTO>>(_cacheKey);
            if (cachedData != null)
            {
                return cachedData;
            }
            else
            {
                var cityList = _cityRepository.Repository.GetAll(includes: new List<string>()
                { "LanguageResourceSet.LanguageRessource",  "Tags"});
                var cityBMList = _mapper.Map<IList<City>>(cityList);
                var cityDtoList = _mapper.Map<IList<CityDTO>>(cityBMList);
                _cache.SetData(_cacheKey, cityDtoList, DateTimeOffset.Now.AddDays(1));
                return (List<CityDTO>)cityDtoList;
            }
        }
        public PagedList<CityDTO> GetAll(PagedParameters pagedParameters)
        {
            var cityDtoList = GetAll();

            return PagedList<CityDTO>.ToGenericPagedList(cityDtoList, pagedParameters);

        }
        public PagedList<CityDTO> GetAllActiveData(PagedParameters pagedParameters)
        {
            var cityDtoList = GetAll().Where(x=>x.Status==Status.isActive).ToList();

            return PagedList<CityDTO>.ToGenericPagedList(cityDtoList, pagedParameters);

        }
        public CityDTO Get(int cityId)
        {
            var cityDto = GetAll();
            return cityDto.FirstOrDefault(x => x.Id == cityId);
        }
        public List<CityDTO> GetCityByRegionId(int regionId)
        {
            List<CityDTO> cityList = GetAll().Where(x => x.RegionId == regionId).ToList();
            return cityList;
        }
        public CityDTO Get(Expression<Func<CityEntity, bool>> expression)
        {
            CityEntity city = _cityRepository.Repository.Get(expression);
            City cityBM = _mapper.Map<City>(city);
            CityDTO cityDTO = _mapper.Map<CityDTO>(cityBM);
            return cityDTO;
        }
        public void Add(CityDTO city, bool updateCache = true)
        {
            City CityBM = _mapper.Map<City>(city);
            CityEntity City = _mapper.Map<CityEntity>(CityBM);
            _cityRepository.Repository.Attach(City);
            _cityRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }
        public void deleteOldTags(int idCity, List<TagCityEntity> tags)
        {
            var oldTags = _tagCityRepository.Repository.GetAll(x => x.CityId == idCity);
            if (oldTags.Count >= 1)
            {
                _tagCityRepository.Repository.DeleteRange(oldTags);
                _tagCityRepository.Save();
            }
        }
        public void Update(CityDTO city, bool updateCache = true)
        {
            City cityBM = _mapper.Map<City>(city);
            CityEntity cityEntity = _mapper.Map<CityEntity>(cityBM);
            _languageResourceService.deleteOldLanguageResources(cityEntity.LanguageResourceSet.LanguageResourceSetId, cityEntity.LanguageResourceSet.LanguageRessource);
            deleteOldTags(cityEntity.Id, cityEntity.Tags);
            _cityRepository.Repository.Update(cityEntity);
            _cityRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);

        }
        public void Remove(CityDTO city, bool updateCache = true)
        {
            City cityBM = _mapper.Map<City>(city);
            CityEntity cityEntity = _mapper.Map<CityEntity>(cityBM);
            cityEntity.Status = Shared.Enum.Status.isDeleted;
            _cityRepository.Repository.Update(cityEntity);
            _cityRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }
        public List<CityDTO> GetFilteredData(CityFillter fillter)
        {
            var refDataDtoList = GetAll();
            refDataDtoList = refDataDtoList.Where(x => (!fillter.Id.HasValue || x.Id == fillter.Id.Value)
            && (string.IsNullOrEmpty(fillter.CityCode) || x.CityCode.ToLower().Trim().Contains(fillter.CityCode.ToLower().Trim()))).ToList();
            return refDataDtoList;
        }
        public void Remove(int refData, bool updateCache = true)
        {
            throw new NotImplementedException();
        }
    }
}
