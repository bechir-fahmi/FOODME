using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Platform.ReferentialData.DataModel.UserData;
using Platform.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.ReferentialData.EntityFramework.Configurations.Entities
{
    public class DefaultRoleClaims : IEntityTypeConfiguration<IdentityRoleClaim<String>>
    {
        public void Configure(EntityTypeBuilder<IdentityRoleClaim<String>> builder)
        {
            builder.HasData(
                new IdentityRoleClaim<String>
                {
                    Id = 1,
                    RoleId = "5310d489-98d2-416e-bbf4-7badc5197f73",
                    ClaimType = "Permission",
                    ClaimValue = "Permission.Brand.ViewAll"
                },
            new IdentityRoleClaim<String>
            {
                Id = 2,
                RoleId = "5310d489-98d2-416e-bbf4-7badc5197f73",
                ClaimType = "Permission",
                ClaimValue = "Permission.Brand.View"
            },
            new IdentityRoleClaim<String>
            {
                Id = 3,
                RoleId = "5310d489-98d2-416e-bbf4-7badc5197f73",
                ClaimType = "Permission",
                ClaimValue = "Permission.Aggregator.ViewAll"
            },
           new IdentityRoleClaim<String>
            {
                Id = 4,
                RoleId = "5310d489-98d2-416e-bbf4-7badc5197f73",
                ClaimType = "Permission",
                ClaimValue = "Permission.Aggregator.View"
           },
            new IdentityRoleClaim<String>
            {
                Id = 5,
                RoleId = "5310d489-98d2-416e-bbf4-7badc5197f73",
                ClaimType = "Permission",
                ClaimValue = "Permission.UserManagement.View"
            },
             new IdentityRoleClaim<String>
             {
                 Id = 6,
                 RoleId = "5310d489-98d2-416e-bbf4-7badc5197f73",
                 ClaimType = "Permission",
                 ClaimValue = "Permission.UserManagement.Update"
             },
             new IdentityRoleClaim<String>
             {
                 Id = 7,
                 RoleId = "5310d489-98d2-416e-bbf4-7badc5197f73",
                 ClaimType = "Permission",
                 ClaimValue = "Permission.UserManagement.Delete"
             },
             new IdentityRoleClaim<String>
             {
                 Id = 8,
                 RoleId = "5310d489-98d2-416e-bbf4-7badc5197f73",
                 ClaimType = "Permission",
                 ClaimValue = "Permission.Offers.ViewAll"
             });
            
        }
    }
}
