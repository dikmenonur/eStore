using eStore.IdentityService.Core.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStore.IdentityService.Core.DataSource
{
    public class SsoDbContext : DbContext
    {
        public SsoDbContext(DbContextOptions options) : base(options)
        {
            this.ChangeTracker.AutoDetectChangesEnabled = false;
            this.ChangeTracker.LazyLoadingEnabled = false;
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public virtual DbSet<User> User{ get; set; }

        public virtual DbSet<UserLoginLogItem> UserLoginLogItem { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<UserLoginLogItem>().ToTable("UserLoginLogItem");
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
