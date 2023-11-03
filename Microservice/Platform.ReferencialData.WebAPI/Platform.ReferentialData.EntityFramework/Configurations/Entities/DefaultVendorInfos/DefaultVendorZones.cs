using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Platform.ReferentialData.DataModel;

namespace Platform.ReferentialData.EntityFramework.Configurations.Entities.DefaultBrands;

public class DefaultVendorZones : IEntityTypeConfiguration<VendorDeliveryZonesEntity>
{
    public void Configure(EntityTypeBuilder<VendorDeliveryZonesEntity> builder)
    {
        builder.HasData(
                new VendorDeliveryZonesEntity()
                {
                    VendorId = new Guid("ce748e7d-e307-4a12-a795-33004abb1bd8"),
                    ZoneId = 1
                },
                new VendorDeliveryZonesEntity
                {
                    VendorId = new Guid("3a7b3888-9b89-459a-a108-e06aefec5500"),
                    ZoneId = 1
                },
                new VendorDeliveryZonesEntity
                {
                    VendorId = new Guid("ce646e7d-e307-4a12-a795-33004abb1bd8"),
                    ZoneId = 1
                }
        );
    }
}