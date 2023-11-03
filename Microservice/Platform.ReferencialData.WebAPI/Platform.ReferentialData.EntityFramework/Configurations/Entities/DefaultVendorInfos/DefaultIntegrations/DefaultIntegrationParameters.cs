using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Platform.ReferentialData.DataModel;
using Platform.Shared.Enum;
using Platform.ReferencialData.BusinessModel;

namespace Platform.ReferentialData.EntityFramework.Configurations.Entities.DefaultBrands.DefaultIntegrations;

public class DefaultIntegrationParameters : IEntityTypeConfiguration<IntegrationParameterEntity>
{
    public void Configure(EntityTypeBuilder<IntegrationParameterEntity> builder)
    {
        builder.HasData(
            new IntegrationParameterEntity
            {
                IntegrationParameterId = new Guid("0127afbd-ade2-49ee-ae67-f034566c692d"),
                Key = "Latitude",
                MatchWithKey = Matching.Latitude,
                QueryOrBody = Source.FromBody,
                Type = ParamsType.In,
                MethodId = new Guid("623320f2-e01d-42ad-a5fb-d56cb3448a38")
            },
            new IntegrationParameterEntity
            {
             IntegrationParameterId = new Guid("0127afbd-ade2-49ee-ae67-f034566c693c"),
                Key = "Longitude",
                MatchWithKey = Matching.Longitude,
                QueryOrBody = Source.FromBody,
                Type = ParamsType.In,
                MethodId = new Guid("623320f2-e01d-42ad-a5fb-d56cb3448a38")
            },
            new IntegrationParameterEntity
            {
                IntegrationParameterId = new Guid("0127afbd-ade2-50ee-ae67-f034566c692d"),
                Key = "TimeEstimation",
                MatchWithKey = Matching.TimeFrom,
                QueryOrBody = Source.FromBody,
                Type = ParamsType.Out,
                MethodId = new Guid("623320f2-e01d-42ad-a5fb-d56cb3448a38")
            },
            new IntegrationParameterEntity
            {
                IntegrationParameterId = new Guid("0127afbd-ade2-51ee-ae67-f034566c692d"),
                Key = "Fees",
                MatchWithKey = Matching.Fee,
                QueryOrBody = Source.FromBody,
                Type = ParamsType.Out,
                MethodId = new Guid("623320f2-e01d-42ad-a5fb-d56cb3448a38")
            },
            new IntegrationParameterEntity
            {
                IntegrationParameterId = new Guid("0127afbd-ade2-52ee-ae67-f034566c692d"),
                Key = "Rating",
                MatchWithKey = Matching.Rating,
                QueryOrBody = Source.FromBody,
                Type = ParamsType.Out,
                MethodId = new Guid("623320f2-e01d-42ad-a5fb-d56cb3448a38")
            },
            new IntegrationParameterEntity
            {
                IntegrationParameterId = new Guid("0127afbd-ade2-53ee-ae67-f034566c692d"),
                Key = "Distance",
                MatchWithKey = Matching.Distance,
                QueryOrBody = Source.FromBody,
                Type = ParamsType.Out,
                MethodId = new Guid("623320f2-e01d-42ad-a5fb-d56cb3448a38")
            },
            new IntegrationParameterEntity
            {
                IntegrationParameterId = new Guid("0127afbd-ade2-54ee-ae67-f034566c692d"),
                Key = "Aggregator",
                MatchWithKey = Matching.Aggregator,
                QueryOrBody = Source.FromBody,
                Type = ParamsType.Out,
                MethodId = new Guid("623320f2-e01d-42ad-a5fb-d56cb3448a38")
            },
            new IntegrationParameterEntity
            {
                IntegrationParameterId = new Guid("0127afbd-ade2-49ee-ae67-f038866c692d"),
                Key = "DeliveryZone",
                MatchWithKey = Matching.Region,
                QueryOrBody = Source.FromBody,
                Type = ParamsType.In,
                MethodId = new Guid("2e635561-5ebb-4774-b9dd-8a6603153990")
            },
            new IntegrationParameterEntity
            {
                IntegrationParameterId = new Guid("0127afbd-ade2-49ee-ae67-f038866c0285"),
                Key = "TimeEstimation",
                MatchWithKey = Matching.TimeFrom,
                QueryOrBody = Source.FromBody,
                Type = ParamsType.Out,
                MethodId = new Guid("2e635561-5ebb-4774-b9dd-8a6603153990")
            },
            new IntegrationParameterEntity
            {
                IntegrationParameterId = new Guid("0127afbd-ade2-49ee-ae67-f038866c654d"),
                Key = "Fees",
                MatchWithKey = Matching.Fee,
                QueryOrBody = Source.FromBody,
                Type = ParamsType.Out,
                MethodId = new Guid("2e635561-5ebb-4774-b9dd-8a6603153990")
            },
            new IntegrationParameterEntity
            {
                IntegrationParameterId = new Guid("0127afbd-ade2-49ee-ae67-f038866c4782"),
                Key = "Rating",
                MatchWithKey = Matching.Rating,
                QueryOrBody = Source.FromBody,
                Type = ParamsType.Out,
                MethodId = new Guid("2e635561-5ebb-4774-b9dd-8a6603153990")
            },
            new IntegrationParameterEntity
            {
                IntegrationParameterId = new Guid("0127afbd-ade2-49ee-ae67-f038866c9633"),
                Key = "Distance",
                MatchWithKey = Matching.Distance,
                QueryOrBody = Source.FromBody,
                Type = ParamsType.Out,
                MethodId = new Guid("2e635561-5ebb-4774-b9dd-8a6603153990")
            },
            new IntegrationParameterEntity
            {
                IntegrationParameterId = new Guid("0127afbd-ade2-49ee-ae67-f038866c200d"),
                Key = "Aggregator",
                MatchWithKey = Matching.Aggregator,
                QueryOrBody = Source.FromBody,
                Type = ParamsType.Out,
                MethodId = new Guid("2e635561-5ebb-4774-b9dd-8a6603153990")
            },
            new IntegrationParameterEntity
            {
                IntegrationParameterId = new Guid("0127afbd-ade2-49ee-ae67-f038866c198d"),
                Key = "DeliveryZone",
                MatchWithKey = Matching.Region,
                QueryOrBody = Source.FromBody,
                Type = ParamsType.In,
                MethodId = new Guid("b5204c9b-cfd7-431b-9dfa-3c373e92fa89")
            },
            new IntegrationParameterEntity
            {
                IntegrationParameterId = new Guid("0127afbd-ade2-49ee-ae67-f038866c1745"),
                Key = "TimeEstimation",
                MatchWithKey = Matching.TimeFrom,
                QueryOrBody = Source.FromBody,
                Type = ParamsType.Out,
                MethodId = new Guid("b5204c9b-cfd7-431b-9dfa-3c373e92fa89")
            },
            new IntegrationParameterEntity
            {
                IntegrationParameterId = new Guid("0127afbd-ade2-49ee-ae67-f038866c166d"),
                Key = "Fees",
                MatchWithKey = Matching.Fee,
                QueryOrBody = Source.FromBody,
                Type = ParamsType.Out,
                MethodId = new Guid("b5204c9b-cfd7-431b-9dfa-3c373e92fa89")
            },
            new IntegrationParameterEntity
            {
                IntegrationParameterId = new Guid("0127afbd-ade2-49ee-ae67-f038866c1012"),
                Key = "Rating",
                MatchWithKey = Matching.Rating,
                QueryOrBody = Source.FromBody,
                Type = ParamsType.Out,
                MethodId = new Guid("b5204c9b-cfd7-431b-9dfa-3c373e92fa89")
            },
            new IntegrationParameterEntity
            {
                IntegrationParameterId = new Guid("0127afbd-ade2-49ee-ae67-f038866c1103"),
                Key = "Distance",
                MatchWithKey = Matching.Distance,
                QueryOrBody = Source.FromBody,
                Type = ParamsType.Out,
                MethodId = new Guid("b5204c9b-cfd7-431b-9dfa-3c373e92fa89")
            },
            new IntegrationParameterEntity
            {
                IntegrationParameterId = new Guid("0127afbd-ade2-49ee-ae67-f038866c100d"),
                Key = "Aggregator",
                MatchWithKey = Matching.Aggregator,
                QueryOrBody = Source.FromBody,
                Type = ParamsType.Out,
                MethodId = new Guid("b5204c9b-cfd7-431b-9dfa-3c373e92fa89")
            }

        );
    }
}


