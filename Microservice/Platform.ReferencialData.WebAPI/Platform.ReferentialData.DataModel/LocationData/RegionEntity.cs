//using Platform.ReferentialData.DataModel.POS.LocationData;
using Platform.Shared.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Platform.ReferentialData.DataModel.LocationData
{
    [Table("Region")]
    public class RegionEntity : ReferentialDataBase
    {
        #region ID
        [Required]
        public int Id { get; set; }
        [Required]
        public Guid NameLabelCode { get; set; }
        public virtual int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public virtual CountryEntity CountryFk { get; set; }
        #endregion
        [ForeignKey("LanguageResourceSetId")]
        public Guid LanguageResourceSetId { get; set; }
        public virtual LanguageResourceSetEntity LanguageResourceSet { get; set; }
        public virtual List<TagRegionEntity> Tags { get; set; }
    }
}
