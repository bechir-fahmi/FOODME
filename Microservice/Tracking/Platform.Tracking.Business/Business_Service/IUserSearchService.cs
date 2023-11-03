using Platform.Tracking.DtoModel.UserSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Tracking.Business.Business_Service
{
    public interface IUserSearchService
    {
        void TrackUserSearch(UserSearchDTO userSearchDto, bool hasResults, bool updateCache = true);
        List<UserSearchDTO> GetAllUserSearches();
    }
}
