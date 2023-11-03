using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Platform.ReferentialData.DataModel;

namespace Platform.ReferentialData.EntityFramework.Configurations.Entities.DefaultBrands;

public class DefaultVendorFoodTypes : IEntityTypeConfiguration<VendorFoodTypeEntity>
{
    public void Configure(EntityTypeBuilder<VendorFoodTypeEntity> builder)
    {
        builder.HasData(
                new VendorFoodTypeEntity()
                {
                    VendorId = new Guid("ce748e7d-e307-4a12-a795-33004abb1bd8"),
                    FoodTypeId = 1
                },
                new VendorFoodTypeEntity
                {
                    VendorId = new Guid("3a7b3888-9b89-459a-a108-e06aefec5500"),
                    FoodTypeId = 2
                },
                new VendorFoodTypeEntity
                {
                    VendorId = new Guid("ce646e7d-e307-4a12-a795-33004abb1bd8"),
                    FoodTypeId = 3
                }
        );
    }
}