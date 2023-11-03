using Platform.GenericRepository;
using Platform.ReferentialData.DtoModel.Authentification;
using Platform.Shared.Enum;

namespace Platform.ReferencialData.Business.business_services.Authentication
{
    public interface IUserService 
    {
        List<UserDTOInfo> GetAllUsersAsync();
        PagedList<UserDTOInfo> GetAllUsersAsync(PagedParameters pagedParameters);
        PagedList<UserDTOInfo> GetUsersByUserTypeAsync(PagedParameters pagedParameters, string userType);
        UserDTOInfo GetUserAsync(string id);
        UserDTO GetUser(string id);
        UserDTO GetUserByPhoneNumber(string id);
        Task<ResponseDTO> AddUser(UserDTO userDTO);
        Task<ResponseDTO> AddUserByAdmin(UserDTO userDTO);

        Task<ResponseDTO> Update(UserDTO userDTO);
        Task<ResponseDTO> UpdateUserByAdmin(UserDTO userDTO);
        void Update(string id, Status status);
        Task Delete(string id);
        Task<IList<UserDTOInfo>> GetAllUsersWithGenderAsync();
        Task<IList<UserDTO>> GetCustomerLastweekAsync();
        Task<IList<UserDTO>> GetCustomerLastDayAsync();
    }
}
