
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Platform.ReferencialData.DataModel.Configurations.Entities;
using Platform.ReferentialData.DataModel;
using Platform.ReferentialData.DataModel.AgeRangeData;
using Platform.ReferentialData.DataModel.BrandData;
using Platform.ReferentialData.DataModel.BrandData.IntegrationBrand;
using Platform.ReferentialData.DataModel.Configurations.Entities;
using Platform.ReferentialData.DataModel.FoodTypeData;
using Platform.ReferentialData.DataModel.KitchenTypeData;
using Platform.ReferentialData.DataModel.LanguageData;
using Platform.ReferentialData.DataModel.LocationData;
using Platform.ReferentialData.DataModel.MealData;
using Platform.ReferentialData.DataModel.SupportService;
using Platform.ReferentialData.DataModel.TagData;
using Platform.ReferentialData.DataModel.Theme;
using Platform.ReferentialData.DataModel.UserAddressData;
using Platform.ReferentialData.DataModel.UserData;
using Platform.ReferentialData.DataModel.WorkingTime;
using Platform.ReferentialData.EntityFramework.Configurations.Entities;
using DeliveryModeEntity = Platform.ReferentialData.DataModel.DeliveryModeData.DeliveryModeEntity;
using Platform.ReferentialData.DataModel.OfferData;
using Platform.ReferentialData.EntityFramework.Configurations.Entities.DefaultBrands;
using Platform.ReferentialData.EntityFramework.Configurations.Entities.DefaultMasterData.DefaultDeliveryMode;
using Platform.ReferentialData.EntityFramework.Configurations.Entities.DefaultMasterData.DefaultFilters;
using Platform.ReferentialData.EntityFramework.Configurations.Entities.DefaultMasterData.DefaultLocation;
using Platform.ReferentialData.EntityFramework.Configurations.Entities.DefaultLanguageRessourceSet;
using Platform.ReferentialData.EntityFramework.Configurations.Entities.DefaultBrands.DefaultIntegrations;
using Platform.ReferentialData.EntityFramework.Configurations.Entities.DefaultLanguageRessource;
using Platform.ReferentialData.DataModel.BrandData.Integration;
using Platform.ReferentialData.DataModel.QueryData;

namespace Platform.ReferentialData.EntityFramework
{
    public class ReferentialDataContext : IdentityDbContext<UserEntity, RoleEntity, string, IdentityUserClaim<string>, UserRoleEntity, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ReferentialDataContext(DbContextOptions options) :
            base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            object value = optionsBuilder.UseLazyLoadingProxies(); // Enable lazy loading
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>()
                .ToTable("User", "Security");

            modelBuilder.Entity<RoleEntity>()
                .ToTable("Role", "Security");

            modelBuilder.Entity<UserRoleEntity>()
                .ToTable("UserRole", "Security");

            modelBuilder.Entity<IdentityUserClaim<string>>()
                .ToTable("UserClaim", "Security");

            modelBuilder.Entity<IdentityRoleClaim<string>>()
               .ToTable("RoleClaim", "Security");

            modelBuilder.Entity<IdentityUserLogin<string>>()
                .ToTable("UserLogin", "Security");

            modelBuilder.Entity<IdentityUserToken<string>>()
                .ToTable("UserToken", "Security");

            modelBuilder.Entity<UserOTPVerificationEntity>()
                .ToTable("UserOTPVerification", "Security");

            modelBuilder.ApplyConfiguration(new DefaultRoles());
            modelBuilder.ApplyConfiguration(new DefaultRoleClaims());
            modelBuilder.ApplyConfiguration(new DefaultAdministator());
            modelBuilder.ApplyConfiguration(new DefaultUserRole());


            modelBuilder.ApplyConfiguration(new DefaultLanguageRessourceSet());
            modelBuilder.ApplyConfiguration(new DefaultFoodTypes());
            modelBuilder.ApplyConfiguration(new DefaultKitchenTypes());
            modelBuilder.ApplyConfiguration(new DefaultMealTimings());
            modelBuilder.ApplyConfiguration(new DefaultMealTypes());
            modelBuilder.ApplyConfiguration(new DefaultCountry());
            modelBuilder.ApplyConfiguration(new DefaultRegion());
            modelBuilder.ApplyConfiguration(new DefaultCities());
            modelBuilder.ApplyConfiguration(new DefaultAreas());
            modelBuilder.ApplyConfiguration(new DefaultZones());
            modelBuilder.ApplyConfiguration(new DefaultDeliveryMode());
            modelBuilder.ApplyConfiguration(new DefaultLanguageRessource());
            modelBuilder.ApplyConfiguration(new DefaultVendors());
            modelBuilder.ApplyConfiguration(new DefaultVendorDeliverys());
            modelBuilder.ApplyConfiguration(new DefaultVendorFoodTypes());
            modelBuilder.ApplyConfiguration(new DefaultVendorKitchenTypes());
            modelBuilder.ApplyConfiguration(new DefaultMealVendorTypes());
            modelBuilder.ApplyConfiguration(new DefaultVendorMealTimings());
            modelBuilder.ApplyConfiguration(new DefaultVendorZones());
            modelBuilder.ApplyConfiguration(new DefaultAuthentications());
            modelBuilder.ApplyConfiguration(new DefaultDynamicIntegrations());
            modelBuilder.ApplyConfiguration(new DefaultIntegrationMethods());
            modelBuilder.ApplyConfiguration(new DefaultIntegrationParameters());


            modelBuilder.UseSerialColumns();

            #region Many-To-Many

            modelBuilder.Entity<VendorFoodTypeEntity>().HasKey(e => new { e.VendorId, e.FoodTypeId });
            modelBuilder.Entity<VendorKitchenTypeEntity>().HasKey(e => new { e.VendorId, e.KitchenTypeId });
            modelBuilder.Entity<VendorMealTimingEntity>().HasKey(e => new { e.VendorId, e.MealTimingId });
            modelBuilder.Entity<VendorMealTypeEntity>().HasKey(e => new { e.VendorId, e.MealTypeId });
            modelBuilder.Entity<VendorDeliveryModeEntity>().HasKey(e => new { e.VendorId, e.DeliveryModeId });
            modelBuilder.Entity<VendorDeliveryZonesEntity>().HasKey(e => new { e.VendorId, e.ZoneId });
            modelBuilder.Entity<StaticIntegrationEntity>().HasKey(e => new { e.VendorId, e.ZoneId });
            modelBuilder.Entity<BrandMatchingEntity>().HasKey(e => new { e.AggregatorId, e.LocalBrandId });
            modelBuilder.Entity<TagFoodTypeEntity>().HasKey(e => new { e.TagId, e.FoodTypeId });
            modelBuilder.Entity<TagKitchenTypeEntity>().HasKey(e => new { e.TagId, e.KitchenTypeId });
            modelBuilder.Entity<TagLanguageEntity>().HasKey(e => new { e.TagId, e.LanguageId });
            modelBuilder.Entity<TagCityEntity>().HasKey(e => new { e.TagId, e.CityId });
            modelBuilder.Entity<TagRegionEntity>().HasKey(e => new { e.TagId, e.RegionId });
            modelBuilder.Entity<TagMealTimingEntity>().HasKey(e => new { e.TagId, e.MealTimingId });
            modelBuilder.Entity<TagMealTypeEntity>().HasKey(e => new { e.TagId, e.MealTypeId });
            modelBuilder.Entity<TagOfferEntity>().HasKey(e => new { e.TagId, e.OfferId });
            modelBuilder.Entity<TagVendorEntity>().HasKey(e => new { e.TagId, e.VendorId });
            modelBuilder.Entity<TagZoneEntity>().HasKey(e => new { e.TagId, e.ZoneId });

            #endregion
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is ReferentialDataBase && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((ReferentialDataBase)entityEntry.Entity).LastModificationTime = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((ReferentialDataBase)entityEntry.Entity).CreationTime = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }

        #region Restaurant Data Tables
        public DbSet<VendorEntity> Vendor { get; set; }

        #endregion
        #region LocationDataTables
        public DbSet<AreaEntity> Area { get; set; }
        public DbSet<ZoneEntity> Zones { get; set; }
        public DbSet<TagZoneEntity> TagZones { get; set; }
        public DbSet<StaticIntegrationEntity> StaticIntegrations { get; set; }
        public DbSet<DynamicIntegrationEntity> DynamicIntegrations { get; set; }
        public DbSet<IntegrationMethodEntity> IntegrationMethod { get; set; }
        public DbSet<IntegrationParameterEntity> IntegrationMethodParameters { get; set; }
        public DbSet<CountryEntity> Country { get; set; }
        public DbSet<CityEntity> City { get; set; }
        public DbSet<TagCityEntity> TagCity { get; set; }
        public DbSet<RegionEntity> Region { get; set; }
        public DbSet<TagRegionEntity> TagRegion { get; set; }
        #endregion
        #region VendorData
        public DbSet<VendorFoodTypeEntity> VendorFoodTypes { get; set; }
        public DbSet<VendorKitchenTypeEntity> VendorKitchenTypes { get; set; }
        public DbSet<VendorMealTimingEntity> VendorMealTimings { get; set; }
        public DbSet<VendorMealTypeEntity> VendorMealTypes { get; set; }
        public DbSet<VendorDeliveryModeEntity> VendorDeliverys { get; set; }
        public DbSet<VendorEntity> Vendors { get; set; }
        public DbSet<VendorDeliveryZonesEntity> VendorZones { get; set; }
        public DbSet<TagVendorEntity> TagVendor { get; set; }
        public DbSet<BrandMatchingEntity> BrandMatching { get; set; }
        public DbSet<QueryEntity> Query { get; set; }
        #endregion

        public DbSet<UserAddressEntity> UserAddresse { get; set; }
        public DbSet<UserAddressTypeEntity> UserAddressType { get; set; }

        #region Language
        public DbSet<LanguageEntity> Language { get; set; }
        public DbSet<LanguageResourceEntity> LanguageResource { get; set; }
        public DbSet<LanguageResourceSetEntity> LanguageResourceSet { get; set; }
        #endregion
      
        public DbSet<UserOTPVerificationEntity> UserOTPVerification { get; set; }

        #region Working Time
        public DbSet<ClenderWorkingTimeEntity> ClenderWorkingTime { get; set; }
        public DbSet<DayWorkingTimeEntity> DayWorkingTime { get; set; }
        public DbSet<ExceptionDayWorkingTimeEntity> ExceptionDayWorkingTime { get; set; }
        public DbSet<ExceptionWeekWorkingTimeEntity> ExceptionWeekWorkingTime { get; set; }


        #endregion
        #region KitchenType
        public DbSet<KitchenTypeEntity> KitchenType { get; set; }
        public DbSet<TagKitchenTypeEntity> TagKitchenType { get; set; }

        #endregion


        public DbSet<ThemeEntity> Themes { get; set; }


        #region SupportService
        public DbSet<TermsServiceEntity> TermsService { get; set; }
        public DbSet<SuportCategoryEntity> SuportCategory { get; set; }
        public DbSet<QuestionAnswerEntity> QuestionAnswer { get; set; }

        #endregion

        #region FoodType
        public DbSet<FoodTypeEntity> FoodType { get; set; }
        public DbSet<TagFoodTypeEntity> TagFoodType { get; set; }
        #endregion

        #region MealType
        public DbSet<MealTypeEntity> MealType { get; set; }
        public DbSet<TagMealTypeEntity> TagMealType { get; set; }
        #endregion

        #region MealTiming
        public DbSet<MealTimingEntity> MealTiming { get; set; }
        public DbSet<TagMealTimingEntity> TagMealTiming { get; set; }
        #endregion

        #region AgeRange
        public DbSet<AgeRangeEntity> AgeRange { get; set; }
        #endregion

        #region DeliveryMode
        public DbSet<DeliveryModeEntity> DeliveryMode { get; set; }
        #endregion
        #region Offer
        public DbSet<OfferEntity> Offer { get; set; }
        public DbSet<TagOfferEntity> TagOffer { get; set; }
        #endregion

        #region Tag
        public DbSet<TagEntity> Tag { get; set; }
        #endregion
    }
}
