using Microsoft.AspNetCore.Identity;
using Platform.ReferencialData.DataModel.UserData;
using Platform.ReferentialData.DataModel.UserAddressData;
using Platform.Shared.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Platform.ReferentialData.DataModel.UserData
{
    [Table("User")]
    public class UserEntity : IdentityUser
    {

        #region User Data
        public string FullName { get; set; }
        public string Picture { get; set; } = string.Empty;
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
        #endregion
        #region User Security
        public AuthentificationSource AuthentificationSource { get; set; }
        public string MacAddress { get; set; }
        public string FCMToken { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string ResetCode { get; set; }
        public DateTime? ResetCodeExpireTime { get; set; }
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

        public virtual ICollection<RefreshToken>? RefreshTokens { get; set; }

        public virtual ICollection<UserAddressEntity>? UserAddresses { get; set; }


    }
}
