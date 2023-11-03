using AutoMapper;
using Platform.ReferencialData.GenericRepository;
using Platform.Shared.Cache;
using Platform.Tracking.Business.Business_Service;
using Platform.Tracking.BusinessModel.UserSearch;
using Platform.Tracking.DataModel.UserSearch;
using Platform.Tracking.DtoModel.UserSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Tracking.Business.Business_Service_Implementation
{
    public class UserSearchService : IUserSearchService
    {
        private readonly IUnitOfWork<UserSearchEntity> _userSearchRepository;
        private readonly IMapper _mapper;
   /*     private readonly ICacheService _cache;*/
        private readonly string _cacheKey = CacheKey.UserSearchCacheKey;

        public UserSearchService(IUnitOfWork<UserSearchEntity> userSearchRepository,IMapper mapper
            /*,ICacheService cache*/
            )
        {
            _userSearchRepository = userSearchRepository;
            _mapper = mapper;
            /*_cache = cache;*/

        }
        public void TrackUserSearch(UserSearchDTO userSearchDto, bool hasResults, bool updateCache = true)
        {
            UserSearch userSearch = _mapper.Map<UserSearch>(userSearchDto);
            userSearch.SearchTime = DateTime.Now;
            userSearch.HasResults = hasResults;

            UserSearchEntity userSearchEntity = _mapper.Map<UserSearchEntity>(userSearch);

            _userSearchRepository.Repository.Insert(userSearchEntity);
            _userSearchRepository.Save();

          /*  if (updateCache)
            {
                _cache.RemoveData(_cacheKey);
            }*/
        }

        public List<UserSearchDTO> GetAllUserSearches()
        {
           /* var cachedData = _cache.GetData<List<UserSearchDTO>>(_cacheKey);
            if (cachedData != null && cachedData.Count != 0)
            {
                return cachedData;
            }
            else*/
           // {
                IList<UserSearchEntity> userSearchEntities = _userSearchRepository.Repository.GetAll();
                IList<UserSearchDTO> userSearchDTOs = _mapper.Map<IList<UserSearchDTO>>(userSearchEntities);

               /* _cache.SetData(_cacheKey, userSearchDTOs, DateTimeOffset.UtcNow.AddDays(1));*/
                return userSearchDTOs.ToList();
            //}
        }

    }
}
