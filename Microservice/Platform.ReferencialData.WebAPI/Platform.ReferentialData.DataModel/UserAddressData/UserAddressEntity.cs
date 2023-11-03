using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Platform.ReferentialData.DataModel.UserData;
using Platform.Shared.Enum;

namespace Platform.ReferentialData.DataModel.UserAddressData
{
    public class UserAddressEntity : ReferentialDataAddressBase
    {
        #region ID Data

        [Required]
        public int Id { get; set; }


        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual UserEntity User { get; set; }
        [ForeignKey("UserAddressTypeId")]
        public int UserAddressTypeId { get; set; }
        #endregion

        #region Data

        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public AddressType AddressType { get; set; }
        public string FullAddress { get; set; }
        #endregion

    }
}
