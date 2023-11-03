using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Platform.ReferentialData.DataModel.UserData;
using Platform.Shared.Enum;

namespace Platform.ReferencialData.DataModel.Configurations.Entities
{
    public class DefaultRoles : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.HasData(
                new RoleEntity
                {
                    Id = "8f1e9ce9-ab8c-4aaf-958b-596b88dc6684",
                    Name = DefaultRole.ADMINISTRATOR.ToString(),
                    NormalizedName = DefaultRole.ADMINISTRATOR.ToString(),
                    Status = Shared.Enum.Status.isActive,
                    CreatorUserId = "No User",
                    CreationTime = DateTime.UtcNow,
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                },
                new RoleEntity
                {
                    Id = "5310d489-98d2-416e-bbf4-7badc5197f73",
                    Name = DefaultRole.CLIENT.ToString(),
                    NormalizedName = DefaultRole.CLIENT.ToString(),
                    Status = Shared.Enum.Status.isActive,
                    CreatorUserId = "No User",
                    CreationTime = DateTime.UtcNow,
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                }
                );

        }
    }
}
