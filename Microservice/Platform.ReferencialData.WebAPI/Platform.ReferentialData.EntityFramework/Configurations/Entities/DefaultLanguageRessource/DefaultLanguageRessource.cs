using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Platform.ReferentialData.DataModel.LanguageData;
using Platform.Shared.Enum;

namespace Platform.ReferentialData.EntityFramework.Configurations.Entities.DefaultLanguageRessourceSet;

public class DefaultLanguageRessource : IEntityTypeConfiguration<LanguageResourceEntity>
{
    public void Configure(EntityTypeBuilder<LanguageResourceEntity> builder)
    {
        builder.HasData(
            new LanguageResourceEntity
            {
                Id = 1,
                Code = new Guid("099ae5f8-6bd2-4ee2-887e-a9d44449f4ce"),
                Value = "Fish",
                LanguageResourceSetId = new Guid("099ae5f8-6bd2-4ee2-887e-a9d44449f4ce"),
                LanguageKey = LanguageKey.en
            },

            new LanguageResourceEntity
            {
                Id = 2,
                Code = new Guid("13488271-3653-41a0-9287-dc65ab6d2d7e"),
                Value = "Pizza",
                LanguageResourceSetId = new Guid("13488271-3653-41a0-9287-dc65ab6d2d7e"),
                LanguageKey = LanguageKey.en
            },
            new LanguageResourceEntity
            {
                Id = 3,
                Code = new Guid("310363c2-3044-47c7-81ea-fababb60d3dc"),
                Value = "Meat",
                LanguageResourceSetId = new Guid("310363c2-3044-47c7-81ea-fababb60d3dc"),
                LanguageKey = LanguageKey.en

            },
            new LanguageResourceEntity
            {
                Id = 4,
                Code = new Guid("310363c2-3044-47c7-81ea-fababb60d3dc"),
                Value = "Family Meal",
                LanguageResourceSetId = new Guid("3ee5766a-af6a-436b-b588-b9ef645867f0"),
                LanguageKey = LanguageKey.en

            },
            new LanguageResourceEntity
            {
                Id = 5,
                Code = new Guid("48178984-0325-469a-a611-6c29c5610f13"),
                Value = "Happy Meal",
                LanguageResourceSetId = new Guid("48178984-0325-469a-a611-6c29c5610f13"),
                LanguageKey = LanguageKey.en
                
            },
            new LanguageResourceEntity
            {
                Id = 6,
                Code = new Guid("d2ab81c5-b9b3-4b14-827e-d24da57d6ff4"),
                Value = "BreakFast",
                LanguageResourceSetId = new Guid("d2ab81c5-b9b3-4b14-827e-d24da57d6ff4"),
                LanguageKey = LanguageKey.en
            },
            new LanguageResourceEntity
            {
                Id = 7,
                Code = new Guid("e5a05881-856c-4453-99b2-74222677ceb7"),
                Value = "Lunch",
                LanguageResourceSetId = new Guid("e5a05881-856c-4453-99b2-74222677ceb7"),
                LanguageKey = LanguageKey.en
            },
            new LanguageResourceEntity
            {
                Id = 8,
                Code = new Guid("ef192566-67d7-4f85-b724-f8163019723a"),
                Value = "Dinner",
                LanguageResourceSetId = new Guid("ef192566-67d7-4f85-b724-f8163019723a"),
                LanguageKey = LanguageKey.en
            },
            new LanguageResourceEntity
            {
                Id = 9,
                Code = new Guid("074be5f8-6bd2-4ee2-667e-a9d44449f4ca"),
                Value = "American",
                LanguageResourceSetId = new Guid("099ae5f8-6bd2-4ee2-667e-a9d44449f4ca"),
                LanguageKey = LanguageKey.en
            },
            new LanguageResourceEntity
            {
                Id = 10,
                Code = new Guid("099ae5f8-6bd2-4ee2-777e-a9d44449f4ea"),
                Value = "Italian",
                LanguageResourceSetId = new Guid("088ae5f8-6bd2-4ee2-777e-a9d44449f4ca"),
                LanguageKey = LanguageKey.en
            },
            new LanguageResourceEntity
            {
                Id = 11,
                Code = new Guid("099ae5f8-6bd2-4ee2-887e-a9d44449f4ca"),
                Value = "Indian",
                LanguageResourceSetId = new Guid("099ae5f8-6bd2-4ee2-777e-a9d44449f4ef"),
                LanguageKey = LanguageKey.en
            },
            new LanguageResourceEntity
            {
                Id = 12,
                Code = new Guid("099ae5f8-6bd2-4ee2-887e-a9d44449f4fe"),
                Value = "Jeddah",
                LanguageResourceSetId = new Guid("099ae5f8-6bd2-4ee2-887e-a9d44449f4fe"),
                LanguageKey = LanguageKey.en
            },
            new LanguageResourceEntity
            {
                Id = 13,
                Code = new Guid("099ae5f7-6bd2-4ee2-887e-a9d44449f4f0"),
                Value = "Riadh",
                LanguageResourceSetId = new Guid("099ae5f7-6bd2-4ee2-887e-a9d44449f4f0"),
                LanguageKey = LanguageKey.en
            },
            new LanguageResourceEntity
            {
                Id = 14,
                Code = new Guid("48178984-0325-469a-a611-6c29c5610f13"),
                Value = "Jeddah saudi arabia",
                LanguageResourceSetId = new Guid("099ae7f8-6bd2-4ee2-887e-a9d44449f413"),
                LanguageKey = LanguageKey.en
            },
            new LanguageResourceEntity
            {
                Id = 15,
                Code = new Guid("48178984-0325-469a-a611-6c29c5610f13"),
                Value = "Riadh saudi arabia",
                LanguageResourceSetId = new Guid("099ae6f8-6bd2-4ee2-887e-a9d44449f414"),
                LanguageKey = LanguageKey.en
            },
            new LanguageResourceEntity
            {
                Id = 16,
                Code = new Guid("48178984-0325-469a-a611-6c29c5610f13"),
                Value = "Jeddah saudi arabia",
                LanguageResourceSetId = new Guid("d2ab81c5-b9b3-6b14-827e-d24da57d6ff7"),
                LanguageKey = LanguageKey.en
            },
            new LanguageResourceEntity
            {
                Id = 17,
                Code = new Guid("48178984-0325-469a-a611-6c29c5610f13"),
                Value = "Riadh saudi arabia",
                LanguageResourceSetId = new Guid("d2ab81c5-b9b3-6b14-827e-d24da57d6ff8"),
                LanguageKey = LanguageKey.en
            }
            );
    }
}