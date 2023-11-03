using Platform.ReferentialData.DtoModel.Authentification;
using System.Security.Claims;

namespace Platform.ReferencialData.Business.business_services.Authentication
{
    public interface IRoleClaimService
    {
        Task<IList<Claim>> GetAllClaimsFromRole(string roleId);

        Task<ResponseDTO> AddClaimsToRole(string roleId, IList<RoleClaimDTO> claims);

        Task<ResponseDTO> UpdateClaimsInRole(RoleDTO roleDTO);

        Task<ResponseDTO> DeleteClaimsFromRole(RoleDTO roleDTO);
    }
}
