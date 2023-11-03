using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Platform.ReferentialData.DataModel.UserData;

namespace Platform.ReferentialData.EntityFramework.Configurations.Entities
{
    public class DefaultAdministator : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            var administrator = new UserEntity()
            {
                Id = "6bf1ea57-1d07-4e8f-97d4-2b3c050a979f",
                UserName = "Administrator",
                NormalizedUserName = "ADMINISTRATOR",
                Email = "mail.foodme@gmail.com",
                NormalizedEmail = "MAIL.FOODME@GMAIL.COM",
                EmailConfirmed = true,
                PhoneNumber = "515553891",
                Status = Shared.Enum.Status.isActive,
                CreatorUserId = "No User",
                CreationTime = DateTime.UtcNow,
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            var hasher = new PasswordHasher<UserEntity>();
            administrator.PasswordHash = hasher.HashPassword(administrator, "Admin2023@");
            builder.HasData(administrator);
        }
    }
}
