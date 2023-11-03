using AutoMapper;
using Platform.GenericRepository;
using Platform.ReferencialData.Business.business_services.DeliveryModeData;
using Platform.ReferencialData.Business.business_services.LanguageData;
using Platform.ReferencialData.BusinessModel.DeliveryModeData;
using Platform.ReferencialData.GenericRepository;
using Platform.ReferentialData.DataModel.DeliveryModeData;
using Platform.ReferentialData.DtoModel.DeliveryModeData;
using Platform.Shared.Cache;
using Platform.Shared.Enum;
using System.Linq.Expressions;

namespace Platform.ReferencialData.Business.business_services_implementations.DeliveryModeData
{
    public class DeliveryModeService : IDeliveryModeService
    {
        private readonly IUnitOfWork<DeliveryModeEntity> _deliveryModeRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly ILanguageResourceService _languageResourceService;
        private readonly string _cacheKey = CacheKey.DeliverModeCaheKey;

        public DeliveryModeService(IUnitOfWork<DeliveryModeEntity> deliveryModeRepository, IMapper mapper, ICacheService cache, ILanguageResourceService languageResourceService)
        {
            _deliveryModeRepository = deliveryModeRepository;
            _mapper = mapper;
            _cache = cache;
            _languageResourceService = languageResourceService;
        }

        public void Add(DeliveryModeDTO refDataDTO, bool updateCache = true)
        {
            DeliveryMode deliveryModeBM = _mapper.Map<DeliveryMode>(refDataDTO);
            DeliveryModeEntity deliveryMode = _mapper.Map<DeliveryModeEntity>(deliveryModeBM);
            _deliveryModeRepository.Repository.Attach(deliveryMode);
            _deliveryModeRepository.Save();
            if (updateCache)
               _cache.RemoveData(_cacheKey);
        }
        public DeliveryModeDTO Get(int id)
        {
            var deliveryModeDTO = GetAll().FirstOrDefault(x => x.Id == id);
            return deliveryModeDTO;
        }
        public DeliveryModeDTO Get(Expression<Func<DeliveryModeEntity, bool>> expression)
        {
            Expression<Func<DeliveryModeDTO, bool>> exp = _mapper.Map<Expression<Func<DeliveryModeDTO, bool>>>(expression);
            List<DeliveryModeDTO> deliveryModeList = GetAll();
            DeliveryModeDTO deliveryMode = null;
            if (deliveryModeList != null && deliveryModeList.Count > 0)
            {
                deliveryMode = ((IQueryable<DeliveryModeDTO>)deliveryModeList).FirstOrDefault(exp);
            }

            return deliveryMode;
        }
        public PagedList<DeliveryModeDTO> GetAll(PagedParameters pagedParameters)
        {
            var deliveryModeDtoList = GetAll();

            return PagedList<DeliveryModeDTO>.ToGenericPagedList(deliveryModeDtoList, pagedParameters);
        }
        public List<DeliveryModeDTO> GetAll()
        {
            var cachedData = _cache.GetData<List<DeliveryModeDTO>>(_cacheKey);
            if (cachedData != null)
            {
                return cachedData;
            }
            else
            {
                var deliveryModeList = _deliveryModeRepository.Repository.GetAll();
                var deliveryModeBMList = _mapper.Map<IList<DeliveryMode>>(deliveryModeList);
                var deliveryModeDtoList = _mapper.Map<IList<DeliveryModeDTO>>(deliveryModeBMList);
                _cache.SetData(_cacheKey, deliveryModeDtoList, DateTimeOffset.UtcNow.AddDays(1));
                return (List<DeliveryModeDTO>)deliveryModeDtoList;
            }
        }
        public PagedList<DeliveryModeDTO> GetAllActiveData(PagedParameters pagedParameters)
        {
            var deliveryModeDtoList = GetAll().Where(x => x.Status == Status.isActive).ToList();

            return PagedList<DeliveryModeDTO>.ToGenericPagedList(deliveryModeDtoList, pagedParameters);
        }
        public void Remove(DeliveryModeDTO refDataDTO, bool updateCache = true)
        {
            DeliveryMode deliveryModeBM = _mapper.Map<DeliveryMode>(refDataDTO);
            DeliveryModeEntity deliveryMode = _mapper.Map<DeliveryModeEntity>(deliveryModeBM);
            deliveryMode.Status = Shared.Enum.Status.isDeleted;
            _deliveryModeRepository.Repository.Update(deliveryMode);
            _deliveryModeRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }
        public void Update(DeliveryModeDTO refDataDTO, bool updateCache = true)
        {
            DeliveryMode deliveryModeBM = _mapper.Map<DeliveryMode>(refDataDTO);
            DeliveryModeEntity deliveryMode = _mapper.Map<DeliveryModeEntity>(deliveryModeBM);
            _deliveryModeRepository.Repository.Update(deliveryMode);
            _deliveryModeRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }
        public void Remove(int refData, bool updateCache = true)
        {
            throw new NotImplementedException();
        }

        /* public List<DeliveryModeDTO> GetFilteredData(DeliveryModeFillter filter)
         {

             var refDataDtoList = GetAll();
             refDataDtoList = refDataDtoList.Where(x => (!filter.Status.HasValue || x.Status == filter.Status.Value)
             && (string.IsNullOrEmpty(filter.Name) || (x.nameImageLanguageRessource != null && x.nameImageLanguageRessource.Any(y => y.Value.ToLower().Trim().Contains(filter.Name.ToLower().Trim()))))).ToList();
             return refDataDtoList;
         }*/
    }
}