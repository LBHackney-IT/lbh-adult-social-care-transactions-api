using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.SeedConfiguration
{
    public class DepartmentsSeed : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasData(new Department { DepartmentId = 1, DepartmentName = "Brokerage" },
                new Department { DepartmentId = 2, DepartmentName = "Finance" });
        }
    }
}
