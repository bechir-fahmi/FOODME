using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Platform.ReferentialData.DataModel.LocationData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.ReferentialData.EntityFramework.Configurations.Entities.DefaultMasterData.DefaultLocation;

public class DefaultAreas : IEntityTypeConfiguration<AreaEntity>
{
    public void Configure(EntityTypeBuilder<AreaEntity> builder)
    {
        builder.HasData(
            new AreaEntity
            {
                Id = 1,
                NameLabelCode = new Guid("ce646e7d-e307-4a12-a795-33004abb1bd8"),
                AreaName = "",
                CityId = 1,
                Longitude = 104.258,
                Latitude = 856.254,
            },
            new AreaEntity
            {
                Id = 2,
                NameLabelCode = new Guid("ce646e7d-e307-4a12-a795-33004abb1bd9"),
                AreaName = "",
                CityId = 2,
                Longitude = 201.589,
                Latitude = 745.256
            }
            );
    }
}