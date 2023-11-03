//using Platform.ReferentialData.DataModel.POS.LocationData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Platform.ReferentialData.DataModel.LocationData
{
    [Table("City")]
    public class CityEntity : ReferentialDataBase
    {
        #region ID
        [Required]
        public int Id { get; set; }
        [Required]
        public Guid NameLabelCode { get; set; }
        public virtual int RegionId { get; set; }
        [ForeignKey("RegionId")]
        public virtual RegionEntity RegionFk { get; set; }
        #endregion

        #region structure
        public string CityCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        #endregion
        [ForeignKey("LanguageResourceSetId")]
        public Guid LanguageResourceSetId { get; set; }
        public virtual LanguageResourceSetEntity LanguageResourceSet { get; set; }
        public virtual List<TagCityEntity> Tags { get; set; }
    }
}
