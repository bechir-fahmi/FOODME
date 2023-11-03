using Platform.ReferentialData.DtoModel.Authentification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.SharedServices.Services
{
    public interface IUserSharedService
    {
        public UserDTO GetUserById(string id);
        public UserDTO GetUserByPhoneNumber(string phoneNumber);
    }
}
