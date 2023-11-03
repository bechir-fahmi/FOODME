using Platform.Shared.Permissions;

namespace Platform.ReferentialData.DtoModel.Authentification
{
    public class RoleClaimDTO
    {
        public Modules ClaimType { get; set; }

        public List<CRUDPermissions> ClaimValue { get; set; }
    }
}
