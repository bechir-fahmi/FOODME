using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Platform.ReferentialData.DataModel.FoodTypeData;
using Platform.ReferentialData.DataModel;

namespace Platform.ReferentialData.EntityFramework.Configurations.Entities.DefaultMasterData.DefaultFilters;

public class DefaultFoodTypes : IEntityTypeConfiguration<FoodTypeEntity>
{
    public void Configure(EntityTypeBuilder<FoodTypeEntity> builder)
    {
        builder.HasData(
            new FoodTypeEntity
            {
                Id = 1,
                NameLabelCode = new Guid("ce646e7d-e307-4a12-a795-33004abb1bd8"),
                ImageLabelCode = new Guid("ce646e7d-e307-4a12-a795-33004abb1bd8"),
                Name = "Fish",
                LanguageResourceSetId = new Guid("099ae5f8-6bd2-4ee2-887e-a9d44449f4ce")
                
            },
            new FoodTypeEntity
            {
                Id = 2,
                NameLabelCode = new Guid("ce646e7d-e307-4a12-a795-33004abb1bd9"),
                ImageLabelCode = new Guid("ce646e7d-e307-4a12-a795-33004abb1bd9"),
                Name = "Pizza",
                LanguageResourceSetId = new Guid("13488271-3653-41a0-9287-dc65ab6d2d7e")

            },
            new FoodTypeEntity
            {
                Id = 3,
                NameLabelCode = new Guid("ce646e7d-e307-4a12-a795-33004abb1bd7"),
                ImageLabelCode = new Guid("ce646e7d-e307-4a12-a795-33004abb1bd7"),
                Name = "Meat",
                LanguageResourceSetId = new Guid("310363c2-3044-47c7-81ea-fababb60d3dc") 

            }

        );
    }
}
