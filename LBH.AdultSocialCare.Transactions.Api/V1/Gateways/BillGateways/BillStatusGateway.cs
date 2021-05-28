using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.BillsDomain;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Gateways.BillGateways
{
    public class BillStatusGateway : IBillStatusGateway
    {
        private readonly DatabaseContext _databaseContext;

        public BillStatusGateway(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<IEnumerable<BillStatusDomain>> GetBillStatusList()
        {
            var billStatus = await _databaseContext.BillStatuses
                .AsNoTracking()
                .ToListAsync().ConfigureAwait(false);
            return billStatus?.ToDomain();
        }
    }
}
