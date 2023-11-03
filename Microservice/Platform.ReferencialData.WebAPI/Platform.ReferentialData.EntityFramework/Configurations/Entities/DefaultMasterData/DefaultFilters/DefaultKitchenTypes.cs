using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Platform.ReferentialData.DataModel.KitchenTypeData;

namespace Platform.ReferentialData.EntityFramework.Configurations.Entities.DefaultMasterData.DefaultFilters;

public class DefaultKitchenTypes : IEntityTypeConfiguration<KitchenTypeEntity>
{
    public void Configure(EntityTypeBuilder<KitchenTypeEntity> builder)
    {
        builder.HasData(
            new KitchenTypeEntity
            {
                Id = 1,
                NameLabelCode = new Guid("ce646e7d-e307-4a12-a795-33004abb1bd8"),
                ImageLabelCode = new Guid("ce646e7d-e307-4a12-a795-33004abb1bd8"),
                Name = "American",
                LanguageResourceSetId = new Guid("099ae5f8-6bd2-4ee2-667e-a9d44449f4ca") 

            },
            new KitchenTypeEntity
            {
                Id = 2,
                NameLabelCode = new Guid("ce646e7d-e307-4a12-a795-33004abb1bd9"),
                ImageLabelCode = new Guid("ce646e7d-e307-4a12-a795-33004abb1bd9"),
                Name = "Italian",
                LanguageResourceSetId = new Guid("088ae5f8-6bd2-4ee2-777e-a9d44449f4ca") 

            },
            new KitchenTypeEntity
            {
                Id = 3,
                NameLabelCode = new Guid("ce646e7d-e307-4a12-a795-33004abb1bd7"),
                ImageLabelCode = new Guid("ce646e7d-e307-4a12-a795-33004abb1bd7"),
                Name = "Indian",
                LanguageResourceSetId = new Guid("099ae5f8-6bd2-4ee2-777e-a9d44449f4ef") 

            }

        );
    }
}
