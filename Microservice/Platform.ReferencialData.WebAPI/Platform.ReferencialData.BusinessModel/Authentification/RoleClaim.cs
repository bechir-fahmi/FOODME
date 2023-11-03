using Platform.Shared.Permissions;

namespace Platform.ReferencialData.BusinessModel.Authentification
{
    public class RoleClaim
    {
        public Modules ClaimType { get; set; }

        public List<CRUDPermissions> ClaimValue { get; set; }
    }
}
