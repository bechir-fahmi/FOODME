using Platform.Shared.Enum;

namespace Platform.FoodMe.Admin.ViewModels.ViewModels.AuthenticationData;

public class RoleVM
{
    public string Id { get; set; }

    public string Name { get; set; }

    public Status Status { get; set; }

    public DateTime CreationTime { get; set; }

    public string CreatorUserId { get; set; }

    public string DeleterUserId { get; set; }

    public DateTime? DeletionTime { get; set; }

    public string? LastModifierUserId { get; set; }

    public DateTime? LastModificationTime { get; set; }

    public IList<RoleClaimVM> Claims { get; set; }
}

public class CreateRoleVM
{
    public string Name { get; set; }
    public IList<RoleClaimVM> Claims { get; set; }
}
