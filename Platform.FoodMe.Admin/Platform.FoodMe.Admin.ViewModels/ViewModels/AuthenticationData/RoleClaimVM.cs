using Platform.Shared.Permissions;

namespace Platform.FoodMe.Admin.ViewModels.ViewModels.AuthenticationData;

public class RoleClaimVM
{
    public Modules ClaimType { get; set; }
    public List<CRUDPermissions> ClaimValue { get; set; }
}
