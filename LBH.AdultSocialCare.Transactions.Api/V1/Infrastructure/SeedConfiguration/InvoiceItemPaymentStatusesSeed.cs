using LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums;
using LBH.AdultSocialCare.Transactions.Api.V1.Extensions;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Invoices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.SeedConfiguration
{
    public class InvoiceItemPaymentStatusesSeed : IEntityTypeConfiguration<InvoiceItemPaymentStatus>
    {
        public void Configure(EntityTypeBuilder<InvoiceItemPaymentStatus> builder)
        {
            builder.HasData(
                new InvoiceItemPaymentStatus
                {
                    StatusId = (int) InvoiceItemPaymentStatusEnum.NotStarted,
                    StatusName = InvoiceItemPaymentStatusEnum.NotStarted.ToDescription(),
                    DisplayName = InvoiceItemPaymentStatusEnum.NotStarted.GetDisplayName()
                },
                new InvoiceItemPaymentStatus
                {
                    StatusId = (int) InvoiceItemPaymentStatusEnum.Held,
                    StatusName = InvoiceItemPaymentStatusEnum.Held.ToDescription(),
                    DisplayName = InvoiceItemPaymentStatusEnum.Held.GetDisplayName()
                },
                new InvoiceItemPaymentStatus
                {
                    StatusId = (int) InvoiceItemPaymentStatusEnum.Paid,
                    StatusName = InvoiceItemPaymentStatusEnum.Paid.ToDescription(),
                    DisplayName = InvoiceItemPaymentStatusEnum.Paid.GetDisplayName()
                });
        }
    }
}
