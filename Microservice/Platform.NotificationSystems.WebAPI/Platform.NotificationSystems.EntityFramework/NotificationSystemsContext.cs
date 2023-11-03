using Microsoft.EntityFrameworkCore;
using Platform.NotificationSystems.DataModel.SMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.NotificationSystems.EntityFramework
{
    public class NotificationSystemsContext : DbContext
    {
        public NotificationSystemsContext(DbContextOptions options) :
            base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }
        public DbSet<SMSProviderEntity> SMSProvider { get; set; }
        public DbSet<SMSProviderEndPointEntity> SMSProviderEndPoint { get; set; }
    }
}
