using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Bills;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Invoices;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.PayRunModels;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Suppliers;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.SeedConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
        public DbSet<Department> Departments { get; set; }
        public DbSet<DisputedInvoice> DisputedInvoices { get; set; }
        public DbSet<DisputedInvoiceChat> DisputedInvoiceChats { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<InvoiceStatus> InvoiceStatuses { get; set; }
        public DbSet<InvoiceNumber> InvoiceNumbers { get; set; }
        public DbSet<InvoiceItemPaymentStatus> InvoiceItemPaymentStatuses { get; set; }
        public DbSet<PayRun> PayRuns { get; set; }
        public DbSet<PayRunItem> PayRunItems { get; set; }
        public DbSet<PayRunType> PayRunTypes { get; set; }
        public DbSet<PayRunStatus> PayRunStatuses { get; set; }
        public DbSet<PayRunSubType> PayRunSubTypes { get; set; }
        public DbSet<BillPayment> BillPayments { get; set; }
        public DbSet<SupplierCreditNote> SupplierCreditNotes { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierTaxRate> SupplierTaxRates { get; set; }
        public DbSet<Ledger> Ledgers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Model Config

            modelBuilder.Entity<DisputedInvoice>(entity =>
            {
                entity.HasIndex(e => new { e.InvoiceId, e.InvoiceItemId })
                    .IsUnique();
            });

            modelBuilder.Entity<PayRunItem>(entity =>
            {
                entity.HasIndex(pri => new { pri.PayRunId, pri.InvoiceId, pri.InvoiceItemId })
                    .IsUnique();
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasIndex(i => new { i.InvoiceNumber })
                    .IsUnique();
            });

            #endregion Model Config

            base.OnModelCreating(modelBuilder);

            #region Database Seeds

            // Seed bill status
            modelBuilder.ApplyConfiguration(new BillStatusSeed());

            // Seed invoice status
            modelBuilder.ApplyConfiguration(new InvoiceStatusSeed());
            modelBuilder.ApplyConfiguration(new InvoiceNumberSeed());
            modelBuilder.ApplyConfiguration(new InvoiceItemPaymentStatusesSeed());

            modelBuilder.ApplyConfiguration(new PayRunTypesSeed());
            modelBuilder.ApplyConfiguration(new PayRunStatusesSeed());
            modelBuilder.ApplyConfiguration(new PayRunSubTypesSeed());
            modelBuilder.ApplyConfiguration(new DepartmentsSeed());
            modelBuilder.ApplyConfiguration(new PackageTypesSeed());

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
