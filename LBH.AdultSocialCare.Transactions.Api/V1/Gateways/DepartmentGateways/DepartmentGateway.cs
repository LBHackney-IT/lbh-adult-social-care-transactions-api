using LBH.AdultSocialCare.Transactions.Api.V1.Domain.DepartmentDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Gateways.DepartmentGateways
{
    public class DepartmentGateway : IDepartmentGateway
    {
        private readonly DatabaseContext _dbContext;

        public DepartmentGateway(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<DepartmentDomain>> GetPaymentDepartments()
        {
            return await _dbContext.Departments.Select(d => new DepartmentDomain
            {
                DepartmentId = d.DepartmentId,
                DepartmentName = d.DepartmentName
            }).ToListAsync()
                .ConfigureAwait(false);
        }
    }
}
