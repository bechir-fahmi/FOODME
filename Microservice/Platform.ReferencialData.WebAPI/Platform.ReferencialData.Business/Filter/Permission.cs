using Platform.Shared.Permissions;

namespace Platform.ReferencialData.Business.Filter
{
    public static class Permissions
    {
        public static List<string> GeneratePermissionsList(Modules module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.ViewAll",
                $"Permissions.{module}.View",
                $"Permissions.{module}.Create",
                $"Permissions.{module}.Update",
                $"Permissions.{module}.Delete",
            };
        }

        public static List<string> GeneratePermission(Modules module, List<CRUDPermissions> permissions)
        {
            List<string> permissionsList = new List<string>();

            foreach (CRUDPermissions permission in permissions)
            {
                if (permission == CRUDPermissions.None) continue;
                if (permission == CRUDPermissions.All) return GeneratePermissionsList(module);
                else
                {
                    permissionsList.Add($"Permissions.{module}.{permission}");
                }

            };
            return permissionsList;
        }

    }
}
