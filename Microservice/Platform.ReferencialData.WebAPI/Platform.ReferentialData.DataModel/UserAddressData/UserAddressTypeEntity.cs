using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Platform.ReferentialData.DataModel.UserAddressData
{
    [Table("UserAddressType")]
    public class UserAddressTypeEntity : ReferentialDataBase
    {
        #region ID Data

        [Required]
        public int Id { get; set; }
        [Required]
        public Guid NameLabelCode { get; set; }
        public virtual LanguageResourceSetEntity LanguageResourceSet { get; set; }
        #endregion
    }
}
