using AutoMapper;
using Platform.GenericRepository;
using Platform.ReferencialData.Business.business_services.LanguageData;
using Platform.ReferencialData.Business.business_services.UserAddressData;
using Platform.ReferencialData.BusinessModel.UserAddressData;
using Platform.ReferencialData.GenericRepository;
using Platform.ReferentialData.DataModel.UserAddressData;
using Platform.ReferentialData.DtoModel.UserAddressData;
using Platform.Shared.Cache;
using Platform.Shared.Enum;
using System.Linq.Expressions;

namespace Platform.ReferencialData.Business.business_services_implementations.UserAddressData
{
    public class UserAddressTypeService : IUserAddressTypeService
    {
        private readonly IUnitOfWork<UserAddressTypeEntity> _userAddressTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILanguageResourceService _languageResourceService;
        private readonly ICacheService _cache;
        private readonly string _cacheKey = CacheKey.UserAddressTypeCacheKey;

        public UserAddressTypeService(IUnitOfWork<UserAddressTypeEntity> userAddressTypeRepository,
            IMapper mapper,
            ILanguageResourceService languageResourceService,
            ICacheService cache)
        {
            _userAddressTypeRepository = userAddressTypeRepository;
            _languageResourceService = languageResourceService;
            _mapper = mapper;
            _cache = cache;
        }

        public List<UserAddressTypeDTO> GetAll()
        {
            var cachedData = _cache.GetData<List<UserAddressTypeDTO>>(_cacheKey);
            if (cachedData != null)
            {
                return cachedData;
            }
            else
            {
                IList<UserAddressTypeEntity> userAddressTypeEntities = _userAddressTypeRepository.Repository.GetAll();
                IList<UserAddressType> userAddressesType = _mapper.Map<IList<UserAddressType>>(userAddressTypeEntities);
                List<UserAddressTypeDTO> userAddressTypeDTO = (List<UserAddressTypeDTO>)_mapper.Map<IList<UserAddressTypeDTO>>(userAddressesType);
                _cache.SetData(_cacheKey, userAddressTypeDTO, DateTimeOffset.Now.AddHours(24));
                return userAddressTypeDTO;
            }
        }

        public PagedList<UserAddressTypeDTO> GetAll(PagedParameters pagedParameters)
        {
            var userAddressTypeList = GetAll();
            return PagedList<UserAddressTypeDTO>.ToGenericPagedList(userAddressTypeList, pagedParameters);
        }
        public PagedList<UserAddressTypeDTO> GetAllActiveData(PagedParameters pagedParameters)
        {
            var userAddressTypeList = GetAll().Where(x=>x.Status==Status.isActive).ToList();
            return PagedList<UserAddressTypeDTO>.ToGenericPagedList(userAddressTypeList, pagedParameters);
        }

        public UserAddressTypeDTO Get(int id)
        {
            List<UserAddressTypeDTO> userAddressTypeList = GetAll();
            UserAddressTypeDTO userAddressType = null;
            if (userAddressTypeList != null && userAddressTypeList.Count > 0)
            {
                userAddressType = GetAll().FirstOrDefault(x => x.Id == id);
            }

            return userAddressType;
        }

        public UserAddressTypeDTO Get(Expression<Func<UserAddressTypeEntity, bool>> expression)
        {
            Expression<Func<UserAddressTypeDTO, bool>> exp = _mapper.Map<Expression<Func<UserAddressTypeDTO, bool>>>(expression);

            List<UserAddressTypeDTO> userAddressTypeList = GetAll();
            UserAddressTypeDTO userAddressType = null;

            if (userAddressTypeList != null && userAddressTypeList.Count > 0)
            {
                userAddressType = ((IQueryable<UserAddressTypeDTO>)userAddressTypeList).FirstOrDefault(exp);
            }

            return userAddressType;
        }

        public void Add(UserAddressTypeDTO userAddressTypeDto, bool updateCache = true)
        {
            UserAddressType useraddressType = _mapper.Map<UserAddressType>(userAddressTypeDto);
            UserAddressTypeEntity userAddressTypeEntity = _mapper.Map<UserAddressTypeEntity>(useraddressType);
            _userAddressTypeRepository.Repository.Attach(userAddressTypeEntity);
            _userAddressTypeRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }

        public void Remove(int id, bool updateCache = true)
        {
            _userAddressTypeRepository.Repository.Delete(id);
            _userAddressTypeRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }

        public void Update(UserAddressTypeDTO refDataDTO, bool updateCache = true)
        {
            UserAddressType userAddressType = _mapper.Map<UserAddressType>(refDataDTO);
            UserAddressTypeEntity userAddressTypeEntity = _mapper.Map<UserAddressTypeEntity>(userAddressType);
            _userAddressTypeRepository.Repository.Update(userAddressTypeEntity);
            _userAddressTypeRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }

        public void Remove(UserAddressTypeDTO refDataDTO, bool updateCache = true)
        {
            throw new NotImplementedException();
        }
    }
}
