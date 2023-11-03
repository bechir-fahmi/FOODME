using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Platform.ReferentialData.DataModel;

namespace Platform.ReferentialData.EntityFramework.Configurations.Entities.DefaultMasterData.DefaultLocation;

public class DefaultZones : IEntityTypeConfiguration<ZoneEntity>
{
    public void Configure(EntityTypeBuilder<ZoneEntity> builder)
    {
        builder.HasData(
            new ZoneEntity
            {
                Id = 1,
                Name = "Jeddah Saudi Arabia",
                CountryId = 1,
                CityId = 1,
                RegionId = 1,
                AreaId = 1,
                LanguageResourceSetId = new Guid("099ae7f8-6bd2-4ee2-887e-a9d44449f413") 

            },
            new ZoneEntity
            {
                Id = 2,
                Name = "Riadh Saudi Arabia",
                CountryId = 1,
                CityId = 2,
                RegionId = 2,
                AreaId = 2,
                LanguageResourceSetId = new Guid("099ae6f8-6bd2-4ee2-887e-a9d44449f414") 

            }
            );
    }
}