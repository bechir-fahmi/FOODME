using AutoMapper;
using Platform.GenericRepository;
using Platform.ReferencialData.Business.business_services;
using Platform.ReferencialData.Business.business_services.LanguageData;
using Platform.ReferencialData.Business.business_services.LocationData;
using Platform.ReferencialData.BusinessModel;
using Platform.ReferencialData.GenericRepository;
using Platform.ReferentialData.DataModel;
using Platform.ReferentialData.DataModel.LocationData;
using Platform.ReferentialData.DtoModel;
using Platform.Shared.Cache;
using Platform.Shared.Enum;

namespace Platform.ReferencialData.Business.business_services_implementations;

public class ZoneService : IZoneService
{
    private readonly IUnitOfWork<ZoneEntity> _zoneRepository;
    private readonly IUnitOfWork<AreaEntity> _areaRepository;
    private readonly IUnitOfWork<TagZoneEntity> _tagZoneRepository;
    private readonly IAreaService _areaService;
    private readonly IMapper _mapper;
    private readonly ICacheService _cache;
    private readonly ILanguageResourceService _languageResourceService;
    private readonly string _cacheKey = CacheKey.ZoneCacheKey;

    public ZoneService(IUnitOfWork<ZoneEntity> zoneRepository, IMapper mapper, ICacheService cache, ILanguageResourceService languageResourceService, IUnitOfWork<TagZoneEntity> tagZoneRepository, IUnitOfWork<AreaEntity> areaRepository, IAreaService areaService)
    {
        _zoneRepository = zoneRepository;
        _mapper = mapper;
        _cache = cache;
        _languageResourceService = languageResourceService;
        _tagZoneRepository = tagZoneRepository;
        _areaRepository = areaRepository;
        _areaService = areaService;
    }

    public void Add(ZoneDTO refDataDTO, bool updateCache = true)
    {
        var zone = _mapper.Map<Zone>(refDataDTO);
        var nwZone = _mapper.Map<ZoneEntity>(zone);
        AreaEntity area = new AreaEntity();
        area.AreaName = zone.Name;
        area.CityId = zone.CityId;

        _areaRepository.Repository.Attach(area);
        _areaRepository.Save();
        if (updateCache)
        {
            _cache.RemoveData(CacheKey.AreaCacheKey);
        }
        var addedArea=_areaService.GetAreaByName(area.AreaName);
        nwZone.AreaId = addedArea.Id;
        _zoneRepository.Repository.Attach(nwZone);
        _zoneRepository.Save();

        if (updateCache)
        {
            _cache.RemoveData(_cacheKey);
        }
    }

    public ZoneDTO Get(int id)
    {
        return GetAll().FirstOrDefault(z => z.Id == id);
    }

    public PagedList<ZoneDTO> GetAll(PagedParameters pagedParameters)
    {
        var zoneDTOs = GetAll();

        return PagedList<ZoneDTO>.ToGenericPagedList(zoneDTOs, pagedParameters);
    }
    public PagedList<ZoneDTO> GetAllActiveData(PagedParameters pagedParameters)
    {
        var zoneDTOs = GetAll().Where(x=>x.Status==Status.isActive).ToList();

        return PagedList<ZoneDTO>.ToGenericPagedList(zoneDTOs, pagedParameters);
    }

    public List<ZoneDTO> GetAll()
    {
        var cachedData = _cache.GetData<List<ZoneDTO>>(_cacheKey);
        if (cachedData != null && cachedData.Count != 0)
        {
            return cachedData;
        }
        else
        {
            var vendorList = _zoneRepository.Repository.GetAll(includes: new List<string>()
            { "LanguageResourceSet",  "Tags", "Country","Area", "City","Region"});
            var vendorBMList = _mapper.Map<IList<Zone>>(vendorList);
            var vendorDtoList = _mapper.Map<IList<ZoneDTO>>(vendorBMList);
            _cache.SetData(_cacheKey, vendorDtoList, DateTimeOffset.UtcNow.AddDays(1));
            return (List<ZoneDTO>)vendorDtoList;
        }
    }

    public void Remove(ZoneDTO refDataDTO, bool updateCache = true)
    {
        var zone = _mapper.Map<Zone>(refDataDTO);
        var nwZone = _mapper.Map<ZoneEntity>(zone);
        nwZone.Status = Status.isDeleted;
        _zoneRepository.Repository.Update(nwZone);
        _zoneRepository.Save();
        if (updateCache)
           _cache.RemoveData(_cacheKey);
        
    }
    public void deleteOldTags(int idZone, List<TagZoneEntity> tags)
    {
        var oldTags = _tagZoneRepository.Repository.GetAll(x => x.ZoneId == idZone);
        if (oldTags.Count >= 1)
        {
            _tagZoneRepository.Repository.DeleteRange(oldTags);
            _tagZoneRepository.Save();
        }
    }
    public void Update(ZoneDTO refDataDTO, bool updateCache = true)
    {
        var zone = _mapper.Map<Zone>(refDataDTO);
        var nwZone = _mapper.Map<ZoneEntity>(zone);
        _languageResourceService.deleteOldLanguageResources(nwZone.LanguageResourceSet.LanguageResourceSetId, nwZone.LanguageResourceSet.LanguageRessource);
        deleteOldTags(nwZone.Id, nwZone.Tags);
        _zoneRepository.Repository.Update(nwZone);
        _zoneRepository.Save();
        if (updateCache)
            _cache.RemoveData(_cacheKey);

    }
}
