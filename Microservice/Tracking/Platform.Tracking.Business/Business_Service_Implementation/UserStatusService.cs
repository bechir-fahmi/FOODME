using AutoMapper;
using Platform.ReferencialData.GenericRepository;
using Platform.Shared.Cache;
using Platform.Tracking.Business.Business_Service;
using Platform.Tracking.BusinessModel.BrandAction;
using Platform.Tracking.BusinessModel.UserStatus;
using Platform.Tracking.DataModel.BrandAction;
using Platform.Tracking.DataModel.UserStatus;
using Platform.Tracking.DtoModel.BrandAction;
using Platform.Tracking.DtoModel.UserStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Tracking.Business.Business_Service_Implementation
{
    public class UserStatusService : IUserStatusService
    {
        private readonly IUnitOfWork<UserStatusEntity> _userStatusRepository;
        private readonly IMapper _mapper;
        /*private readonly ICacheService _cache;*/
        private readonly string _cacheKey = CacheKey.UserStatusCacheKey;
        public UserStatusService(IUnitOfWork<UserStatusEntity> userStatusRepository, IMapper mapper
          /*  , ICacheService cache*/
            )
        {
            _userStatusRepository = userStatusRepository;
            _mapper = mapper;
            /*_cache = cache;*/
        }
        public List<UserStatusDTO> GetAllUserStatus()
        {
         /*   var cachedData = _cache.GetData<List<UserStatusDTO>>(_cacheKey);
            if (cachedData != null && cachedData.Count != 0)
            {
                return cachedData;
            }
            else
            {*/
                IList<UserStatusEntity> userStatusEntities = _userStatusRepository.Repository.GetAll();
                IList<UserStatus> UserStatus = _mapper.Map<IList<UserStatus>>(userStatusEntities);
                IList<UserStatusDTO> UserStatusDTOs = _mapper.Map<IList<UserStatusDTO>>(UserStatus);
             /*   _cache.SetData(_cacheKey, UserStatusDTOs, DateTimeOffset.UtcNow.AddDays(1));*/
                return (List<UserStatusDTO>)UserStatusDTOs;
            //}
        }

        public void AddActiveUser (UserStatusDTO userStatusDto, bool updateCache = true)
        {
            UserStatus userStatus = _mapper.Map<UserStatus>(userStatusDto);
            userStatus.DateLogin = DateTime.Now;
            userStatus.Status = true;
            UserStatusEntity userStatusEntity = _mapper.Map<UserStatusEntity>(userStatus);
            _userStatusRepository.Repository.Insert(userStatusEntity);
            _userStatusRepository.Save();
/*            if (updateCache)
            {
                _cache.RemoveData(_cacheKey);
            }*/
        }

        public void UpdateLogOutUser(UserStatusDTO userStatusDto, bool updateCache = true)
        {
            UserStatusEntity? userStatusEntity = _userStatusRepository.Repository.GetAll(user => user.UserId == userStatusDto.UserId).LastOrDefault();

            if (userStatusEntity != null)
            {
                userStatusEntity.DateLogout = DateTime.Now;
                userStatusEntity.Status = false;
                _userStatusRepository.Repository.Update(userStatusEntity);
                _userStatusRepository.Save();
             /*   if (updateCache)
                {
                    _cache.RemoveData(_cacheKey);
                }*/
            } 
        }

        public List<UserStatusDTO> GetAllUserByStatus(bool status)
        {
            List<UserStatusDTO> userStatusDTOs = GetAllUserStatus()
                .Where(user => user.Status == status).ToList();

            return userStatusDTOs;
        }    
        

        public List<UserStatusDTO> GetAllUserByStatusAndPeridique(Boolean status, DateTime startDate,DateTime endDate)
        {
            List<UserStatusDTO> userStatusDTOs = GetAllUserStatus()
                .Where(user => user.Status == status && user.DateLogin >= startDate && user.DateLogout <= endDate).ToList();

            return userStatusDTOs;
        }


    }
}
