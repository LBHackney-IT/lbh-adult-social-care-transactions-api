using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Bills;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Invoices;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.SeedConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure
{

    public class DatabaseContext : DbContext
    {
        //TODO: rename DatabaseContext to reflect the data source it is representing. eg. MosaicContext.
        //Guidance on the context class can be found here https://github.com/LBHackney-IT/lbh-base-api/wiki/DatabaseContext
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<DatabaseEntity> DatabaseEntities { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillFile> BillFiles { get; set; }
        public DbSet<BillItem> BillItems { get; set; }
        public DbSet<BillStatus> BillStatuses { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<InvoiceStatus> InvoiceStatuses { get; set; }
        public DbSet<InvoiceNumber> InvoiceNumbers { get; set; }
        public DbSet<InvoiceItemPaymentStatus> InvoiceItemPaymentStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Database Seeds

            // Seed bill status
            modelBuilder.ApplyConfiguration(new BillStatusSeed());

            // Seed invoice status
            modelBuilder.ApplyConfiguration(new InvoiceStatusSeed());

            modelBuilder.ApplyConfiguration(new InvoiceNumberSeed());
            modelBuilder.ApplyConfiguration(new InvoiceItemPaymentStatusesSeed());

            #endregion Database Seeds
        }

        public override int SaveChanges()
        {
            SetEntitiesUpdatedOnSave();

            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            SetEntitiesUpdatedOnSave();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = new CancellationToken())
        {
            SetEntitiesUpdatedOnSave();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            SetEntitiesUpdatedOnSave();

            return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        private void SetEntitiesUpdatedOnSave()
        {
            IList<EntityEntry> entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity && e.State == EntityState.Modified)
                .ToList();

            Type baseEntityType = typeof(BaseEntity);

            IList<Type> entityTypes = entries.Select(item => item.GetType())
                .Distinct()
                .Where(item => baseEntityType.IsAssignableFrom(item))
                .ToList();

            IList<EntityEntry> entitiesToUpdate = entries.Where(item => entityTypes.Contains(item.GetType())).ToList();

            foreach (EntityEntry entityEntry in entitiesToUpdate)
            {
                ((BaseEntity) entityEntry.Entity).DateUpdated = DateTimeOffset.UtcNow;
            }
        }
    }
}
