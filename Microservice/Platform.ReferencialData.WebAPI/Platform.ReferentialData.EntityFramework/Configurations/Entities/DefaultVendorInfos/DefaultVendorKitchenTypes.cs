using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Platform.ReferentialData.DataModel;

namespace Platform.ReferentialData.EntityFramework.Configurations.Entities.DefaultBrands;

public class DefaultVendorKitchenTypes : IEntityTypeConfiguration<VendorKitchenTypeEntity>
{
    public void Configure(EntityTypeBuilder<VendorKitchenTypeEntity> builder)
    {
        builder.HasData(
                new VendorKitchenTypeEntity()
                {
                    VendorId = new Guid("ce748e7d-e307-4a12-a795-33004abb1bd8"),
                    KitchenTypeId = 1
                },
                new VendorKitchenTypeEntity
                {
                    VendorId = new Guid("3a7b3888-9b89-459a-a108-e06aefec5500"),
                    KitchenTypeId = 2
                },
                new VendorKitchenTypeEntity
                {
                    VendorId = new Guid("ce646e7d-e307-4a12-a795-33004abb1bd8"),
                    KitchenTypeId = 3
                }
        );
    }
}