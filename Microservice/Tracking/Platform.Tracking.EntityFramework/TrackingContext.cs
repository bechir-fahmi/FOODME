using Microsoft.EntityFrameworkCore;
using Platform.Tracking.DataModel.BrandAction;
using Platform.Tracking.DataModel.GetDeals;
using Platform.Tracking.DataModel.Offre;
using Platform.Tracking.DataModel.UserSearch;
using Platform.Tracking.DataModel.UserStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Tracking.EntityFramework
{
    public class TrackingContext : DbContext
    {
        public TrackingContext(DbContextOptions options) :
            base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BrandActionSummaryView>()
              .ToView("brandactionsummaryviews").HasNoKey();
            modelBuilder.UseSerialColumns();
        }

        public DbSet<BrandActionEntity> BrandAction { get; set; }
        public DbSet<BrandActionSummaryEntity> BrandActionSummary { get; set; }
        public DbSet<BrandActionSummaryView> BrandActionSummaryViews { get; set; }
        public DbSet<UserStatusEntity> UserStatus { get; set; }
        public DbSet<UserSearchEntity> UserSearch { get; set; }
        public DbSet<OfferActionEntity> OfferAction { get; set; }
        public DbSet<DealActionEntity> DealAction { get; set; }
        public DbSet<AggregatorItemEntity> AggregatorItem { get; set; }
    }
}
