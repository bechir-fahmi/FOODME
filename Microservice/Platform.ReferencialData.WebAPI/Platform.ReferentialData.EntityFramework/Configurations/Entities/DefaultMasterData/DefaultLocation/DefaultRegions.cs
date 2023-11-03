using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Platform.ReferentialData.DataModel.LocationData;

namespace Platform.ReferentialData.EntityFramework.Configurations.Entities.DefaultMasterData.DefaultLocation;

public class DefaultRegion : IEntityTypeConfiguration<RegionEntity>
{
    public void Configure(EntityTypeBuilder<RegionEntity> builder)
    {
        builder.HasData(
            new RegionEntity
            {
                Id = 1,
                NameLabelCode = new Guid("ce646e7d-e307-4a12-a795-33004abb1bd8"),
                CountryId = 1,
                LanguageResourceSetId = new Guid("d2ab81c5-b9b3-6b14-827e-d24da57d6ff7") 

            },
            new RegionEntity
            {
                Id = 2,
                NameLabelCode = new Guid("ce646e7d-e307-4a12-a795-33004abb1bd9"),
                CountryId = 1,
                LanguageResourceSetId = new Guid("d2ab81c5-b9b3-6b14-827e-d24da57d6ff8") 
            }
            );
    }
}