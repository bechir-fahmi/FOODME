using Microsoft.AspNetCore.Authorization;
using Platform.Shared.Enum;

namespace Platform.ReferencialData.Business.Filter
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionsRequirement>
    {
        public PermissionAuthorizationHandler() { }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionsRequirement requirement)
        {
            if (context.User == null)
            {
                return Task.CompletedTask;
            }
            var permissionAll= "Permissions.All." + requirement.Permission.Split(".")[2];
            var canAccess = context.User.Claims.Any(c => c.Type == "Permission" && c.Value == requirement.Permission) ||
                context.User.Claims.Any(c => c.Type == "Permission" && c.Value == permissionAll) || 
                context.User.IsInRole(DefaultRole.ADMINISTRATOR.ToString());
            if (canAccess)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
        
    }
}
