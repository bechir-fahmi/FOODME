using Microsoft.AspNetCore.Identity;
using Platform.Shared.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Platform.ReferentialData.DataModel.UserData
{
    [Table("Role")]
    public class RoleEntity : IdentityRole
    {

        #region Base Data
        public Status Status { get; set; }

        public string CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }

        public string DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }

        public int? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }


        #endregion
    }
}
