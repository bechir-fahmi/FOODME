using Platform.FoodMe.Admin.ViewModels.ViewModels.LanguageData;
using System.ComponentModel.DataAnnotations;

namespace Platform.FoodMe.Admin.ViewModels.ViewModels.LocationData
{
    public class AreaVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public Guid NameLabelCode { get; set; }
        public virtual int CityId { get; set; }
        public string AreaCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int? POSAreaId { get; set; }
        public List<LanguageResourceVM> LanguageResources { get; set; }
    }
}
