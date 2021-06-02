using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Invoices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.SeedConfiguration
{
    public class InvoiceStatusSeed : IEntityTypeConfiguration<InvoiceStatus>
    {
        public void Configure(EntityTypeBuilder<InvoiceStatus> builder)
        {
            builder.HasData(new InvoiceStatus
            {
                Id = 1,
                StatusName = "Draft"
            }, new InvoiceStatus
            {
                Id = 2,
                StatusName = "Paid"
            }, new InvoiceStatus
            {
                Id = 3,
                StatusName = "Held"
            });
        }
    }
}
