using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Platform.ReferentialData.DataModel.MealData;

namespace Platform.ReferentialData.EntityFramework.Configurations.Entities.DefaultMasterData.DefaultFilters;

public class DefaultMealTimings : IEntityTypeConfiguration<MealTimingEntity>
{
    public void Configure(EntityTypeBuilder<MealTimingEntity> builder)
    {
        builder.HasData(
            new MealTimingEntity
            {
                Id = 1,
                NameLabelCode = new Guid("ce646e7d-e307-4a12-a795-33004abb1bd8"),
                ImageLabelCode = new Guid("ce646e7d-e307-4a12-a795-33004abb1bd8"),
                Name = "BreakFast",
                LanguageResourceSetId = new Guid("d2ab81c5-b9b3-4b14-827e-d24da57d6ff4") 

            },
            new MealTimingEntity
            {
                Id = 2,
                NameLabelCode = new Guid("ce646e7d-e307-4a12-a795-33004abb1bd9"),
                ImageLabelCode = new Guid("ce646e7d-e307-4a12-a795-33004abb1bd9"),
                Name = "Lunch",
                LanguageResourceSetId = new Guid("e5a05881-856c-4453-99b2-74222677ceb7")

            },
            new MealTimingEntity
            {
                Id = 3,
                NameLabelCode = new Guid("ce646e7d-e307-4a12-a795-33004abb1bd7"),
                ImageLabelCode = new Guid("ce646e7d-e307-4a12-a795-33004abb1bd7"),
                Name = "Dinner",
                LanguageResourceSetId = new Guid("ef192566-67d7-4f85-b724-f8163019723a")

            }
        );
    }
}
