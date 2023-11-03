using CloudinaryDotNet.Actions;
using Platform.ReferencialData.BusinessModel.UserAddressData;
using Platform.ReferencialData.DataModel.UserData;
using Platform.Shared.Enum;

namespace Platform.ReferencialData.BusinessModel.Authentification
{
    public class User
    {
        #region User Data
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Picture { get; set; } = string.Empty;
        public string Gender { get; set; }
        public string Age { get; set; }
        public string UserType { get; set; }
        public string AssignedTo { get; set; }
        public string Location { get; set; }
        public string NumberOfOperation { get; set; }
        public string RequestCode { get; set; }
        public string lastActiveDate { get; set; }
        public DateTime? RequestTime { get; set; }
        public DateTime? ApprovalTime { get; set; }
        public DateTime? RejectTime { get; set; }

        public virtual List<UserAddress>? UserAddresses { get; set; }
        #endregion

        #region Base Data
        public Status Status { get; set; }

        public DateTime CreationTime { get; set; }
        public string CreatorUserId { get; set; }

        public string DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }

        public string? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        #endregion

        #region Security
        public AuthentificationSource AuthentificationSource { get; set; }
        public string MacAddress { get; set; }
        public string FCMToken { get; set; }
        #endregion

        public bool TwoFactorEnabled { get; set; }
        public string Password { get; set; }

        public virtual List<Role> Roles { get; set; }

    }
}
