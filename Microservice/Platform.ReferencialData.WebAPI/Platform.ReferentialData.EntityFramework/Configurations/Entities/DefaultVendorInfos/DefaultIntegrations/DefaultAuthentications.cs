using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Platform.ReferentialData.DataModel.FoodTypeData;
using Platform.ReferentialData.DataModel.BrandData;
using Nest;
using Platform.Shared.Enum;

namespace Platform.ReferentialData.EntityFramework.Configurations.Entities.DefaultBrands.DefaultIntegrations;

public class DefaultAuthentications : IEntityTypeConfiguration<AuthenticationEntity>
{
    public void Configure(EntityTypeBuilder<AuthenticationEntity> builder)
    {
        builder.HasData(
                        new AuthenticationEntity
                        {
                            AuthenticationId = new Guid("0efd66e7-f5ad-42a5-928c-241c4cb71ae4"),
                            AuthenticationType = AuthenticationType.NoAuth,
                            Login = "",
                            Password = "",
                            APIkey = "",
                            Token = ""
                        },
                        new AuthenticationEntity
                        {
                            AuthenticationId = new Guid("0efd66e7-e4ad-42a5-928c-241c4cb71ae4"),
                            AuthenticationType = AuthenticationType.NoAuth,
                            Login = "",
                            Password = "",
                            APIkey = "",
                            Token = ""
                        },
                        new AuthenticationEntity
                        {
                            AuthenticationId = new Guid("16ce9df4-a1a3-4e3e-80cd-49be1d3f6913"),
                            AuthenticationType = AuthenticationType.BasicAuth,
                            Login = "admin",
                            Password = "password",
                            APIkey = "",
                            Token = ""
                        }
                        );
    }
}