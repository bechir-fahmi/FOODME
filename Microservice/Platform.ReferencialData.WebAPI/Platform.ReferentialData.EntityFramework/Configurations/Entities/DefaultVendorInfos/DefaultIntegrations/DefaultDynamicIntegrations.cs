using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Platform.ReferentialData.DataModel.BrandData;
using Platform.Shared.Enum;

namespace Platform.ReferentialData.EntityFramework.Configurations.Entities.DefaultBrands.DefaultIntegrations;

public class DefaultDynamicIntegrations : IEntityTypeConfiguration<DynamicIntegrationEntity>
{
    public void Configure(EntityTypeBuilder<DynamicIntegrationEntity> builder)
    {
        builder.HasData(
                new DynamicIntegrationEntity()
                {
                    DynamicIntegrationId = new Guid("ce646e7d-e154-4a12-a795-33004abb1bd8"),
                    VendorId = new Guid("ce748e7d-e307-4a12-a795-33004abb1bd8"),
                    
                    URi = "localhost",
                    http = "http",
                    Port = "3004",
                    AuthenticationId = new Guid("16ce9df4-a1a3-4e3e-80cd-49be1d3f6913"),

                },
                new DynamicIntegrationEntity
                {
                    DynamicIntegrationId = new Guid("ce646e7d-e154-4a17-a795-33004acc1bd8"),
                    VendorId = new Guid("3a7b3888-9b89-459a-a108-e06aefec5500"),
                    URi = "localhost",
                    http = "http",
                    Port = "3004",
                    AuthenticationId = new Guid("0efd66e7-f5ad-42a5-928c-241c4cb71ae4"),

                },
                new DynamicIntegrationEntity
                {
                    DynamicIntegrationId = new Guid("ce646e7d-e154-4a17-a000-33004abb1bd8"),
                    VendorId = new Guid("ce646e7d-e307-4a12-a795-33004abb1bd8"),
                    URi = "localhost",
                    http = "http",
                    Port = "3004",
                    AuthenticationId = new Guid("0efd66e7-e4ad-42a5-928c-241c4cb71ae4"),
                }
        );
    }
}