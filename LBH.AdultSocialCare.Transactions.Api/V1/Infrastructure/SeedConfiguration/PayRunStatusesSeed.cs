using LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums;
using LBH.AdultSocialCare.Transactions.Api.V1.Extensions;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.PayRunEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.SeedConfiguration
{
    public class PayRunStatusesSeed : IEntityTypeConfiguration<PayRunStatus>
    {
        public void Configure(EntityTypeBuilder<PayRunStatus> builder)
        {
            builder.HasData(
                new PayRunStatus
                {
                    PayRunStatusId = (int) PayRunStatusesEnum.Draft,
                    StatusName = PayRunStatusesEnum.Draft.ToDescription()
                },
                new PayRunStatus
                {
                    PayRunStatusId = (int) PayRunStatusesEnum.Approved,
                    StatusName = PayRunStatusesEnum.Approved.ToDescription()
                });
        }
    }
}
