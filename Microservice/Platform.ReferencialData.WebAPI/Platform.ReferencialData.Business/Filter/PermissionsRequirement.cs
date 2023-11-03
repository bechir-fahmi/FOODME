using Microsoft.AspNetCore.Authorization;

namespace Platform.ReferencialData.Business.Filter
{
    public class PermissionsRequirement : IAuthorizationRequirement
    {
        public string Permission { get; private set; }

        public PermissionsRequirement(string permission)
        {
            Permission = permission;
        }
    }
}
