//using Platform.ReferentialData.DataModel.POS.LocationData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Platform.ReferentialData.DataModel.LocationData
{
    [Table("Country")]
    public class CountryEntity : ReferentialDataBase
    {
        #region ID
        [Required]
        public int Id { get; set; }
        [Required]
        public Guid NameLabelCode { get; set; }
        #endregion

        #region Structure
        [Required]
        public string Code { get; set; }
        [Required]
        public string CountryKey { get; set; }
        #endregion

    }
}
