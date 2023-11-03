using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Platform.ReferentialData.DataModel.MealData;

namespace Platform.ReferentialData.EntityFramework.Configurations.Entities.DefaultMasterData.DefaultFilters;

public class DefaultMealTypes : IEntityTypeConfiguration<MealTypeEntity>
{
    public void Configure(EntityTypeBuilder<MealTypeEntity> builder)
    {
        builder.HasData(
            new MealTypeEntity
            {
                Id = 1,
                NameLabelCode = new Guid("ce646e7d-e307-4a12-a795-33004abb1bd8"),
                ImageLabelCode = new Guid("ce646e7d-e307-4a12-a795-33004abb1bd8"),
                Name = "Family Meal",
                LanguageResourceSetId = new Guid("3ee5766a-af6a-436b-b588-b9ef645867f0")
            },
            new MealTypeEntity
            {
                Id = 2,
                NameLabelCode = new Guid("ce646e7d-e307-4a12-a795-33004abb1bd9"),
                ImageLabelCode = new Guid("ce646e7d-e307-4a12-a795-33004abb1bd9"),
                Name = "Happy Meal",
                LanguageResourceSetId = new Guid("48178984-0325-469a-a611-6c29c5610f13")
          
            }

        );
    }
}
