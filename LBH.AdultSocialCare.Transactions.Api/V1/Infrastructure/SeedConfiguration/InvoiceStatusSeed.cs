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
            builder.HasData(
                new InvoiceStatus
                {
                    Id = (int) InvoiceStatusEnum.Draft,
                    StatusName = InvoiceStatusEnum.Draft.ToDescription(),
                    DisplayName = InvoiceStatusEnum.Draft.GetDisplayName(),
                    ApprovalStatus = false
                },
                new InvoiceStatus
                {
                    Id = (int) InvoiceStatusEnum.Approved,
                    StatusName = InvoiceStatusEnum.Approved.ToDescription(),
                    DisplayName = InvoiceStatusEnum.Approved.GetDisplayName(),
                    ApprovalStatus = false
                },
                new InvoiceStatus
                {
                    Id = (int) InvoiceStatusEnum.InPayRun,
                    StatusName = InvoiceStatusEnum.InPayRun.ToDescription(),
                    DisplayName = InvoiceStatusEnum.InPayRun.GetDisplayName(),
                    ApprovalStatus = false
                },
                new InvoiceStatus
                {
                    Id = (int) InvoiceStatusEnum.Held,
                    StatusName = InvoiceStatusEnum.Held.ToDescription(),
                    DisplayName = InvoiceStatusEnum.Held.GetDisplayName(),
                    ApprovalStatus = false
                },
                new InvoiceStatus
                {
                    Id = (int) InvoiceStatusEnum.Accepted,
                    StatusName = InvoiceStatusEnum.Accepted.ToDescription(),
                    DisplayName = InvoiceStatusEnum.Accepted.GetDisplayName(),
                    ApprovalStatus = false
                },
                new InvoiceStatus
                {
                    Id = (int) InvoiceStatusEnum.Released,
                    StatusName = InvoiceStatusEnum.Released.ToDescription(),
                    DisplayName = InvoiceStatusEnum.Released.GetDisplayName(),
                    ApprovalStatus = false
                },
                new InvoiceStatus
                {
                    Id = (int) InvoiceStatusEnum.Paid,
                    StatusName = InvoiceStatusEnum.Paid.ToDescription(),
                    DisplayName = InvoiceStatusEnum.Paid.GetDisplayName(),
                    ApprovalStatus = false
                });
        }
    }
}
