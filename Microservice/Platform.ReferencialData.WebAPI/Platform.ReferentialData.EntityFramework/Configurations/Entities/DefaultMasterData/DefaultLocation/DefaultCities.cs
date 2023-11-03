using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Platform.ReferentialData.DataModel.LocationData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.ReferentialData.EntityFramework.Configurations.Entities.DefaultMasterData.DefaultLocation;

public class DefaultCities : IEntityTypeConfiguration<CityEntity>
{
    public void Configure(EntityTypeBuilder<CityEntity> builder)
    {
        builder.HasData(
            new CityEntity
            {
                Id = 1,
                NameLabelCode = new Guid("ce646e7d-e307-4a12-a795-33004abb1bd8"),
                RegionId = 1,
                CityCode = "Jeddah saudi arabia",
                Longitude = 104.256,
                Latitude = 856.254,
                LanguageResourceSetId = new Guid("099ae5f8-6bd2-4ee2-887e-a9d44449f4fe") 

            },
            new CityEntity
            {
                Id = 2,
                NameLabelCode = new Guid("ce646e7d-e307-4a12-a795-33004abb1bd9"),
                RegionId = 2,
                CityCode = "Riadh saudi arabia",
                Longitude = 201.589,
                Latitude = 745.256,
                 LanguageResourceSetId = new Guid("099ae5f7-6bd2-4ee2-887e-a9d44449f4f0")

            }
            );
    }
}