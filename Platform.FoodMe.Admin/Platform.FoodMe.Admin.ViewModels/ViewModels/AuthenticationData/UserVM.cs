using Platform.FoodMe.Admin.ViewModels.ViewModels.UserAddressData;
using Platform.ReferencialData.BusinessModel.UserAddressData;
using Platform.ReferencialData.DataModel.UserData;
using Platform.Shared.Enum;

namespace Platform.FoodMe.Admin.ViewModels.ViewModels.AuthenticationData;

public class UserVM
{
    #region User Data
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;

    public string FullName { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string Picture { get; set; } = string.Empty;

    public List<UserAddress> UserAddresses { get; set; }
    #endregion

    #region Base Data
    public Status Status { get; set; }

    public DateTime? CreationTime { get; set; }
    public string CreatorUserId { get; set; }

    public string DeleterUserId { get; set; }
    public DateTime? DeletionTime { get; set; }

    public string LastModifierUserId { get; set; }
    public DateTime? LastModificationTime { get; set; }
    #endregion

    #region Security
    public AuthentificationSource AuthentificationSource { get; set; }
    public string MacAddress { get; set; }
    public string FCMToken { get; set; }
    #endregion

    public bool TwoFactorEnabled { get; set; }
    public string Password { get; set; }

    public List<RoleVM> Roles { get; set; }

}



public class CreateOrEditUserDto
{
    public string Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string Picture { get; set; } = string.Empty;
    public DateTime CreationTime { get; set; }
    public string MacAddress { get; set; }
}
