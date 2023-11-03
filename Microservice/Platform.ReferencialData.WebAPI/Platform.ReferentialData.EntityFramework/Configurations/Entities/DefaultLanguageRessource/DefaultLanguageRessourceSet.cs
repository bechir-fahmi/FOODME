using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Platform.ReferentialData.DataModel;

namespace Platform.ReferentialData.EntityFramework.Configurations.Entities.DefaultLanguageRessource;

public class DefaultLanguageRessourceSet : IEntityTypeConfiguration<LanguageResourceSetEntity>
{
    public void Configure(EntityTypeBuilder<LanguageResourceSetEntity> builder)
    {
        builder.HasData(
            new LanguageResourceSetEntity
            {
                LanguageResourceSetId = new Guid("099ae5f8-6bd2-4ee2-887e-a9d44449f4ce")
            },
            new LanguageResourceSetEntity
            {
                LanguageResourceSetId = new Guid("13488271-3653-41a0-9287-dc65ab6d2d7e")
            },
            new LanguageResourceSetEntity
            {
                LanguageResourceSetId = new Guid("310363c2-3044-47c7-81ea-fababb60d3dc")
            },
            new LanguageResourceSetEntity
            {
                LanguageResourceSetId = new Guid("3ee5766a-af6a-436b-b588-b9ef645867f0")
            },
            new LanguageResourceSetEntity
            {
                LanguageResourceSetId = new Guid("48178984-0325-469a-a611-6c29c5610f13")
            },
            new LanguageResourceSetEntity
            {
                LanguageResourceSetId = new Guid("d2ab81c5-b9b3-4b14-827e-d24da57d6ff4")
            },
            new LanguageResourceSetEntity
            {
                LanguageResourceSetId = new Guid("e5a05881-856c-4453-99b2-74222677ceb7")
            },
            new LanguageResourceSetEntity
            {
                LanguageResourceSetId = new Guid("ef192566-67d7-4f85-b724-f8163019723a")
            },
            new LanguageResourceSetEntity
            {
                LanguageResourceSetId = new Guid("099ae5f8-6bd2-4ee2-667e-a9d44449f4ca")
            },
            new LanguageResourceSetEntity
            {
                LanguageResourceSetId = new Guid("088ae5f8-6bd2-4ee2-777e-a9d44449f4ca")
            },
            new LanguageResourceSetEntity
            {
                LanguageResourceSetId = new Guid("099ae5f8-6bd2-4ee2-777e-a9d44449f4ef")
            },
            new LanguageResourceSetEntity
            {
                LanguageResourceSetId = new Guid("099ae5f8-6bd2-4ee2-887e-a9d44449f4fe")
            },
            new LanguageResourceSetEntity
            {
                LanguageResourceSetId = new Guid("099ae5f7-6bd2-4ee2-887e-a9d44449f4f0")
            },
            new LanguageResourceSetEntity
            {
                LanguageResourceSetId = new Guid("099ae7f8-6bd2-4ee2-887e-a9d44449f413")
            },
            new LanguageResourceSetEntity
            {
                LanguageResourceSetId = new Guid("099ae6f8-6bd2-4ee2-887e-a9d44449f414")
            },
            new LanguageResourceSetEntity
            {
                LanguageResourceSetId = new Guid("d2ab81c5-b9b3-6b14-827e-d24da57d6ff7")
            },
            new LanguageResourceSetEntity
            {
                LanguageResourceSetId = new Guid("d2ab81c5-b9b3-6b14-827e-d24da57d6ff8")
            }
            );
    }
}
