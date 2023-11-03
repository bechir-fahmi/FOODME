using Platform.Tracking.DtoModel.UserStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Tracking.Business.Business_Service
{
    public interface IUserStatusService
    {
        List<UserStatusDTO> GetAllUserStatus();
        void AddActiveUser(UserStatusDTO userStatusDto, bool updateCache = true);
        void UpdateLogOutUser(UserStatusDTO userStatusDto, bool updateCache = true);
        List<UserStatusDTO> GetAllUserByStatus(bool status);
        List<UserStatusDTO> GetAllUserByStatusAndPeridique(Boolean status, DateTime startDate, DateTime endDate);

    }
}
