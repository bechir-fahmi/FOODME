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
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork<CountryEntity> _countryRepository;
        private readonly IMapper _mapper;
        private readonly ILanguageResourceService _languageResourceService;
        private readonly ICacheService _cache;
        private readonly string _cacheKey = CacheKey.CountryCacheKey;
        public CountryService(IUnitOfWork<CountryEntity> countryRepository,
            IMapper mapper,
            ILanguageResourceService languageResourceService,
            ICacheService cache)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
            _languageResourceService = languageResourceService;
            _cache = cache;
        }

        public List<CountryDTO> GetAll()
        {
            var cacheData = _cache.GetData<List<CountryDTO>>(_cacheKey);
            if (cacheData != null)
            {
                return cacheData;
            }
            else
            {
                var countryList = _countryRepository.Repository.GetAll();
                var countryBMList = _mapper.Map<IList<Country>>(countryList);
                var countryDtoList = _mapper.Map<IList<CountryDTO>>(countryBMList);
                _cache.SetData(_cacheKey, countryDtoList, DateTimeOffset.UtcNow.AddDays(1));

                return (List<CountryDTO>)countryDtoList;
            }
        }

        public PagedList<CountryDTO> GetAll(PagedParameters pagedParameters)
        {
            var countryDtoList = GetAll();

            return PagedList<CountryDTO>.ToGenericPagedList(countryDtoList, pagedParameters);

        }
        public PagedList<CountryDTO> GetAllActiveData(PagedParameters pagedParameters)
        {
            var countryDtoList = GetAll().Where(x=>x.Status==Status.isActive).ToList();

            return PagedList<CountryDTO>.ToGenericPagedList(countryDtoList, pagedParameters);

        }

        public CountryDTO Get(int countryId)
        {
            var countryDto = GetAll().FirstOrDefault(x => x.Id == countryId);
            return countryDto;
        }

        public CountryDTO Get(Expression<Func<CountryEntity, bool>> expression)
        {
            CountryEntity country = _countryRepository.Repository.Get(expression);
            Country countryBM = _mapper.Map<Country>(country);
            CountryDTO countryDTO = _mapper.Map<CountryDTO>(countryBM);
            return countryDTO;
        }

        public void Add(CountryDTO country, bool updateCache = true)
        {
            Country CountryBM = _mapper.Map<Country>(country);
            CountryEntity Country = _mapper.Map<CountryEntity>(CountryBM);
            _countryRepository.Repository.Attach(Country);
            _countryRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }

        public void Update(CountryDTO country, bool updateCache = true)
        {
            Country countryBM = _mapper.Map<Country>(country);
            CountryEntity countryEntity = _mapper.Map<CountryEntity>(countryBM);

            _countryRepository.Repository.Update(countryEntity);
            _countryRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }

        public void Remove(CountryDTO country, bool updateCache = true)
        {
            Country countryBM = _mapper.Map<Country>(country);
            CountryEntity countryEntity = _mapper.Map<CountryEntity>(countryBM);
            countryEntity.Status = Shared.Enum.Status.isDeleted;
            _countryRepository.Repository.Update(countryEntity);
            _countryRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }

        public List<CountryDTO> GetFilteredData(CountryFillter filter)
        {
            var refDataDtoList = GetAll();

            refDataDtoList = refDataDtoList.Where(x => (!filter.Id.HasValue || x.Id == filter.Id.Value)
            && (string.IsNullOrEmpty(filter.CountryKey) || x.CountryKey == filter.CountryKey)
            && (string.IsNullOrEmpty(filter.Code) || x.Code.ToLower().Trim().Contains(filter.Code.ToLower().Trim()))).ToList();

            return refDataDtoList;
        }

        public void Remove(int refData, bool updateCache = true)
        {
            throw new NotImplementedException();
        }
    }
}
