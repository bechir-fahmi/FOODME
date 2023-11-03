using Platform.GenericRepository;
using Platform.ReferencialData.BusinessModel;
using Platform.ReferentialData.DataModel.BrandData;
using Platform.ReferentialData.DtoModel;
using Platform.ReferentialData.DtoModel.BrandData;
using Platform.ReferentialData.DtoModel.BrandData.Integration;
using Platform.ReferentialData.DtoModel.BrandData.Recommandation;
using Platform.ReferentialData.DtoModel.QueryData;
using Platform.Shared.Enum;
using Platform.Tracking.DtoModel.BrandAction;

namespace Platform.ReferencialData.Business.business_services;

public interface IVendorService
{
    #region Vendor
    VendorDTO Get(Guid id, VendorType vendorType);
    public VendorDTO GetVendorByName(string name);
    PagedList<VendorDTO> Get(string tag, PagedParameters pagedParameters, VendorType vendorType);
    PagedList<VendorDTO> GetAll(PagedParameters pagedParameters, VendorType vendorType, string userId);
    List<VendorDTO> GetAll(VendorType vendorType);
    List<VendorDTO> GetAll();
    PagedList<VendorDTO> GetAllActiveData(PagedParameters pagedParameters, VendorType vendorType,string userId);
    void Remove(Guid id, VendorType vendorType, bool updateCache = true);
    void Update(VendorDTO refDataDTO, VendorType vendorType, bool updateCache = true);
    void Add(VendorDTO refDataDTO, bool updateCache = true);
    #endregion

    #region Deals
    Task<List<DealsDTO>> GetDeals(AddressToSearchDTO addressToSearch, int languageKey);
    Task<List<DealsDTO>> GetFilteredData(VendorFilter filter, int languageKey);
    PagedList<DealsDTO> GetPagedDeals(PagedParameters pagedParameters, AddressToSearchDTO addressToSearch, int languageKey);
    PagedList<DealsDTO> GetPagedFilteredDeals(PagedParameters pagedParameters, VendorFilter filter, int languageKey);
    #endregion

    #region Recommendations
    List<RecommandationDTO> GetBrandRecommandations(IList<BrandActionSummaryDTO> BrandActionSummaryDTOList, AddressToSearchDTO addressToSearch, int languageKey);
    #endregion
   
    #region General Informations
    VendorGeneralInformationDTO GetGeneralInformations(Guid id, VendorType vendorType);
    VendorDTO AddGeneralInformations(VendorGeneralInformationDTO refDataDTO, bool updateCache = true);
    VendorDTO UpdateGeneralInformations(VendorGeneralInformationDTO refDataDTO, bool updateCache = true);
    #endregion

    #region Vendor Data
    VendorDataDTO GetVendorData(Guid id, VendorType vendorType);
    void Add(VendorDataDTO refDataDTO, Guid vendorId,VendorType vendorType, bool updateCache = true);
    VendorDTO Update(VendorDataDTO refDataDTO, Guid vendorId, bool updateCache = true);
    #endregion

    #region Delivery Zones
    List<VendorDeliveryZoneDTO> GetVendorDeliveryZones(Guid id, VendorType vendorType);
    void Add(List<VendorDeliveryZoneDTO> refDataDTO, Guid vendorId, VendorType vendorType, bool updateCache = true);
    void Update(List<VendorDeliveryZoneDTO> refDataDTO, Guid vendorId, VendorType vendorType, bool updateCache = true);
    #endregion

    #region Static Integrations
    List<StaticIntegrationDTO> GetAllStaticIntegration(Guid id);
    StaticIntegrationDTO GetStaticIntegration(Guid id);
    void AddStaticIntegration(List<StaticIntegrationDTO> refDataDTO, Guid vendorId, VendorType vendorType, bool updateCache = true);
    void UpdateStaticIntegration(StaticIntegrationDTO refDataDTO, Guid vendorId, VendorType vendorType,bool updateCache = true);
    void RemoveStaticIntegration(Guid id, bool updateCache = true);
    #endregion

    #region Dynamic Integrations
    bool VendorHaveDynamicInetgration(Guid vendorId, VendorType vendorType);
    DynamicIntegrationEntity GetDynamicIntegration(Guid id);
    List<DynamicIntegrationDTO> GetDynamicIntegrationsByVendor(Guid vendorId,VendorType vendorType);
    void AddDynamicIntegration(DynamicIntegrationDTO refDataDTO, Guid vendorId, VendorType vendorType, QueryDTO queryDTO, bool updateCache = true);
    void UpdateDynamicIntegration(DynamicIntegrationDTO refDataDTO, Guid vendorId, bool updateCache = true);
    void RemoveDynamicIntegration(Guid id, bool updateCache = true);

    #endregion

    #region Brand Matching
    void MatchBrands(List<BrandMatchingDTO> brandMatchingDTO);
    Task<List<DealsDTO>> GetBrandsToMatch(DynamicIntegrationDTO dynamicIntegration);
    #endregion


}
