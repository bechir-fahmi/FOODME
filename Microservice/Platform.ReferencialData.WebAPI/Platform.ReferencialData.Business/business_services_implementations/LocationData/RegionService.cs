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
    public class RegionService : IRegionService
    {

        private readonly IUnitOfWork<RegionEntity> _regionRepository;
        private readonly IUnitOfWork<TagRegionEntity> _tagRegionRepository;
        private readonly IMapper _mapper;
        private readonly ILanguageResourceService _languageResourceService;
        private readonly ICacheService _cache;
        private readonly string _cacheKey = CacheKey.RegionCacheKey;
        public RegionService(IUnitOfWork<RegionEntity> regionRepository,IUnitOfWork<TagRegionEntity> tagRegionRepository,
            IMapper mapper,
            ILanguageResourceService languageResourceService,
            ICacheService cache)
        {
            _regionRepository = regionRepository;
            _tagRegionRepository = tagRegionRepository;
            _mapper = mapper;
            _languageResourceService = languageResourceService;
            _cache = cache;
        }

        public List<RegionDTO> GetAll()
        {
            var cachedData = _cache.GetData<List<RegionDTO>>(_cacheKey);
            if (cachedData != null)
            {
                return cachedData;
            }
            else
            {
                var regionList = _regionRepository.Repository.GetAll(includes: new List<string>()
            { "LanguageResourceSet.LanguageRessource",  "Tags"});
                var regionBMList = _mapper.Map<IList<Region>>(regionList);
                var regionDtoList = _mapper.Map<IList<RegionDTO>>(regionBMList);
                _cache.SetData(_cacheKey, regionDtoList, DateTimeOffset.UtcNow.AddDays(1));
                return (List<RegionDTO>)regionDtoList;
            }
        }
        
        public PagedList<RegionDTO> GetAll(PagedParameters pagedParameters)
        {
            var regionDtoList = GetAll();

            return PagedList<RegionDTO>.ToGenericPagedList(regionDtoList, pagedParameters);
        }
        public PagedList<RegionDTO> GetAllActiveData(PagedParameters pagedParameters)
        {
            var regionDtoList = GetAll().Where(x=>x.Status==Status.isActive).ToList();

            return PagedList<RegionDTO>.ToGenericPagedList(regionDtoList, pagedParameters);
        }
        public RegionDTO Get(int id)
        {
            RegionDTO regionDTO = GetAll().FirstOrDefault(x => x.Id == id);
            return regionDTO;
        }

        public RegionDTO Get(Expression<Func<RegionEntity, bool>> expression)
        {
            RegionEntity region = _regionRepository.Repository.Get(expression);
            Region regionBM = _mapper.Map<Region>(region);
            RegionDTO regionDTO = _mapper.Map<RegionDTO>(regionBM);
            return regionDTO;
        }

        public void Add(RegionDTO region, bool updateCache = true)
        {
            Region regionBM = _mapper.Map<Region>(region);
            RegionEntity Region = _mapper.Map<RegionEntity>(regionBM);
            _regionRepository.Repository.Attach(Region);
            _regionRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }
        public void deleteOldTags(int idRegion, List<TagRegionEntity> tags)
        {
            var oldTags = _tagRegionRepository.Repository.GetAll(x => x.RegionId == idRegion);
            if (oldTags.Count >= 1)
            {
                _tagRegionRepository.Repository.DeleteRange(oldTags);
                _tagRegionRepository.Save();
            }
        }
        public void Update(RegionDTO region, bool updateCache = true)
        {
            Region regionBM = _mapper.Map<Region>(region);
            RegionEntity regionEntity = _mapper.Map<RegionEntity>(regionBM);
            _languageResourceService.deleteOldLanguageResources(regionEntity.LanguageResourceSet.LanguageResourceSetId, regionEntity.LanguageResourceSet.LanguageRessource);
            deleteOldTags(regionEntity.Id, regionEntity.Tags);
            _regionRepository.Repository.Update(regionEntity);
            _regionRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }

        public void Remove(RegionDTO region, bool updateCache = true)
        {
            Region regionBM = _mapper.Map<Region>(region);
            RegionEntity regionEntity = _mapper.Map<RegionEntity>(regionBM);
            regionEntity.Status = Shared.Enum.Status.isDeleted;
            _regionRepository.Repository.Update(regionEntity);
            _regionRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }
        public List<RegionDTO> GetFilteredData(RegionFillter filter)
        {
            var refDataDtoList = GetAll();
            refDataDtoList = refDataDtoList.Where(x => (!filter.Id.HasValue || x.Id == filter.Id.Value)).ToList();
            return refDataDtoList;
        }

        public void Remove(int refData, bool updateCache = true)
        {
            throw new NotImplementedException();
        }
    }
}
