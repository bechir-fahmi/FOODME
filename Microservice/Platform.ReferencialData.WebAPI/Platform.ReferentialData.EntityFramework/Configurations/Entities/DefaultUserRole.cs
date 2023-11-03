using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Platform.ReferentialData.DataModel.UserData;

namespace Platform.ReferentialData.DataModel.Configurations.Entities
{
    public class DefaultUserRole : IEntityTypeConfiguration<UserRoleEntity>
    {
        public void Configure(EntityTypeBuilder<UserRoleEntity> builder)
        {
            builder.HasData(
                new UserRoleEntity
                {
                    UserId = "6bf1ea57-1d07-4e8f-97d4-2b3c050a979f",
                    RoleId = "8f1e9ce9-ab8c-4aaf-958b-596b88dc6684"
                });
        }
    }
}
