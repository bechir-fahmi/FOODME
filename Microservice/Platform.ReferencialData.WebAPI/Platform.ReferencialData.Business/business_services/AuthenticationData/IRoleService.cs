using Platform.GenericRepository;
using Platform.ReferentialData.DtoModel.Authentification;

namespace Platform.ReferencialData.Business.business_services.Authentication
{
    public interface IRoleService
    {
        Task<IList<RoleDTO>> GetAllRoles();

        Task<PagedList<RoleDTO>> GetAllRoles(PagedParameters pagedParameters);

        Task<RoleDTO> GetRole(string id);

        Task<RoleDTO> GetRoleByName(string name);

        Task<ResponseDTO> CreateRole(CreateRoleDTO roleDTO, string userId);

        Task<ResponseDTO> UpdateRole(RoleDTO roleDTO);

        Task<ResponseDTO> DeleteRole(string id);
        List<RoleDTO> GetRolesByUserName(string username);
    }
}
