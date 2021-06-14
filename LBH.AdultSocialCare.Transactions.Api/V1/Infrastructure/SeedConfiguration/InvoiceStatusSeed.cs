using LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums;
using LBH.AdultSocialCare.Transactions.Api.V1.Extensions;
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
                Id = (int) InvoiceStatusEnum.Draft,
                StatusName = InvoiceStatusEnum.Draft.ToDescription()
            }, new InvoiceStatus
            {
                Id = (int) InvoiceStatusEnum.Held,
                StatusName = InvoiceStatusEnum.Held.ToDescription()
            }, new InvoiceStatus
            {
                Id = (int) InvoiceStatusEnum.Accepted,
                StatusName = InvoiceStatusEnum.Accepted.ToDescription()
            }, new InvoiceStatus
            {
                Id = (int) InvoiceStatusEnum.Released,
                StatusName = InvoiceStatusEnum.Released.ToDescription()
            });
        }
    }
}
