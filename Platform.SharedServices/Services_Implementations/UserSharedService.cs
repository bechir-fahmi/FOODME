using Platform.ReferentialData.DtoModel.Authentification;
using Platform.Shared.HttpHelpers;
using Platform.Shared.MicroservicesURLs;
using Platform.SharedServices.Services;

namespace Platform.SharedServices.Implementations
{
    public class UserSharedService : IUserSharedService
    {
        private readonly IHelper<UserDTO, UserDTO, UserDTO> _helper;

        public UserSharedService(IHelper<UserDTO, UserDTO, UserDTO> helper)
        {
            _helper = helper;
        }
        public UserDTO GetUserById(string id)
        {
            string endPoint = $"{Microservice.RefData}/User/GetUser/id/{id}";
            var user = _helper.Get(endPoint);
            return user;

        }

        public UserDTO GetUserByPhoneNumber(string phoneNumber)
        {
            string endPoint = $"{Microservice.RefData}/User/GetUserByPhoneNumber/{phoneNumber}";
            var user = _helper.Get(endPoint);
            return user;

        }

    }
}
