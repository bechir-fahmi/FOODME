using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Platform.ReferentialData.DataModel.DeliveryModeData;

namespace Platform.ReferentialData.EntityFramework.Configurations.Entities.DefaultMasterData.DefaultDeliveryMode;

public class DefaultDeliveryMode : IEntityTypeConfiguration<DeliveryModeEntity>
{
    public void Configure(EntityTypeBuilder<DeliveryModeEntity> builder)
    {
        builder.HasData(
            new DeliveryModeEntity
            {
                Id = 1,
                NameLabelCode = new Guid("ce646e7d-e307-4a12-a795-33004abb1bd8"),
                ImageLabelCode = new Guid("ce646e7d-e307-4a12-a795-33004abb1bd8"),
                Name = "Delivery",
            },
            new DeliveryModeEntity
            {
                Id = 2,
                NameLabelCode = new Guid("ce646e7d-e307-4a12-a795-33004abb1be9"),
                ImageLabelCode = new Guid("ce646e7d-e307-4a12-a795-33004abb1be9"),
                Name = "Carhop",
            }
            );
    }
}