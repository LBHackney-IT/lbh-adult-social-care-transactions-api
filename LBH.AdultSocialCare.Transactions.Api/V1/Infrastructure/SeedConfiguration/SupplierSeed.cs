using LBH.AdultSocialCare.Transactions.Api.V1.AppConstants;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Suppliers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.SeedConfiguration
{
    public class SupplierSeed : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            var dateTimeOffset = new DateTimeOffset(AppTimeConstants.CreateUpdateDefaultDateTime).ToOffset(TimeSpan.Zero);
            builder.HasData(new Supplier
            {
                SupplierId = 1,
                PackageTypeId = 1,
                SupplierName = "ABC Limited"
            }, new Supplier
            {
                SupplierId = 2,
                PackageTypeId = 2,
                SupplierName = "XYZ Ltd"
            });
        }
    }
}
