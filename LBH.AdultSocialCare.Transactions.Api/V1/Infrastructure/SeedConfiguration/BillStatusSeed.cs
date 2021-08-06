using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Bills;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.SeedConfiguration
{
    public class BillStatusSeed : IEntityTypeConfiguration<BillStatus>
    {
        public void Configure(EntityTypeBuilder<BillStatus> builder)
        {
            builder.HasData(new BillStatus
            {
                Id = 1,
                StatusName = "Outstanding"
            }, new BillStatus
            {
                Id = 2,
                StatusName = "Paid"
            }, new BillStatus
            {
                Id = 3,
                StatusName = "Paid Partially"
            }, new BillStatus
            {
                Id = 4,
                StatusName = "Overdue"
            });
        }
    }
}
