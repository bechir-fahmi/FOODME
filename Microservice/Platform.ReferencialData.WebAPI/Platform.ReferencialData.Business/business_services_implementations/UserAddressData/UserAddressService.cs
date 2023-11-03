using AutoMapper;
using Platform.GenericRepository;
using Platform.ReferencialData.BusinessModel.UserAddressData;
using Platform.ReferencialData.GenericRepository;
using Platform.ReferentialData.DataModel.UserAddressData;
using Platform.ReferentialData.DtoModel.UserAddressData;
using System.Linq.Expressions;
using Platform.ReferencialData.Business.business_services.UserAddressData;
using Platform.Shared.Cache;
using Platform.Shared.Enum;

namespace Platform.ReferencialData.Business.business_services_implementations.UserAddressData
{
    public class UserAddressService : IUserAddressService
    {
        private readonly IUnitOfWork<UserAddressEntity> _userAddressRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly string _cacheKey = CacheKey.UserAddresCacheKey;

        public UserAddressService(IUnitOfWork<UserAddressEntity> userAddressRepository, IMapper mapper, ICacheService cache)
        {
            _userAddressRepository = userAddressRepository;
            _mapper = mapper;
            _cache = cache;
        }

        public void Add(UserAddressDTO refDataDTO, bool updateCache = true)
        {
            UserAddress useraddress = _mapper.Map<UserAddress>(refDataDTO);
            UserAddressEntity userAddressEntity = _mapper.Map<UserAddressEntity>(useraddress);
            _userAddressRepository.Repository.Attach(userAddressEntity);
            _userAddressRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);

        }

        public UserAddressDTO Get(int id)
        {
           var userAddressDTO=GetAll().FirstOrDefault(x=> x.Id == id);  
            return userAddressDTO;
        }

        public UserAddressDTO Get(Expression<Func<UserAddressEntity, bool>> expression)
        {
            UserAddressEntity userAddressEntity = _userAddressRepository.Repository.Get(expression);
            UserAddress userAddress = _mapper.Map<UserAddress>(userAddressEntity);
            UserAddressDTO userAddressDTO = _mapper.Map<UserAddressDTO>(userAddress);
            return userAddressDTO;
        }

        public List<UserAddressDTO> GetAll()
        {
            var cachedData = _cache.GetData<List<UserAddressDTO>>(_cacheKey);
            if (cachedData != null && cachedData.Count > 0)
            {
                return cachedData;
            }
            else
            {
                var userAddressList = _userAddressRepository.Repository.GetAll();
                var userAddressBMList = _mapper.Map<IList<UserAddress>>(userAddressList);
                var userAddressDtoList = _mapper.Map<IList<UserAddressDTO>>(userAddressBMList);
                _cache.SetData(_cacheKey, userAddressDtoList, DateTimeOffset.UtcNow.AddDays(1));
                return (List<UserAddressDTO>)userAddressDtoList;
            }

           
        }

        public PagedList<UserAddressDTO> GetAll(PagedParameters pagedParameters)
        {
            var userAddressDTO = GetAll();

            return PagedList<UserAddressDTO>.ToGenericPagedList(userAddressDTO, pagedParameters);

        }
        public PagedList<UserAddressDTO> GetAllActiveData(PagedParameters pagedParameters)
        {
            var userAddressDTO = GetAll().Where(x=>x.Status==Status.isActive).ToList();

            return PagedList<UserAddressDTO>.ToGenericPagedList(userAddressDTO, pagedParameters);

        }

        public List<UserAddressDTO> GetUserAddressByUserId(string userId)
        {
            var userAddressList = _userAddressRepository.Repository.GetAll(x => x.UserId == userId);
            var userAddressBMList = _mapper.Map<IList<UserAddress>>(userAddressList);
            var userAddressDtoList = _mapper.Map<IList<UserAddressDTO>>(userAddressBMList);
            return (List<UserAddressDTO>)userAddressDtoList;
        }

        public List<UserAddressDTO> GetUserAddressActiveByUserId(string userId)
        {
            List<UserAddressDTO> userAddressDTO = GetAll().Where(x => x.Status == Status.isActive && x.UserId == userId).ToList(); 
            return userAddressDTO;
        }

        public void Remove(UserAddressDTO refDataDTO, bool updateCache = true)
        {
            UserAddress userAddress = _mapper.Map<UserAddress>(refDataDTO);
            UserAddressEntity userAddressEntity = _mapper.Map<UserAddressEntity>(userAddress);
            userAddressEntity.Status = Shared.Enum.Status.isDeleted;
            _userAddressRepository.Repository.Update(userAddressEntity);
            _userAddressRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }

        public void Remove(int refData, bool updateCache = true)
        {
            throw new NotImplementedException();
        }

        public void Update(UserAddressDTO refDataDTO, bool updateCache = true)
        {
            UserAddress userAddress = _mapper.Map<UserAddress>(refDataDTO);
            UserAddressEntity userAddressEntity = _mapper.Map<UserAddressEntity>(userAddress);
            _userAddressRepository.Repository.Update(userAddressEntity);
            _userAddressRepository.Save();
            if (updateCache)
                _cache.RemoveData(_cacheKey);
        }
        
    }
}
