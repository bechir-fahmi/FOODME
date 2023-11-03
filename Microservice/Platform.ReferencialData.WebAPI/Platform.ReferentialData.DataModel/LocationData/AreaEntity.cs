
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Platform.ReferentialData.DataModel.LocationData
{
    [Table("Area")]
    public class AreaEntity : ReferentialDataBase
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public Guid NameLabelCode { get; set; }
        public int CityId { get; set; }
        [ForeignKey("CityId")]
        public virtual CityEntity CityFK { get; set; } 
        public string AreaName { get; set; }       
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
