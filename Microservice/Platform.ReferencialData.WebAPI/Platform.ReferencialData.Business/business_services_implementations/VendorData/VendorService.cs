using AutoMapper;
using Platform.GenericRepository;
using Platform.ReferencialData.Business.business_services;
using Platform.ReferencialData.Business.business_services.LanguageData;
using Platform.ReferencialData.BusinessModel;
using Platform.ReferencialData.GenericRepository;
using Platform.ReferentialData.DataModel;
using Platform.ReferentialData.DataModel.BrandData.IntegrationBrand;
using Platform.ReferentialData.DataModel.LocationData;
using Platform.ReferentialData.DtoModel;
using Platform.ReferentialData.DtoModel.BrandData;
using Platform.Shared.Cache;
using System.Linq.Expressions;
using Platform.Shared.Enum;
using Platform.ReferentialData.DataModel.BrandData;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Platform.ReferentialData.DtoModel.BrandData.Recommandation;
using Platform.Tracking.DtoModel.BrandAction;
using Platform.ReferencialData.BusinessModel.BrandData.Integration;
using Platform.ReferentialData.DtoModel.BrandData.Integration;
using Platform.ReferentialData.DataModel.BrandData.Integration;
using System.Collections.Generic;
using Platform.ReferencialData.Business.business_services.Authentication;
using Platform.Shared.Images;
using System.Globalization;
using System.IO;
using NetTopologySuite.Index.HPRtree;
using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore.Query;
using Platform.ReferentialData.DataModel.QueryData;
using Platform.ReferentialData.DtoModel.QueryData;
using Platform.ReferencialData.BusinessModel.QueryData;
using Twilio.TwiML.Messaging;

namespace Platform.ReferencialData.Business.business_services_implementations;

public class VendorService : IVendorService
{
    private readonly IUnitOfWork<VendorEntity> _vendorRepository;
    private readonly IUnitOfWork<CityEntity> _cityRepository;
    private readonly IUnitOfWork<RegionEntity> _regionRepository;
    private readonly IUnitOfWork<ZoneEntity> _zoneRepository;
    private readonly IUnitOfWork<VendorDeliveryZonesEntity> _vendorZoneRepository;
    private readonly IUnitOfWork<StaticIntegrationEntity> _staticIntegrationRepository;
    private readonly IUnitOfWork<DynamicIntegrationEntity> _dynamicIntegrationRepository;
    private readonly IUnitOfWork<IntegrationMethodEntity> _methodRepository;
    private readonly IUnitOfWork<IntegrationParameterEntity> _parameterRepository;
    private readonly IUnitOfWork<AuthenticationEntity> _authRepository;
    private readonly IUnitOfWork<VendorDeliveryModeEntity> _vendorDeliveryModeRepository;
    private readonly IUnitOfWork<VendorFoodTypeEntity> _vendorFoodTypeRepository;
    private readonly IUnitOfWork<VendorKitchenTypeEntity> _vendorKitchenTypeRepository;
    private readonly IUnitOfWork<VendorMealTypeEntity> _vendorMealTypeRepository;
    private readonly IUnitOfWork<VendorMealTimingEntity> _vendorMealTimingRepository;
    private readonly IUnitOfWork<BrandMatchingEntity> _brandMatchingRepository;
    private readonly IUnitOfWork<QueryEntity> _queryRepository;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly ICacheService _cache;
    private readonly string _cacheKey = CacheKey.VendorCacheKey;
    private readonly string _cacheKeyForDeals = CacheKey.DealsCacheKey;
    private readonly string _cacheKeyForMatching = CacheKey.BrandMatchingCacheKey;

    public VendorService(IUnitOfWork<VendorEntity> vendorRepository, IUserService userService, IMapper mapper, ICacheService cache, ILanguageResourceService languageResourceService,
                            IUnitOfWork<CityEntity> cityRepository, IUnitOfWork<RegionEntity> regionRepository,
                            IUnitOfWork<ZoneEntity> zoneRepository, IUnitOfWork<VendorDeliveryZonesEntity> vendorZoneRepository, IUnitOfWork<StaticIntegrationEntity> staticIntegrationRepository,
                            IUnitOfWork<DynamicIntegrationEntity> dynamicIntegrationRepository, IUnitOfWork<IntegrationMethodEntity> methodRepository,
                            IUnitOfWork<IntegrationParameterEntity> parametreRepository, IUnitOfWork<AuthenticationEntity> authRepository, IUnitOfWork<VendorDeliveryModeEntity> vendorDeliveryModeRepository,
                            IUnitOfWork<VendorFoodTypeEntity> vendorFoodTypeRepository, IUnitOfWork<VendorMealTypeEntity> vendorMealTypeRepository, IUnitOfWork<VendorMealTimingEntity> vendorMealTimingRepository,
                            IUnitOfWork<VendorKitchenTypeEntity> vendorKitchenTypeRepository, IUnitOfWork<BrandMatchingEntity> brandMatchingRepository, IUnitOfWork<QueryEntity> queryRepository)
    {
        _vendorRepository = vendorRepository;
        _userService = userService;
        _mapper = mapper;
        _cache = cache;
        _cityRepository = cityRepository;
        _regionRepository = regionRepository;
        _zoneRepository = zoneRepository;
        _staticIntegrationRepository = staticIntegrationRepository;
        _dynamicIntegrationRepository = dynamicIntegrationRepository;
        _methodRepository = methodRepository;
        _parameterRepository = parametreRepository;
        _authRepository = authRepository;
        _vendorDeliveryModeRepository = vendorDeliveryModeRepository;
        _vendorFoodTypeRepository = vendorFoodTypeRepository;
        _vendorKitchenTypeRepository = vendorKitchenTypeRepository;
        _vendorMealTypeRepository = vendorMealTypeRepository;
        _vendorMealTimingRepository = vendorMealTimingRepository;
        _brandMatchingRepository = brandMatchingRepository;
        _vendorZoneRepository = vendorZoneRepository;
        _queryRepository = queryRepository;

    }

    #region Vendor Management
    #region Get
    public List<VendorDTO> GetAll()
    {
        var cachedData = _cache.GetData<List<VendorDTO>>(_cacheKey);
        cachedData = null;
        if (cachedData != null && cachedData.Count != 0)
        {
            return cachedData;
        }
        else
        {
            var vendorList = _vendorRepository.Repository.GetAll(
                includes: new List<string>()
               { "Zones.Zone.LanguageResourceSet.LanguageRessource", "StaticIntegrations", "DynamicIntegrations",
                "VendorFoodTypes.FoodType.LanguageResourceSet.LanguageRessource",
                "VendorMealTypes.MealType.LanguageResourceSet.LanguageRessource",
                "VendorMealTimings.MealTiming.LanguageResourceSet.LanguageRessource",
                "VendorKitchenTypes.KitchenType.LanguageResourceSet.LanguageRessource" }
            );
            var vendorBMList = _mapper.Map<IList<Vendor>>(vendorList);
            List<VendorDTO> vendorDTOList = _mapper.Map<IList<VendorDTO>>(vendorBMList).ToList();
            _cache.SetData(_cacheKey, vendorDTOList, DateTimeOffset.UtcNow.AddDays(1));
            return vendorDTOList;
        }

    }
    public List<VendorDTO> GetAll(VendorType vendorType)
    {

        var res = GetAll().Where(x => x.Type == vendorType).ToList();
        return res;
    }
    public VendorDTO Get(Guid Id, VendorType vendorType)
    {
        var vendorDTO = GetAll(vendorType).FirstOrDefault(x => x.VendorId == Id);
        return vendorDTO;
    }
    public PagedList<VendorDTO> Get(string tag, PagedParameters pagedParameters, VendorType vendorType)
    {
        var vendorDTOByTag = GetAll(vendorType);
        return PagedList<VendorDTO>.ToGenericPagedList(vendorDTOByTag, pagedParameters);
    }
    public PagedList<VendorDTO> GetAll(PagedParameters pagedParameters, VendorType vendorType, string userId)
    {
        var userEntity = _userService.GetUser(userId);
        var vendorDTOList = GetAll(vendorType);
        if (userEntity.UserType == "aggregator" || userEntity.UserType == "brand")
        {
            if (userEntity.AssignedTo != null)
            {
                var VendorDtoListFiltredByVendor = vendorDTOList.Where(x => x.VendorId.ToString() == userEntity.AssignedTo).ToList();
                return PagedList<VendorDTO>.ToGenericPagedList(VendorDtoListFiltredByVendor, pagedParameters);
            }
            else
            {
                return PagedList<VendorDTO>.ToGenericPagedList(new List<VendorDTO> { }, pagedParameters);
            }
        }

        return PagedList<VendorDTO>.ToGenericPagedList(vendorDTOList, pagedParameters);
    }
    public List<VendorDTO> GetAllActiveData(VendorType vendorType)
    {
        return GetAll(vendorType).Where(x => x.Status == Status.isActive).ToList();
    }
    public List<VendorDTO> GetAllActiveData()
    {
        var aggregators = GetAll(VendorType.Aggregator).Where(x => x.Status == Status.isActive).ToList();
        var vendors = GetAll(VendorType.Brand).Where(x => x.Status == Status.isActive).ToList().Union(aggregators).ToList();
        return vendors;
    }
    public PagedList<VendorDTO> GetAllActiveData(PagedParameters pagedParameters, VendorType vendorType, string userId)
    {
        var userEntity = _userService.GetUser(userId);
        var vendorDTOList = GetAllActiveData(vendorType);
        if (userEntity.UserType == "aggregator" || userEntity.UserType == "brand")
        {
            if (userEntity.AssignedTo != null)
            {
                var VendorDtoListFiltredByVendor = vendorDTOList.Where(x => x.VendorId.ToString() == userEntity.AssignedTo).ToList();
                return PagedList<VendorDTO>.ToGenericPagedList(VendorDtoListFiltredByVendor, pagedParameters);
            }
            else
            {
                return PagedList<VendorDTO>.ToGenericPagedList(new List<VendorDTO> { }, pagedParameters);
            }
        }
        return PagedList<VendorDTO>.ToGenericPagedList(GetAllActiveData(vendorType), pagedParameters);
    }
    public VendorDTO Get(Expression<Func<VendorEntity, bool>> expression, VendorType vendorType)
    {
        Expression<Func<VendorDTO, bool>> exp = _mapper.Map<Expression<Func<VendorDTO, bool>>>(expression);
        List<VendorDTO> vendorList = GetAll(vendorType);
        VendorDTO vendorDTO = null;
        if (vendorList != null && vendorList.Count > 0)
        {
            vendorDTO = ((IQueryable<VendorDTO>)vendorList).FirstOrDefault(exp);
        }

        return vendorDTO;
    }
    private bool VendorExist(Guid vendorId, VendorType vendorType)
    {
        var vendorModel = Get(vendorId, vendorType);
        if (vendorModel != null)
            return true;
        return false;

    }

    public VendorDTO GetVendorByName(string name)
    {
        var vendorEntity = _vendorRepository.Repository.Get(c => string.IsNullOrEmpty(name) && c.Name.ToLower() == name.ToLower());
        Vendor vendorBM = _mapper.Map<Vendor>(vendorEntity);
        VendorDTO vendorDTO = _mapper.Map<VendorDTO>(vendorBM);
        return vendorDTO;
    }
    List<DynamicIntegrationDTO> IVendorService.GetDynamicIntegrationsByVendor(Guid vendorId, VendorType vendorType)
    {
        return GetAll(vendorType).FirstOrDefault(x => x.VendorId == vendorId).DynamicIntegrations.ToList();
    }
    #endregion
    #region Set
    public void Add(VendorDTO vendor, bool updateCache = true)
    {
        if (!string.IsNullOrEmpty(vendor.Logo))
        {
            var imageURL = ImageHelper.SaveImage(ImageHelper.SaveUrl, vendor.Logo);
            vendor.Logo = imageURL;
        }
        Vendor vendorBM = _mapper.Map<Vendor>(vendor);
        VendorEntity vendorEntity = _mapper.Map<VendorEntity>(vendorBM);
        _vendorRepository.Repository.Attach(vendorEntity);
        _vendorRepository.Save();
        if (updateCache)
        {
            _cache.RemoveData(_cacheKey);
        }
    }
    public void Update(VendorDTO vendor, VendorType vendorType, bool updateCache = true)
    {
        Vendor vendorBM = _mapper.Map<Vendor>(vendor);
        VendorEntity vendorEntity = _mapper.Map<VendorEntity>(vendorBM);
        var res = _vendorDeliveryModeRepository.Repository.GetAll().Where(x => x.VendorId == vendorEntity.VendorId);
        _vendorDeliveryModeRepository.Repository.DeleteRange(res);
        _vendorDeliveryModeRepository.Save();
        var res1 = _vendorFoodTypeRepository.Repository.GetAll().Where(x => x.VendorId == vendorEntity.VendorId);
        _vendorFoodTypeRepository.Repository.DeleteRange(res1);
        _vendorFoodTypeRepository.Save();
        var res2 = _vendorMealTypeRepository.Repository.GetAll().Where(x => x.VendorId == vendorEntity.VendorId);

        _vendorMealTypeRepository.Repository.DeleteRange(res2);
        _vendorMealTypeRepository.Save();
        var res3 = _vendorMealTimingRepository.Repository.GetAll().Where(x => x.VendorId == vendorEntity.VendorId);

        _vendorMealTimingRepository.Repository.DeleteRange(res3);
        _vendorMealTimingRepository.Save();
        var res4 = _vendorKitchenTypeRepository.Repository.GetAll().Where(x => x.VendorId == vendorEntity.VendorId);
        _vendorKitchenTypeRepository.Repository.DeleteRange(res4);
        _vendorKitchenTypeRepository.Save();

        var res5 = _vendorZoneRepository.Repository.GetAll().Where(x => x.VendorId == vendorEntity.VendorId);
        _vendorZoneRepository.Repository.DeleteRange(res5);
        _vendorZoneRepository.Save();

        var vendorExist = _vendorRepository.Repository.Get(x => x.VendorId == vendorEntity.VendorId);
        if (vendorExist?.Logo != vendor.Logo)
        {
            if (!string.IsNullOrEmpty(vendorExist.Logo))
            {
                ImageHelper.DeleteImage(ImageHelper.SaveUrl, vendorExist.Logo);
            }
            if (!string.IsNullOrEmpty(vendor.Logo))
            {
                var imageURL = ImageHelper.SaveImage(ImageHelper.SaveUrl, vendor.Logo);
                vendorEntity.Logo = imageURL;
            }
        }
        vendorEntity.Type = vendorType;
        _vendorRepository.Repository.Update(vendorEntity);
        _vendorRepository.Save();
        if (updateCache)
        {
            _cache.RemoveData(_cacheKey);
        }
    }
    public void Remove(Guid vendorId, VendorType vendorType, bool updateCache = true)
    {
        var vendorDTO = Get(vendorId, vendorType);
        Vendor vendorBM = _mapper.Map<Vendor>(vendorDTO);
        VendorEntity vendorEntity = _mapper.Map<VendorEntity>(vendorBM);
        vendorEntity.Status = Status.isDeleted;
        _vendorRepository.Repository.Update(vendorEntity);
        _vendorRepository.Save();
        if (updateCache)
        {
            _cache.RemoveData(_cacheKey);
        }
    }
    #endregion

    #region General Information
    public VendorDTO AddGeneralInformations(VendorGeneralInformationDTO vendor, bool updateCache = true)
    {
        if (!string.IsNullOrEmpty(vendor.Logo))
        {
            var imageURL = ImageHelper.SaveImage(ImageHelper.SaveUrl, vendor.Logo);
            vendor.Logo = imageURL;

        }
        VendorDTO vendorDTO = new VendorDTO();
        Vendor vendorBM = _mapper.Map<Vendor>(vendor);
        VendorEntity vendorEntity = _mapper.Map<VendorEntity>(vendorBM);
        _vendorRepository.Repository.Insert(vendorEntity);
        _vendorRepository.Save();
        vendorBM = _mapper.Map<Vendor>(vendorEntity);
        vendorDTO = _mapper.Map<VendorDTO>(vendorBM);


        if (updateCache)
        {
            _cache.RemoveData(_cacheKey);
        }
        return vendorDTO;
    }
    public VendorGeneralInformationDTO GetGeneralInformations(Guid id, VendorType vendorType)
    {
        VendorGeneralInformationDTO vendorDTO = new VendorGeneralInformationDTO();
        var vendorEntity = Get(id, vendorType);
        Vendor vendorBM = _mapper.Map<Vendor>(vendorEntity);
        vendorDTO = _mapper.Map<VendorGeneralInformationDTO>(vendorBM);
        return vendorDTO;
    }
    public VendorDTO UpdateGeneralInformations(VendorGeneralInformationDTO refDataDTO, bool updateCache = true)
    {
        var vendorExist = _vendorRepository.Repository.Get(x => x.VendorId == refDataDTO.VendorId);
        if (!string.IsNullOrEmpty(vendorExist.Logo))
        {
            ImageHelper.DeleteImage(ImageHelper.SaveUrl, vendorExist.Logo);
        }
        if (!string.IsNullOrEmpty(refDataDTO.Logo))
        {
            var imageURL = ImageHelper.SaveImage(ImageHelper.SaveUrl, refDataDTO.Logo);
            refDataDTO.Logo = imageURL;
        }
        VendorDTO vendorDTO = new VendorDTO();
        Vendor vendorBM = _mapper.Map<Vendor>(refDataDTO);
        VendorEntity vendorEntity = _mapper.Map<VendorEntity>(vendorBM);
        _vendorRepository.Repository.Update(vendorEntity);
        _vendorRepository.Save();
        vendorBM = _mapper.Map<Vendor>(vendorEntity);
        vendorDTO = _mapper.Map<VendorDTO>(vendorBM);


        if (updateCache)
        {
            _cache.RemoveData(_cacheKey);
        }
        return vendorDTO;
    }

    #endregion

    #region Delivery Zones
    public List<VendorDeliveryZoneDTO> GetVendorDeliveryZones(Guid id, VendorType vendorType)
    {
        return Get(id, vendorType).Zones.ToList();
    }
    public void Add(List<VendorDeliveryZoneDTO> vendorDeliveryZone, Guid vendorId, VendorType vendorType, bool updateCache = true)
    {
        List<VendorDeliveryZone> zonesBM = _mapper.Map<List<VendorDeliveryZone>>(vendorDeliveryZone);
        List<VendorDeliveryZonesEntity> nwVendorZone = _mapper.Map<List<VendorDeliveryZonesEntity>>(zonesBM);
        if (VendorExist(vendorId, vendorType))
        {
            _vendorZoneRepository.Repository.InsertRange(nwVendorZone);
            _vendorZoneRepository.Save();
            if (updateCache)
            {
                _cache.RemoveData(_cacheKey);
            }
        }
    }
    public void Update(List<VendorDeliveryZoneDTO> deliveryZones, Guid vendorId, VendorType vendorType, bool updateCache = true)
    {
        List<VendorDeliveryZone> zonesBM = _mapper.Map<List<VendorDeliveryZone>>(deliveryZones);
        List<VendorDeliveryZonesEntity> zonesEntity = _mapper.Map<List<VendorDeliveryZonesEntity>>(zonesBM);
        var vendor = Get(vendorId, vendorType);
        if (vendor != null)
        {
            List<VendorDeliveryZone> oldZonesBM = _mapper.Map<List<VendorDeliveryZone>>(vendor.Zones);
            List<VendorDeliveryZonesEntity> oldZonesEntity = _mapper.Map<List<VendorDeliveryZonesEntity>>(oldZonesBM);
            _vendorZoneRepository.Repository.DeleteRange(oldZonesEntity);
            _vendorZoneRepository.Repository.InsertRange(zonesEntity);
            _vendorRepository.Save();
            if (updateCache)
            {
                _cache.RemoveData(_cacheKey);
            }
        }
    }
    #endregion

    #region Vendor Data
    public VendorDataDTO GetVendorData(Guid id, VendorType vendorType)
    {
        VendorDataDTO vendorDataDTO = new VendorDataDTO();
        var vendorEntity = Get(id, vendorType);
        vendorDataDTO.VendorMealTypes = vendorEntity.VendorMealTypes;
        vendorDataDTO.VendorMealTimings = vendorEntity.VendorMealTimings;
        vendorDataDTO.VendorKitchenTypes = vendorEntity.VendorKitchenTypes;
        vendorDataDTO.VendorFoodTypes = vendorEntity.VendorFoodTypes;
        vendorDataDTO.VendorDeliverys = vendorEntity.VendorDeliverys;
        vendorDataDTO.Description = vendorEntity.Description;
        vendorDataDTO.OtherDescription = vendorEntity.OtherDescription;
        return vendorDataDTO;
    }
    public void Add(VendorDataDTO vendorData, Guid vendorId, VendorType vendorType, bool updateCache = true)
    {
        var vendor = _vendorRepository.Repository.Get(x => x.VendorId == vendorId);

        var vendorKitchenTypesBM = _mapper.Map<List<VendorKitchenType>>(vendorData.VendorKitchenTypes);
        var vendorKitchenTypesEntity = _mapper.Map<List<VendorKitchenTypeEntity>>(vendorKitchenTypesBM);

        var vendorFoodTypesBM = _mapper.Map<List<VendorFoodType>>(vendorData.VendorFoodTypes);
        var vendorFoodTypesEntity = _mapper.Map<List<VendorFoodTypeEntity>>(vendorFoodTypesBM);

        var vendorMealTypesBM = _mapper.Map<List<VendorMealType>>(vendorData.VendorMealTypes);
        var vendorMealTypesEntity = _mapper.Map<List<VendorMealTypeEntity>>(vendorMealTypesBM);

        var vendorMealTimingsBM = _mapper.Map<List<VendorMealTiming>>(vendorData.VendorMealTimings);
        var vendorMealTimingsEntity = _mapper.Map<List<VendorMealTimingEntity>>(vendorMealTimingsBM);

        vendor.VendorKitchenTypes = vendorKitchenTypesEntity;
        vendor.VendorFoodTypes = vendorFoodTypesEntity;
        vendor.VendorMealTypes = vendorMealTypesEntity;
        vendor.VendorMealTimings = vendorMealTimingsEntity;

        if (VendorExist(vendorId, vendorType))
        {
            //todo verify constraint couple(vendorDTO, foodType) exist
            _vendorRepository.Repository.Attach(vendor);
            _vendorRepository.Save();
            if (updateCache)
            {
                _cache.RemoveData(_cacheKey);
            }
        }
    }
    public VendorDTO Update(VendorDataDTO refDataDTO, Guid vendorId, bool updateCache = true)
    {
        VendorDTO vendorDTO = new VendorDTO();
        Vendor vendorBM = _mapper.Map<Vendor>(refDataDTO);
        VendorEntity vendorEntity = _mapper.Map<VendorEntity>(vendorBM);
        _vendorRepository.Repository.Update(vendorEntity);
        _vendorRepository.Save();
        vendorBM = _mapper.Map<Vendor>(vendorEntity);
        vendorDTO = _mapper.Map<VendorDTO>(vendorBM);


        if (updateCache)
        {
            _cache.RemoveData(_cacheKey);
        }
        return vendorDTO;
    }
    #endregion

    #region Static Integrations
    public List<StaticIntegrationDTO> GetAllStaticIntegration(Guid vendorId)
    {
        List<StaticIntegrationDTO> staticIntegrationsDTO = new List<StaticIntegrationDTO>();
        var staticIntegrationsEntity = _staticIntegrationRepository.Repository.GetAll(x => x.VendorId == vendorId);
        List<StaticIntegration> staticIntegrationsBM = _mapper.Map<List<StaticIntegration>>(staticIntegrationsEntity);
        staticIntegrationsDTO = _mapper.Map<List<StaticIntegrationDTO>>(staticIntegrationsBM);
        return staticIntegrationsDTO;
    }
    public StaticIntegrationDTO GetStaticIntegration(Guid id)
    {
        StaticIntegrationDTO staticIntegrationDTO = new StaticIntegrationDTO();
        var staticIntegrationEntity = _staticIntegrationRepository.Repository.GetAll(x => x.StaticIntegrationId == id);
        StaticIntegration staticIntegrationBM = _mapper.Map<StaticIntegration>(staticIntegrationEntity);
        staticIntegrationDTO = _mapper.Map<StaticIntegrationDTO>(staticIntegrationBM);
        return staticIntegrationDTO;
    }
    public void AddStaticIntegration(List<StaticIntegrationDTO> staticIntegrationsDTO, Guid vendorId, VendorType vendorType, bool updateCache = true)
    {
        List<StaticIntegration> staticIntegrationsBM = _mapper.Map<List<StaticIntegration>>(staticIntegrationsDTO);
        List<StaticIntegrationEntity> staticIntegrationsEntity = _mapper.Map<List<StaticIntegrationEntity>>(staticIntegrationsBM);
        if (VendorExist(vendorId, vendorType))
        {
            _staticIntegrationRepository.Repository.InsertRange(staticIntegrationsEntity);
            _vendorRepository.Save();
            if (updateCache)
            {
                _cache.RemoveData(_cacheKey);
            }
        }
    }
    public void UpdateAllStaticIntegrations(List<StaticIntegrationDTO> staticIntegrationsDTO, Guid vendorId, VendorType vendorType, bool updateCache = true)
    {
        List<StaticIntegration> staticIntegrationsBM = _mapper.Map<List<StaticIntegration>>(staticIntegrationsDTO);
        List<StaticIntegrationEntity> staticIntegrationsEntity = _mapper.Map<List<StaticIntegrationEntity>>(staticIntegrationsBM);
        if (VendorExist(vendorId, vendorType))
        {
            foreach (var item in staticIntegrationsDTO)
            {
                UpdateStaticIntegration(item, vendorId, vendorType);
            }
        }
    }
    public void UpdateStaticIntegration(StaticIntegrationDTO staticIntegrationDTO, Guid vendorId, VendorType vendorType, bool updateCache = true)
    {
        StaticIntegration staticIntegrationBM = _mapper.Map<StaticIntegration>(staticIntegrationDTO);
        StaticIntegrationEntity staticIntegrationEntity = _mapper.Map<StaticIntegrationEntity>(staticIntegrationBM);
        if (VendorExist(vendorId, vendorType))
        {
            _staticIntegrationRepository.Repository.Update(staticIntegrationEntity);
            _staticIntegrationRepository.Save();
            if (updateCache)
            {
                _cache.RemoveData(_cacheKey);
            }
        }
    }
    public void RemoveStaticIntegration(Guid id, bool updateCache = true)
    {
        _staticIntegrationRepository.Repository.Delete(id);
        _staticIntegrationRepository.Save();
        if (updateCache)
        {
            _cache.RemoveData(_cacheKey);
        }
    }

    #endregion

    #region Dynamic Integrations
    public void AddDynamicIntegration(DynamicIntegrationDTO vendorDynamicIntegration, Guid vendorId, VendorType vendorType, QueryDTO queryDTO, bool updateCache = true)
    {
        DynamicIntegration vendorDIBM = _mapper.Map<DynamicIntegration>(vendorDynamicIntegration);
        DynamicIntegrationEntity vendorDIEntity = _mapper.Map<DynamicIntegrationEntity>(vendorDIBM);
        if (VendorExist(vendorId, vendorType))
        {
            _authRepository.Repository.Attach(vendorDIEntity.Authentication);
            _authRepository.Save();
            foreach (IntegrationMethodEntity method in vendorDIEntity.IntegrationMethod)
            {
                if (!method.UseDefaultAuth)
                {
                    _authRepository.Repository.Attach(method.Authentication);
                    _authRepository.Save();
                }
                else
                {

                    method.AuthenticationId = vendorDIEntity.Authentication.AuthenticationId;
                }
/*                if (method.IntegrationType == IntegrationType.Matching)
                {
                    BrandMatchingDTO brandMatchingDTO = new BrandMatchingDTO();
                    brandMatchingDTO.AggregatorId = vendorId;
                    brandMatchingDTO.LocalBrandId = Guid.Parse(method.IntegrationParameters.Find(x => x.MatchWithKey == Matching.BrandId).Key);
                    brandMatchingDTO.DistantBrandId = method.IntegrationParameters.Find(x => x.MatchWithKey == Matching.BrandId).MatchWithValue;

                    MatchBrands(brandMatchingDTO);
                }*/
            }

            _dynamicIntegrationRepository.Repository.Insert(vendorDIEntity);
            _dynamicIntegrationRepository.Save();
            Query queryBM = _mapper.Map<Query>(queryDTO);
            QueryEntity queryEntity = _mapper.Map<QueryEntity>(queryBM);
            _queryRepository.Repository.Insert(queryEntity);
            _queryRepository.Save();

            if (updateCache)
            {
                _cache.RemoveData(_cacheKey);
            }
        }
    }
    public DynamicIntegrationEntity GetDynamicIntegration(Guid id)
    {
        DynamicIntegrationDTO dynamicIntegrationDTO = new DynamicIntegrationDTO();
        var dynamicIntegrationEntity = _dynamicIntegrationRepository.Repository.Get(x => x.DynamicIntegrationId == id);
        return dynamicIntegrationEntity;
    }
    private async Task<List<DealsDTO>> GetDealsFromDynamicIntegrations(VendorDTO vendor, AddressToSearchDTO addressToSearch, List<ZoneDTO> zones)
    {
        List<DealsDTO> deals = new List<DealsDTO>();
        foreach (var dynamicIntegration in vendor.DynamicIntegrations)
        {
            var methodsEntity = _methodRepository.Repository.GetAll(x => x.DynamicIntegrationId == dynamicIntegration.DynamicIntegrationId);
            var methodsBM = _mapper.Map<List<IntegrationMethod>>(methodsEntity);
            dynamicIntegration.IntegrationMethod = _mapper.Map<List<IntegrationMethodDTO>>(methodsBM);
            foreach (var method in dynamicIntegration.IntegrationMethod)
            {
                var authEntity = _authRepository.Repository.Get(x => x.AuthenticationId == method.AuthenticationId);
                var authsBM = _mapper.Map<AuthenticationBM>(authEntity);
                method.MethodAuthentication = _mapper.Map<AuthenticationDTO>(authsBM);
                var api = string.Concat(dynamicIntegration.http, @"://", dynamicIntegration.URi, ":", dynamicIntegration.Port, @"/", method.EndPoint);
                var listParametersIN = GetMethodParametersIN(addressToSearch, method);
                var listParametersOUT = GetMethodParametersOUT(method);
                HttpContent content = null;
                if (listParametersIN[0].Any())
                {
                    var body = CreateBody(addressToSearch, zones,listParametersIN[0]);
                    content = new StringContent(body, Encoding.UTF8, "application/json");           
                }
                if (listParametersIN[1].Any())
                {
                    var query = CreateQuery(addressToSearch, listParametersIN[1],method.EndPoint);
                    if (method.MethodType == MethodType.Get)
                    {
                        api += query;
                    }
                }
                deals = await CallApiByPost(api, content, listParametersOUT, method, addressToSearch, vendor);
            }
        }
        return deals;
    }
    public void UpdateDynamicIntegration(DynamicIntegrationDTO dIntegrationDTO, Guid vendorId, bool updateCache = true)
    {
        var oldDynamicIntegration = GetDynamicIntegration(dIntegrationDTO.DynamicIntegrationId);
        var oldparameters = oldDynamicIntegration.IntegrationMethod.FirstOrDefault().IntegrationParameters;
        _parameterRepository.Repository.DeleteRange(oldparameters);
        _parameterRepository.Save();
        DynamicIntegration DIntegrationdBM = _mapper.Map<DynamicIntegration>(dIntegrationDTO);
        DynamicIntegrationEntity DIEntity = _mapper.Map<DynamicIntegrationEntity>(DIntegrationdBM);
        _dynamicIntegrationRepository.Repository.Update(DIEntity);
        _dynamicIntegrationRepository.Save();
        if (updateCache)
        {
            _cache.RemoveData(_cacheKey);
        }
    }
    public void RemoveDynamicIntegration(Guid id, bool updateCache = true)
    {
        _dynamicIntegrationRepository.Repository.Delete(id);
        _dynamicIntegrationRepository.Save();
        if (updateCache)
        {
            _cache.RemoveData(_cacheKey);
        }
    }
    bool IVendorService.VendorHaveDynamicInetgration(Guid vendorId, VendorType vendorType)
    {
        var vendorModel = Get(vendorId, vendorType);
        if ((vendorModel.DynamicIntegrations != null) && (vendorModel.DynamicIntegrations.Count > 0))
            return true;
        return false;
    }
    #endregion

    #region BrandMatching
    public void MatchBrands(List<BrandMatchingDTO> brandMatchingDTO)
    {

        List<BrandMatching> BrandMatchingBM = _mapper.Map<List<BrandMatching>>(brandMatchingDTO);
        List<BrandMatchingEntity> BrandMatchingEntity = _mapper.Map<List<BrandMatchingEntity>>(BrandMatchingBM);
        _brandMatchingRepository.Repository.InsertRange(BrandMatchingEntity);
        _brandMatchingRepository.Save();
    }
    public List<BrandMatchingDTO> GetAllMatching()
    {
        List<BrandMatchingDTO> matching = new List<BrandMatchingDTO>();
        List<BrandMatchingEntity> matchingEntity = _brandMatchingRepository.Repository.GetAll().ToList();
        List<BrandMatching> matchingBM = _mapper.Map<IList<BrandMatching>>(matchingEntity).ToList();
        matching = _mapper.Map<IList<BrandMatchingDTO>>(matchingBM).ToList();
        return matching;
    }
    #endregion

    #endregion


    #region Search Engine

    /// <summary>
    /// INTEGRATION STEP 1 : Fix Search Zone  
    /// </summary>
    /// <param name="addressToSearch"></param>
    /// <returns></returns>
    private List<ZoneDTO> FixSearchZone(AddressToSearchDTO addressToSearch, int languageKey)
    {
        List<ZoneDTO> zones = new();
        List<Zone> zonesBM = new();
        List<ZoneEntity> zonesEntity = new();
        if (addressToSearch.RegionName != null)
        {
            var allRegions = _regionRepository.Repository.GetAll(includes: new List<string>()
            { "LanguageResourceSet.LanguageRessource"});
            IList<RegionEntity> regions = new List<RegionEntity>();
            if (allRegions!=null)
            {
                regions = allRegions.Where(x => x.LanguageResourceSet != null && x.LanguageResourceSet.LanguageRessource.First(l => l.LanguageKey == (LanguageKey)languageKey).Value.ToLower() == addressToSearch.RegionName.ToLower()).ToList();
            }
            if (regions != null)
            {
                foreach (var item in regions)
                {
                    zonesEntity = _zoneRepository.Repository.GetAll(x => x.RegionId == item.Id).ToList();
                }
            }
        }
        if (zonesEntity != null)
        {
            zonesBM = _mapper.Map<List<Zone>>(zonesEntity);
        }
        return _mapper.Map<List<ZoneDTO>>(zonesBM);
    }
    /// <summary>
    /// INTEGRATION STEP 2 : Get Parameters IN
    /// </summary>
    /// <param name="addressToSearch"></param>
    /// <param name="method"></param>
    /// <returns></returns>
    private List<List<IntegrationParameterDTO>> GetMethodParametersIN(AddressToSearchDTO addressToSearch, IntegrationMethodDTO method)
    {
        List<List<IntegrationParameterDTO>> paramsIN = new();
        List<IntegrationParameterDTO> parametreInBody = new();
        List<IntegrationParameterDTO> parametreInQuery = new();
        var parms = _parameterRepository.Repository.GetAll(x => x.MethodId == method.IntegrationMethodId).Where(y => y.Type == ParamsType.In);
        var parmsBM = _mapper.Map<List<IntegrationParameter>>(parms);
        method.IntegrationParameters = _mapper.Map<List<IntegrationParameterDTO>>(parmsBM);
        if (addressToSearch.Latitude != null && addressToSearch.Longitude != null && method.IntegrationParameters.Any(p => p.MatchWithKey == Matching.Longitude))
        {
            parametreInBody = new List<IntegrationParameterDTO>();
            parametreInBody.AddRange(method.IntegrationParameters.Where(x => x.MatchWithKey == Matching.Latitude && x.QueryOrBody == Source.FromBody).ToList());
            parametreInBody.AddRange(method.IntegrationParameters.Where(x => x.MatchWithKey == Matching.Longitude && x.QueryOrBody == Source.FromBody).ToList());

            parametreInQuery.AddRange(method.IntegrationParameters.Where(x => x.MatchWithKey == Matching.Latitude && x.QueryOrBody == Source.FromQuery).ToList());
            parametreInQuery.AddRange(method.IntegrationParameters.Where(x => x.MatchWithKey == Matching.Longitude && x.QueryOrBody == Source.FromQuery).ToList());
        }
        if (addressToSearch.RegionName != null && method.IntegrationParameters.Any(p => p.MatchWithKey == Matching.Region))
        {
            parametreInBody = new List<IntegrationParameterDTO>();
            parametreInBody.AddRange(method.IntegrationParameters.Where(x => x.MatchWithKey == Matching.Region && x.QueryOrBody == Source.FromBody).ToList());
            parametreInQuery.AddRange(method.IntegrationParameters.Where(x => x.MatchWithKey == Matching.Region && x.QueryOrBody == Source.FromQuery).ToList());
        }
        if (addressToSearch.CountryName != null)
        {
            parametreInBody.AddRange(method.IntegrationParameters.Where(x => x.MatchWithKey == Matching.Country && x.QueryOrBody == Source.FromBody).ToList());
            parametreInQuery.AddRange(method.IntegrationParameters.Where(x => x.MatchWithKey == Matching.Country && x.QueryOrBody == Source.FromQuery).ToList());
        }
        if (addressToSearch.CityCode != null)
        {
            parametreInBody.AddRange(method.IntegrationParameters.Where(x => x.MatchWithKey == Matching.City && x.QueryOrBody == Source.FromBody).ToList());
            parametreInQuery.AddRange(method.IntegrationParameters.Where(x => x.MatchWithKey == Matching.City && x.QueryOrBody == Source.FromQuery).ToList());
        }
        if (addressToSearch.ZoneName != null && method.IntegrationParameters.Any(p => p.MatchWithKey == Matching.Zone))
        {
            parametreInBody = new List<IntegrationParameterDTO>();
            parametreInBody.AddRange(method.IntegrationParameters.Where(x => x.MatchWithKey == Matching.Zone && x.QueryOrBody == Source.FromBody).ToList());
            parametreInQuery.AddRange(method.IntegrationParameters.Where(x => x.MatchWithKey == Matching.Zone && x.QueryOrBody == Source.FromQuery).ToList());
        }
        paramsIN.Add(parametreInBody);
        paramsIN.Add(parametreInQuery);
        return paramsIN;
    }
    /// <summary>
    /// INTEGRATION STEP 3 : Get Parameters OUT
    /// </summary>
    /// <param name="addressToSearch"></param>
    /// <param name="method"></param>
    /// <returns></returns>
    private List<IntegrationParameterDTO> GetMethodParametersOUT(IntegrationMethodDTO method)
    {
        List<List<IntegrationParameterDTO>> paramsIN = new();
        List<IntegrationParameterDTO> parametreInBody = new();
        List<IntegrationParameterDTO> parametreInQuery = new();
        var parms = _parameterRepository.Repository.GetAll(x => x.MethodId == method.IntegrationMethodId).Where(y => y.Type == ParamsType.Out);
        var parmsBM = _mapper.Map<List<IntegrationParameter>>(parms);
        method.IntegrationParameters = _mapper.Map<List<IntegrationParameterDTO>>(parmsBM);
        return method.IntegrationParameters;
    }

    /// <summary>
    /// INTEGRATION STEP 4 : Create Vendor body 
    /// </summary>
    /// <param name="addressToSearch"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    private static string CreateBody(AddressToSearchDTO addressToSearch, List<ZoneDTO> zones, List<IntegrationParameterDTO> parameters)
    {
        string objectReturn = "{";
        foreach (var parameter in parameters)
        {
            if (parameter.MatchWithKey == Matching.City && addressToSearch.CityCode != null)
            {
                objectReturn += '"' + parameter.Key + '"' + ":" + '"' + addressToSearch.CityCode + '"';
            }
            else if (parameter.MatchWithKey == Matching.Region && addressToSearch.RegionName != null)
            {
                objectReturn += '"' + parameter.Key + '"' + ":" + '"' + addressToSearch.RegionName + '"';
            }
            else if (parameter.MatchWithKey == Matching.Zone && addressToSearch.ZoneName != null)
            {
                objectReturn += '"' + parameter.Key + '"' + ":" + '"' + addressToSearch.ZoneName + '"';
            }
            else if (parameter.MatchWithKey == Matching.Country && addressToSearch.CountryName != null)
            {
                objectReturn += '"' + parameter.Key + '"' + ":" + '"' + addressToSearch.CountryName + '"';
            }
            else if (parameter.MatchWithKey == Matching.Latitude && addressToSearch.Latitude != null)
            {
                objectReturn += '"' + parameter.Key + '"' + ":" + '"' + addressToSearch.Latitude + '"' + ",";
            }
            if (parameter.MatchWithKey == Matching.Longitude && addressToSearch.Longitude != null)
            {
                objectReturn += '"' + parameter.Key + '"' + ":" + '"' + addressToSearch.Longitude + '"';
            }
            if (parameter.MatchWithKey.ToString() == "-1")
            {
                    objectReturn += '"' + parameter.Key + "=" + parameter.MatchWithValue + '"';
            }
        }
        if (objectReturn.EndsWith(","))
        {
            objectReturn = objectReturn.Remove(objectReturn.Length - 1);
        }
        return objectReturn + "}";
    }

    /// <summary>
    /// INTEGRATION STEP 5 : Create Vendor query
    /// </summary>
    /// <param name="addressToSearch"></param>
    /// <param name="apiParametres"></param>
    /// <returns></returns>
    private static string CreateQuery(AddressToSearchDTO addressToSearch, List<IntegrationParameterDTO> apiParametres, string endpoint)
    {
        string objectReturn = "";
        if ( !endpoint.EndsWith('&'))
        {
            objectReturn = "?";
        }       
        foreach (var parametre in apiParametres)
        {
            if (parametre.MatchWithKey == Matching.City && addressToSearch.CityCode != null)
            {
                objectReturn = objectReturn + parametre.Key + "=" + addressToSearch.CityCode + "&";
            }
            if (parametre.MatchWithKey == Matching.Region && addressToSearch.RegionName != null)
            {
                objectReturn = objectReturn + parametre.Key + "=" + addressToSearch.RegionName + "&";
            }
            if (parametre.MatchWithKey == Matching.Country && addressToSearch.CountryName != null)
            {
                objectReturn = objectReturn + parametre.Key + "=" + addressToSearch.CountryName + "&";
            }
            if (parametre.MatchWithKey == Matching.Latitude && addressToSearch.Latitude != null)
            {
                var culture = CultureInfo.InvariantCulture;
                objectReturn = objectReturn + parametre.Key + "=" + addressToSearch.Latitude.Value.ToString(culture) + "&";
            }
            if (parametre.MatchWithKey == Matching.Longitude && addressToSearch.Longitude != null)
            {
                var culture = CultureInfo.InvariantCulture;
                objectReturn = objectReturn + parametre.Key + "=" + addressToSearch.Longitude.Value.ToString(culture) + "&";
            }
            if (parametre.MatchWithKey.ToString() =="-1" )
            {
                objectReturn = objectReturn + parametre.Key + "=" + parametre.MatchWithValue + "&";
            }
        }
        return objectReturn.Length == 0 ? "" : objectReturn?.Remove(objectReturn.Length - 1);
    }
    /// <summary>
    /// INTEGRATION STEP 6 : Call Vendor
    /// </summary>
    /// <param name="dynamicIntegration"></param>
    /// <param name="body"></param>
    /// <param name="api"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    private async Task<List<DealsDTO>> CallApiByPost(string api, HttpContent content, List<IntegrationParameterDTO> parametersOUT, IntegrationMethodDTO method, AddressToSearchDTO deliveryZone, VendorDTO vendor)
    {

        HttpClient client = new();
        if (method.MethodAuthentication.AuthenticationType == AuthenticationType.APIKey)
        {
            client.DefaultRequestHeaders.Add("x-vendorDTO-key", method.MethodAuthentication.APIkey);
        }
        if (method.MethodAuthentication.AuthenticationType == AuthenticationType.BasicAuth)
        {
            string authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{method.MethodAuthentication.Login}:{method.MethodAuthentication.Password}"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
        }
        if (method.MethodAuthentication.AuthenticationType == AuthenticationType.JWTBearer)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", method.MethodAuthentication.Token);
        }
        HttpResponseMessage response;
        if (method.MethodType == MethodType.Post)
        {
            try
            {
                response = client.PostAsync(api, content).Result;
            }
            catch
            { 
                response = null;    
            }
        }
        else
        {
            try
            { 
            response = await client.GetAsync(api);
            }
            catch
            {
                response = null;
            }
        } 

        DealsDTO vendorDealDTO = new();
        if (vendor.Type == VendorType.Brand)
        {
            vendorDealDTO.Id = vendor.VendorId;
            vendorDealDTO.Name = vendor.Name;
            vendorDealDTO.Logo = vendor.Logo;
            vendorDealDTO.AndLink = vendor.AndLink;
            vendorDealDTO.IOSLink = vendor.IOSLink;
            vendorDealDTO.WebLink = vendor.WebLink;
            vendorDealDTO.Type = vendor.Type;
            vendorDealDTO.Description = vendor.Description;
            vendorDealDTO.OtherDescription = vendor.OtherDescription;
            vendorDealDTO.Aggregator = "Internal";

            vendorDealDTO.VendorDeliverys = vendor.VendorDeliverys;
            vendorDealDTO.VendorMealTimings = vendor.VendorMealTimings;
            vendorDealDTO.VendorMealTypes = vendor.VendorMealTypes;
            vendorDealDTO.VendorKitchenTypes = vendor.VendorKitchenTypes;
            vendorDealDTO.VendorFoodTypes = vendor.VendorFoodTypes;
        }
        if (response!=null && response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            var responseJson = JToken.Parse(responseBody);
            var deals = new List<DealsDTO>();
            var deal = new DealsDTO();
            bool readyToAdd = false;
            foreach (var item in responseJson)
            {
                foreach (var parameter in parametersOUT)
                {
                    if (item.Path.Equals(parameter.Key))
                    {
                        switch (parameter.MatchWithKey)
                        {
                            case Matching.Fee:
                                vendorDealDTO.Fees = item.First.ToString();
                                break;
                            case Matching.MinimumAmmount:
                                vendorDealDTO.MinimumAmmount = item.First.ToString();
                                break;
                            case Matching.TimeFrom:
                                vendorDealDTO.TimeEstimationFrom = item.First.ToString();
                                break;
                            case Matching.TimeTo:
                                vendorDealDTO.TimeEstimationTo = item.First.ToString();
                                break;
                            case Matching.Rating:
                                vendorDealDTO.Rating = item.First.ToString();
                                break;
                            case Matching.Distance:
                                vendorDealDTO.Distance = item.First.ToString();
                                break;
                            case Matching.BrandName:
                                vendorDealDTO.brandName = item.First.ToString();
                                break;
                            case Matching.BrandId:
                                vendorDealDTO.BrandId = item.First.ToString();
                                break;
                            case Matching.Aggregator:
                                var aggregator = item.First;
                                var agg = aggregator.ToString();
                                    JToken brandList;
                                    //get BrandList parameter out
                                    IntegrationParameterDTO brandListparam = parametersOUT.Find(p => p.MatchWithKey == Matching.BrandList);

                                    if (brandListparam == null)
                                    {
                                        if (aggregator.Count() != 0)
                                        {
                                            brandList = aggregator;
                                            var brandItem = brandList.First;
                                            vendorDealDTO.Logo = vendor.Logo;
                                            vendorDealDTO.Type = vendor.Type;
                                            vendorDealDTO.DeliveryZone = deliveryZone.RegionName;
                                            vendorDealDTO.Fees = getMatchingKey(parametersOUT, Matching.Fee, brandItem.Children().ToList());
                                            vendorDealDTO.MinimumAmmount = getMatchingKey(parametersOUT, Matching.MinimumAmmount, brandItem.Children().ToList());
                                            vendorDealDTO.TimeEstimationFrom = getMatchingKey(parametersOUT, Matching.TimeFrom, brandItem.Children().ToList());
                                            vendorDealDTO.TimeEstimationTo = getMatchingKey(parametersOUT, Matching.TimeTo, brandItem.Children().ToList());
                                            vendorDealDTO.Rating = getMatchingKey(parametersOUT, Matching.Rating, brandItem.Children().ToList());
                                            vendorDealDTO.Distance = getMatchingKey(parametersOUT, Matching.Distance, brandItem.Children().ToList());
                                            vendorDealDTO.brandName = getMatchingKey(parametersOUT, Matching.BrandName, brandItem.Children().ToList());
                                            vendorDealDTO.Name = vendorDealDTO.brandName;
                                            readyToAdd = true;
                                        }

                                    }
                                    else
                                        {
                                        string brandListkey = brandListparam.Key;
                                        brandList = aggregator[brandListkey];
                                        foreach (var brandItem in brandList)
                                        {
                                            vendorDealDTO = new DealsDTO();
                                            vendorDealDTO.Name = vendor.Name;
                                            vendorDealDTO.Type = vendor.Type;
                                            vendorDealDTO.DeliveryZone = deliveryZone.RegionName;
                                        var Fees = brandItem.Children().Where(p => p is JProperty && ((JProperty)p).Name.ToString() == "Fees");
                                        if( Fees.Count()>0)
                                            vendorDealDTO.Fees = Fees.Select(p => ((JProperty)p).Value).FirstOrDefault().ToString();

                                        var MinimumAmmount = brandItem.Children().Where(p => p is JProperty && ((JProperty)p).Name.ToString() == "MinimumAmmount");
                                        if(MinimumAmmount.Count() > 0)
                                            vendorDealDTO.MinimumAmmount = MinimumAmmount.Select(p => ((JProperty)p).Value).FirstOrDefault().ToString();

                                        var TimeEstimationFrom = brandItem.Children().Where(p => p is JProperty && ((JProperty)p).Name.ToString() == "TimeEstimationFrom");
                                        if(TimeEstimationFrom.Count() > 0)
                                            vendorDealDTO.TimeEstimationFrom = TimeEstimationFrom.Select(p => ((JProperty)p).Value).FirstOrDefault().ToString();

                                        var TimeEstimationTo = brandItem.Children().Where(p => p is JProperty && ((JProperty)p).Name.ToString() == "TimeEstimationTo");
                                        if(TimeEstimationTo.Count() > 0)
                                            vendorDealDTO.TimeEstimationTo = TimeEstimationTo.Select(p => ((JProperty)p).Value).FirstOrDefault().ToString();
                                        var Rating = brandItem.Children().Where(p => p is JProperty && ((JProperty)p).Name.ToString() == "Rating");
                                        if (Rating.Count() > 0l) 
                                        vendorDealDTO.Rating = Rating.Select(p => ((JProperty)p).Value).FirstOrDefault().ToString();
                                        var Distance = brandItem.Children().Where(p => p is JProperty && ((JProperty)p).Name.ToString() == "Distance");
                                        if(Distance.Count() > 0)
                                            vendorDealDTO.Distance = Distance.Select(p => ((JProperty)p).Value).FirstOrDefault().ToString();
                                        var BrandId = brandItem.Children().Where(p => p is JProperty && ((JProperty)p).Name.ToString() == "id");
                                        if(BrandId.Count() > 0)
                                            vendorDealDTO.BrandId = BrandId.Select(p => ((JProperty)p).Value).FirstOrDefault().ToString();
                                        var BrandName = brandItem.Children().Where(p => p is JProperty && ((JProperty)p).Name.ToString() == "name");
                                        if(BrandName.Count() > 0)
                                        vendorDealDTO.brandName = BrandName.Select(p => ((JProperty)p).Value).FirstOrDefault().ToString();
                                            //get local brand id
                                            var brand = _brandMatchingRepository.Repository.Get(x => x.AggregatorId == vendor.VendorId && x.DistantBrandId.ToString() == vendorDealDTO.BrandId);
                                            if (brand != null)
                                            {
                                                var brandId = brand.LocalBrandId;
                                                vendorDealDTO.Id = brandId;                                                
                                            }
                                            deals.Add(vendorDealDTO);
                                        }
                                    }   
                                break;
                        }
                    }
                }
            }
            vendorDealDTO.DeliveryZone = deliveryZone.RegionName;
            if (vendorDealDTO.Type == VendorType.Brand && readyToAdd)
                    deals.Add(vendorDealDTO);
                    return deals;       
        }
    return null;
}
    private string getMatchingKey(List<IntegrationParameterDTO> parametersOUT, Matching matchKey, List<JToken> selectedToken,int newIndex = 0)
    {
        string value = string.Empty;
        string key = string.Empty;
        IntegrationParameterDTO Param = parametersOUT.Find(p => p.MatchWithKey == matchKey);
        if (Param != null)
        {
            key = Param.Key;
        }
                        
        var path = selectedToken.Find(p => p.Path == key);

        if (path != null)
        {
            value = path.Values().First().ToString();
        }else
        {
            /*int newIndex = 1; // Replace '0' with '1'*/

            int startIndex = key.IndexOf('[');
            int endIndex = key.IndexOf(']');

            if (startIndex != -1 && endIndex != -1)
            {
                string indexSubstring = key.Substring(startIndex + 1, endIndex - startIndex - 1);

                if (int.TryParse(indexSubstring, out int currentIndex))
                {
                    string newSubstring = newIndex.ToString();
                    key = key.Remove(startIndex + 1, indexSubstring.Length).Insert(startIndex + 1, newSubstring);

                    Console.WriteLine("Modified key: " + key);
                    path = selectedToken.Find(p => p.Path == key);
                    value = path.Values().First().ToString();
                }
                else
                {
                    Console.WriteLine("Invalid format, unable to modify.");
                }
            }
            else
            {
                Console.WriteLine("Invalid format, unable to modify.");
            }
         
        }

        /*
              var test = selectedToken.Where(p => p is JProperty && ((JProperty)p).Name.ToString() == key)
                                                 .Select(p => ((JProperty)p).Value).FirstOrDefault().ToString();*/



        return value;

    }
    private static async Task<List<DealsDTO>> CallApiByGet(string api, string token)
    {
        HttpClient client = new();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await client.GetAsync(api);
        if (response.IsSuccessStatusCode)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<DealsDTO>>(await response.Content.ReadAsStringAsync());
            }
            catch
            {
                return new List<DealsDTO>().DefaultIfEmpty().ToList();
            }

        }
        return new List<DealsDTO>().DefaultIfEmpty().ToList();
    }
    #region DEALS
    public async Task<List<DealsDTO>> GetDeals(AddressToSearchDTO addressToSearch, int languageKey)
    {
        List<ZoneDTO> DeliveryZoneSearched = new();
        /*  var cachedData = _cache.GetData<List<DealsDTO>>(_cacheKeyForDeals);
                if (cachedData != null && cachedData.Count != 0 && !addressToSearch.IsUpdated)
                 {
                     return cachedData;
                 }
                 else
                 {*/
        if (addressToSearch.ZoneName != null)
        {
                ZoneEntity zoneEntity = _zoneRepository.Repository.Get(z => z.LanguageResourceSet.LanguageRessource.First(l => l.LanguageKey == (LanguageKey)languageKey).Value.ToLower() == addressToSearch.ZoneName.ToLower());
                if(zoneEntity!=null)
                {
                    Zone zoneBM = _mapper.Map<Zone>(zoneEntity);
                    ZoneDTO zoneDTO = _mapper.Map<ZoneDTO>(zoneBM);
                    DeliveryZoneSearched.Add(zoneDTO);
                }    
        }
        else
        {
                DeliveryZoneSearched = FixSearchZone(addressToSearch, languageKey);
        }
            List<DealsDTO> Deals = new();
            List<DealsDTO> OrderedDeals = new();
            if (DeliveryZoneSearched.Any())
            {
                var zoneIds = DeliveryZoneSearched.Select(x => x.Id).ToList();
                var allDealsByZones = GetAllActiveData().Where(x => x.Zones.Any(y => zoneIds.Contains(y.ZoneId))).ToList();

                foreach (var item in allDealsByZones)
                {
                    List<DealsDTO> deals = new();
                    if (item.StaticIntegrations != null && item.StaticIntegrations.Any())
                    {
                        item.StaticIntegrations = item.StaticIntegrations?.Where(x => zoneIds.Contains(x.ZoneId)).ToList();
                        deals.AddRange(item.StaticIntegrations.Select(x => new DealsDTO()
                        {
                            Fees = x.Fees,
                            TimeEstimationFrom = x.TimeEstimation
                        }).ToList());
                    }
                    if (item.DynamicIntegrations != null && item.DynamicIntegrations.Any())
                    {
                        List<DealsDTO> dealsDTOs = await GetDealsFromDynamicIntegrations(item, addressToSearch, DeliveryZoneSearched);
                        if (dealsDTOs != null && dealsDTOs.Count() > 0 && dealsDTOs.Any(x => x != null))
                            Deals.AddRange(dealsDTOs);

                    }
                }
            }
            _cache.SetData(_cacheKeyForDeals, OrderedDeals, DateTimeOffset.UtcNow.AddDays(1));
            var aggregatorsDeals = Deals.Where(x => x.Type == VendorType.Aggregator).ToList();
            var brandDeals = Deals.Where(x => x.Type == VendorType.Brand).ToList();
            var finalDeals = GroupDealsByBrand(brandDeals, aggregatorsDeals);
            var orderedDeals = new List<DealsDTO>();
            if (finalDeals.Count() > 0)
            {
                orderedDeals = finalDeals.OrderBy(x => x.Fees).ThenByDescending(x => x.TimeEstimationFrom).ToList();
            }
            return orderedDeals;
       // }
    }
    #region PAGINATION/FILTER
    public PagedList<DealsDTO> GetPagedDeals(PagedParameters pagedParameters, AddressToSearchDTO addressToSearch, int languageKey)
    {
        var deals = GetDeals(addressToSearch, languageKey).Result;
        return PagedList<DealsDTO>.ToGenericPagedList(deals, pagedParameters);
    }
    public PagedList<DealsDTO> GetPagedFilteredDeals(PagedParameters pagedParameters, VendorFilter filter, int languageKey)
    {
        var deals = GetFilteredData(filter, languageKey).Result;
        return PagedList<DealsDTO>.ToGenericPagedList(deals, pagedParameters);
    }
    public async Task<List<DealsDTO>> GetFilteredData(VendorFilter filter, int languageKey)
    {
        AddressToSearchDTO addressToSerch = new AddressToSearchDTO
        {
            CityCode = filter.CityCode,
            CountryName = filter.CountryName,
            RegionName = filter.RegionName,
            ZoneName = filter.ZoneName,
            Latitude = filter.Latitude,
            Longitude = filter.Longitude
        };
        var deals = await GetDeals(addressToSerch, languageKey);
        var res = deals.Where(x =>
            (string.IsNullOrEmpty(filter.Name) || (x.Name?.ToLower().Contains(filter.Name.ToLower()) ?? false))
            && ((filter.DeliveryModeIds == null) || (x.VendorDeliverys?.Any(y => filter.DeliveryModeIds.Contains(y.DeliveryModeId)) ?? true))
            && ((filter.FoodTypeIds == null) || (x.VendorFoodTypes?.Any(y => filter.FoodTypeIds.Contains(y.FoodTypeId)) ?? false))
            && ((filter.KitchenTypeIds == null) || (x.VendorKitchenTypes?.Any(y => filter.KitchenTypeIds.Contains(y.KitchenTypeId)) ?? false))
            && ((filter.MealTimingIds == null) || (x.VendorMealTimings?.Any(y => filter.MealTimingIds.Contains(y.MealTimingId)) ?? false))
            && ((filter.MealTypeIds == null) || (x.VendorMealTypes?.Any(y => filter.MealTypeIds.Contains(y.MealTypeId)) ?? false))
        ).ToList();
        return res;
    }
    #endregion
    #endregion

    #region MAPPING OUTPUT TO MOBILE
    public static List<DealsDTO> GroupDealsByBrand(List<DealsDTO> brandDeals, List<DealsDTO> aggregatorDeals)
    {
        var aggregatorDealsByBrand = aggregatorDeals
       .Where(deal => !string.IsNullOrEmpty(deal.brandName))
       .Select(group =>
       {
           var groupedDeal = new DealsDTO
           {
               Id = group.Id,
               Name = group.brandName,
               OtherDescription = group.OtherDescription,
               Aggregator = group.Name,
               Rating = group.Rating,
               Fees = group.Fees,
               MinimumAmmount = group.MinimumAmmount,
               Distance = group.Distance,
               TimeEstimationFrom = group.TimeEstimationFrom,
               TimeEstimationTo = group.TimeEstimationTo,
               brandName = group.brandName,
               DeliveryZone = group.DeliveryZone,

           };

           return groupedDeal;
       })
       .ToList();
        if (brandDeals.ToList() != null && brandDeals.ToList().Count != 0)
        {
            List<DealsDTO> brandDealsCopy = new List<DealsDTO>(brandDeals);

            foreach (DealsDTO bDeal in brandDealsCopy)
            {
                bDeal.DealsInfos = new List<DealsDTO>();
                if (aggregatorDealsByBrand.ToList() != null && aggregatorDealsByBrand.ToList().Count != 0)
                {
                    List<DealsDTO> aggregatorDealsCopy = new List<DealsDTO>(aggregatorDealsByBrand);

                    foreach (DealsDTO aDeal in aggregatorDealsCopy)
                    {
                        if (bDeal.Id == aDeal.Id)
                        {
                            aDeal.AndLink = bDeal.AndLink;
                            aDeal.WebLink = bDeal.WebLink;
                            aDeal.Description = bDeal.Description;
                            aDeal.OtherDescription = bDeal.OtherDescription;
                            aDeal.VendorDeliverys = bDeal.VendorDeliverys;
                            aDeal.VendorFoodTypes = bDeal.VendorFoodTypes;
                            aDeal.VendorKitchenTypes = bDeal.VendorKitchenTypes;
                            aDeal.VendorMealTimings = bDeal.VendorMealTimings;
                            aDeal.VendorMealTypes = bDeal.VendorMealTypes;

                            bDeal.DealsInfos.Add(aDeal);
                            aggregatorDealsByBrand.Remove(aDeal);
                        }
                        if (aDeal.Id == Guid.Empty)
                        {
                            brandDeals.Add(aDeal);
                            aggregatorDealsByBrand.Remove(aDeal);
                        }
                    }
                }
            }
        }
        return brandDeals;
    }


    public Dictionary<string, List<DealsDTO>> GroupDealsByBrandName(List<DealsDTO> deals)
    {
        // Group deals by brandName using LINQ
        var groupedDeals = deals
            .Where(deal => !string.IsNullOrEmpty(deal.Logo)) // Change to the appropriate property for brandName
            .GroupBy(deal => deal.Logo) // Change to the appropriate property for brandName
            .ToDictionary(group => group.Key, group => group.SelectMany(deal => deal.DealsInfos).ToList());

        return groupedDeals;
    }

    #endregion
    #endregion

    #region Recommandation
    public List<RecommandationDTO> GetBrandRecommandations(IList<BrandActionSummaryDTO> BrandActionSummaryDTOList, AddressToSearchDTO addressToSearch, int languageKey)
    {
        var deals = GetDeals(addressToSearch, languageKey).Result.Where(x => x.Id != Guid.Empty).ToList();

        if (deals.Count == 0)
        {
            return null;
        }
        var y = 0.0;
        var RecommandationList = from deal in deals
                                 join brandActionSummary in BrandActionSummaryDTOList
                                 on deal.Id equals brandActionSummary.BrandModelId into brandActionSummaries
                                 from brandActionSummary in brandActionSummaries.DefaultIfEmpty()
                                 select new
                                 {
                                     brandId = deal.Id,
                                     brandName = deal.Name,
                                     brandLogo = deal.Logo,
                                     ViewDetailsCount = brandActionSummary?.ViewDetailsCount == null ? 0 : brandActionSummary.ViewDetailsCount,
                                     GoToAppCount = brandActionSummary?.GoToAppCount == null ? 0 : brandActionSummary.GoToAppCount,
                                     rating = calculateParamScore(deal, "Rating"),
                                     distance = calculateParamScore(deal, "Distance")

                                 };
        var recommandationDTOList = new List<RecommandationDTO>();
        foreach (var Recommandation in RecommandationList)
        {
            var distance = Recommandation.distance != 0.0 ? Recommandation.distance : 1;
            double BrandScore = (1 / distance) * 10 + Recommandation.rating * 0.1 +
                Recommandation.GoToAppCount * 0.2 + Recommandation.ViewDetailsCount * 0.1;

            RecommandationDTO recommandationDTO = new()
            {
                BrandModelId = Recommandation.brandId,
                BrandName = Recommandation.brandName,
                BrandLogo = Recommandation.brandLogo,
                RatingMean = Recommandation.rating,
                DistanceMean = Recommandation.distance!=10000.0 ? Recommandation.distance :-1,
                BrandScore = BrandScore,
            };
            recommandationDTOList.Add(recommandationDTO);
        }
        var orderedRecommandationDTOList = recommandationDTOList.OrderByDescending(x => x.BrandScore).ToList();
        return orderedRecommandationDTOList;
    }
    private double calculateParamScore(DealsDTO deal, string param)
    {
        if (param == "Rating")
        {
            var brandRating = 0.0;
            var brandRatingCount = 0;
            if (deal.Rating != "")
            {
                try
                {
                    brandRating = double.Parse(deal.Rating);
                }
                catch (Exception ex)
                {
                    brandRating = 0.0;
                }
                brandRatingCount = 1;
            }
            if (deal.DealsInfos != null)
            {
                foreach (var dealInfo in deal.DealsInfos)
                {
                    if (dealInfo.Rating != "")
                    {
                        try
                        {
                            brandRating = double.Parse(deal.Rating);
                            brandRatingCount += 1;
                        }
                        catch (Exception ex)
                        {
                            brandRating = 0.0;
                        }

                    }
                }
            }
            return brandRatingCount != 0 ? brandRating / brandRatingCount : 0.0;
        }
        else if (param == "Distance")
        {
            var brandDistance = 10000.0;
            var brandDistanceMin = 10000.0;
            if (deal.Distance != "")
            {
                try
                {
                    brandDistance = double.Parse(deal.Distance);
                    if (brandDistance < brandDistanceMin)
                    {
                        brandDistanceMin = brandDistance;
                    }
                }
                catch (Exception ex)
                {
                }
            }
            //if (deal.DealsInfos != null)
            //{
            //    foreach (var dealInfo in deal.DealsInfos)
            //    {
            //        if (dealInfo.Distance != "")
            //        {
            //            try
            //            {
            //                brandDistance = double.Parse(dealInfo.Distance);
            //                if (brandDistance < brandDistanceMin)
            //                {
            //                    brandDistanceMin = brandDistance;
            //                }
            //            }
            //            catch (Exception ex)
            //            {
            //            }
            //            }
            //        }
        //}
                return brandDistanceMin;
            }
            else
            {
                return 10000.0;
            }

        }

    #endregion

    #region matchingBrnads
    public async Task<List<DealsDTO>> GetBrandsToMatch(DynamicIntegrationDTO dynamicIntegration)
    {
        HttpClient client = new();
        var method = dynamicIntegration.IntegrationMethod.FirstOrDefault();
        if (dynamicIntegration.Authentication.AuthenticationType == AuthenticationType.APIKey)
        {
            client.DefaultRequestHeaders.Add("x-vendorDTO-key", method.MethodAuthentication.APIkey);
        }
        if (dynamicIntegration.Authentication.AuthenticationType == AuthenticationType.BasicAuth)
        {
            string authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{method.MethodAuthentication.Login}:{method.MethodAuthentication.Password}"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
        }
        if (dynamicIntegration.Authentication.AuthenticationType == AuthenticationType.JWTBearer)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", method.MethodAuthentication.Token);
        }
        //get query by dynamic integration
        var brands = new List<DealsDTO>();
        var query = _queryRepository.Repository.Get(q=>q.IdDynamicIntegration== dynamicIntegration.DynamicIntegrationId);

        HttpContent content = new StringContent("",Encoding.UTF8, "application/json");
        HttpResponseMessage response;
        if (query.Method == MethodType.Post)
        {
            try
            {
                response = client.PostAsync(query.Api, content).Result;
            }
            catch
            {
                response = null;
            }
        }
        else
        {
            try
            {
                response = await client.GetAsync(query.Api);
            }
            catch
            {
                response = null;
            }
        }
        if (response != null && response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            var responseJson = JToken.Parse(responseBody);
            
            bool readyToAdd = false;
            var parametersOUT = GetMethodParametersOUT(dynamicIntegration.IntegrationMethod.FirstOrDefault());
            int newIndex = 0;
            foreach (var item in responseJson.FirstOrDefault().First())
            {

                var brand = new DealsDTO();
                
                foreach (var parameter in parametersOUT)
                {
                    if(parameter.MatchWithKey == Matching.BrandName)
                    { 
                         if(item?.Count()>0)
                        {
                            brand.brandName = getMatchingKey(parametersOUT, Matching.BrandName, item?.ToList(), newIndex);
                        }
                    }
                    if (parameter.MatchWithKey == Matching.BrandId)
                    {
                        if (item?.Count() > 0)
                        {
                            brand.BrandId = getMatchingKey(parametersOUT, Matching.BrandId, item?.ToList(), newIndex);
                        }
                    }
                }
                brands.Add(brand);
                newIndex++;
            }
        }
        return brands;

    }

    #endregion

}
