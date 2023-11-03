using Platform.ReferentialData.DtoModel.Authentification;

namespace Platform.ReferencialData.Business.business_services.Authentication
{
    public interface IUserRoleService
    {
        Task<ResponseDTO> AddUserToRole(string userId, string roleId);
        Task<IList<RoleDTO>> GetUserRoles(string userId);

        Task<ResponseDTO> RemoveUserFromRole(string userId, string roleId);
    }
}
