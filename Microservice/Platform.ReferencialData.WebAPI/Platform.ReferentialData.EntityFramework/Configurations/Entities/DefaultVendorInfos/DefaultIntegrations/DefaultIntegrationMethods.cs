using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Platform.ReferentialData.DataModel.BrandData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Platform.ReferentialData.DataModel;
using Platform.Shared.Enum;

namespace Platform.ReferentialData.EntityFramework.Configurations.Entities.DefaultBrands.DefaultIntegrations;

public class DefaultIntegrationMethods : IEntityTypeConfiguration<IntegrationMethodEntity>
{
    public void Configure(EntityTypeBuilder<IntegrationMethodEntity> builder)
    {
        builder.HasData(
                new IntegrationMethodEntity
                {
                    IntegrationMethodId = new Guid("623320f2-e01d-42ad-a5fb-d56cb3448a38"),
                    UseDefaultAuth = true,
                    IntegrationType = IntegrationType.DeliveryInfo,
                    EndPoint = "SecureApi/KFC/deliveryInfo",
                    Content = ContentType.TextHtml,
                    MethodType = MethodType.Post,
                    AuthenticationId = new Guid("16ce9df4-a1a3-4e3e-80cd-49be1d3f6913"),
                    DynamicIntegrationId = new Guid("ce646e7d-e154-4a12-a795-33004abb1bd8"),
                },
                new IntegrationMethodEntity
                {
                    IntegrationMethodId = new Guid("2e635561-5ebb-4774-b9dd-8a6603153990"),
                    UseDefaultAuth = true,
                    IntegrationType = IntegrationType.DeliveryInfo,
                    EndPoint = "NotsecureApi/Magma/deliveryInfo",
                    Content = ContentType.TextHtml,
                    MethodType = MethodType.Post,
                    AuthenticationId = new Guid("0efd66e7-f5ad-42a5-928c-241c4cb71ae4"),

                    DynamicIntegrationId = new Guid("ce646e7d-e154-4a17-a795-33004acc1bd8"),
                },
                new IntegrationMethodEntity
                {
                    IntegrationMethodId = new Guid("b5204c9b-cfd7-431b-9dfa-3c373e92fa89"),
                    UseDefaultAuth = true,
                    IntegrationType = IntegrationType.DeliveryInfo,
                    EndPoint = "NotsecureApi/chikeNDip/deliveryInfo",
                    Content = ContentType.TextHtml,
                    MethodType = MethodType.Post,
                    DynamicIntegrationId = new Guid("ce646e7d-e154-4a17-a000-33004abb1bd8"),
                    AuthenticationId = new Guid("0efd66e7-e4ad-42a5-928c-241c4cb71ae4"),

                }
                );
    }
}