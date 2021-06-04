using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Invoices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.SeedConfiguration
{
    public class InvoiceNumberSeed : IEntityTypeConfiguration<InvoiceNumber>
    {
        public void Configure(EntityTypeBuilder<InvoiceNumber> builder)
        {
            builder.HasData(new InvoiceNumber
            {
                InvoiceNumberId = 1,
                Prefix = "INV",
                CurrentInvoiceNumber = 1,
                PostFix = null
            });
        }
    }
}
