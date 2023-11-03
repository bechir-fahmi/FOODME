using Platform.ReferentialData.DataModel.LocationData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Platform.ReferentialData.DataModel;

[Table("Zones")]
public class ZoneEntity : ReferentialDataBase
{
    [Key]
    [Required]
    public int Id { get; set; }
    public string Name { get; set; }
    [Required]
    [ForeignKey("CountryId")]
    public virtual CountryEntity Country { get; set; }
    public int CountryId { get; set; }
    [Required]
    [ForeignKey("CityId")]
    public virtual CityEntity City { get; set; }
    public int CityId { get; set; }
    [Required]
    [ForeignKey("RegionId")]
    public virtual RegionEntity Region { get; set; }
    public int RegionId { get; set; }
    [Required]
    [ForeignKey("AreaId")]
    public virtual AreaEntity Area { get; set; }
    public int AreaId { get; set; }
    [ForeignKey("LanguageResourceSetId")]
    public Guid LanguageResourceSetId { get; set; }
    public virtual LanguageResourceSetEntity LanguageResourceSet { get; set; }
    public virtual List<TagZoneEntity> Tags { get; set; }
    
}
