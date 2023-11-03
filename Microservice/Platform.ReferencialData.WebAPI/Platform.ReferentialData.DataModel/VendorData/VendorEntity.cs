using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Platform.ReferentialData.DataModel.BrandData;
using Platform.ReferentialData.DataModel.BrandData.IntegrationBrand;
using Platform.Shared.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Platform.ReferentialData.DataModel;

[Table("Vendor")]
public class VendorEntity : ReferentialDataBase
{
    #region Informations
        [Key]
        [Required]
        public Guid VendorId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Logo { get; set; }
        public string AndLink { get; set; }
        public string IOSLink { get; set; }
        public string WebLink { get; set; }
        public VendorType Type { get; set; }
        public int? AdminScore { get; set; } = 0;
    #endregion

    #region Data
    #region Kitchen,Food and Meal
    public virtual ICollection<VendorDeliveryModeEntity> VendorDeliverys { get; set; } = new List<VendorDeliveryModeEntity>();
        public virtual ICollection<VendorFoodTypeEntity> VendorFoodTypes { get; set; } = new List<VendorFoodTypeEntity>();
        public virtual ICollection<VendorKitchenTypeEntity> VendorKitchenTypes { get; set; } = new List<VendorKitchenTypeEntity>();
        public virtual ICollection<VendorMealTimingEntity> VendorMealTimings { get; set; } = new List<VendorMealTimingEntity>();
        public virtual ICollection<VendorMealTypeEntity> VendorMealTypes { get; set; } = new List<VendorMealTypeEntity>();
        #endregion
        public string Description { get; set; }
        public string OtherDescription { get; set; }
    #endregion

    #region Zone
        public virtual ICollection<VendorDeliveryZonesEntity> Zones { get; set; } = new List<VendorDeliveryZonesEntity>();
    #endregion

    #region Vendor Integration
    public virtual ICollection<StaticIntegrationEntity> StaticIntegrations { get; set; } = new List<StaticIntegrationEntity>();
     public virtual ICollection<DynamicIntegrationEntity> DynamicIntegrations { get; set; } = new List<DynamicIntegrationEntity>();
    #endregion
}
