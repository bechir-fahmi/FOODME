using Platform.ReferencialData.BusinessModel.UserAddressData;
using Platform.ReferencialData.DataModel.UserData;
using Platform.ReferentialData.DtoModel.UserAddressData;
using Platform.Shared.Enum;
using System.ComponentModel.DataAnnotations;

namespace Platform.ReferentialData.DtoModel.Authentification
{

    public class UserDetailStatus
    {
        #region Base Data
        public Status Status { get; set; }
        public DateTime CreationTime { get; set; }
        public string CreatorUserId { get; set; }
        public string DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public int? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        #endregion
    }


    public class UserDTO
    {

        #region User Data
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Picture { get; set; } = string.Empty;
        public virtual List<UserAddress> UserAddresses { get; set; }
        public string UserType { get; set; }
        public string AssignedTo { get; set; }
        public string Location { get; set; }
        public string NumberOfOperation { get; set; }
        public string lastActiveDate { get; set; }
        public string RequestCode { get; set; }
        public DateTime? RequestTime { get; set; } = null;
        public DateTime? ApprovalTime { get; set; } = null;
        public DateTime? RejectTime { get; set; } = null;
        #endregion

        #region Base Data
        public Status Status { get; set; }
        public DateTime CreationTime { get; set; }
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
        public virtual List<RoleDTO> Roles { get; set; }

    }

    public class UserDTOInfo
    {

        public string Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; }

        public string FullName { get; set; }

        public string Picture { get; set; } = string.Empty;

        public Status Status { get; set; }

        public virtual List<UserAddressDTO> UserAddresses { get; set; }

        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public string UserType { get; set; }
        public string AssignedTo { get; set; }
        public string Location { get; set; }
        public string NumberOfOperation { get; set; }
        public string lastActiveDate { get; set; }
        public string RequestCode { get; set; }
        public DateTime? RequestTime { get; set; }
        public DateTime? ApprovalTime { get; set; }
        public DateTime? RejectTime { get; set; }
        public virtual List<RoleDTO> Roles { get; set; }


    }





}
