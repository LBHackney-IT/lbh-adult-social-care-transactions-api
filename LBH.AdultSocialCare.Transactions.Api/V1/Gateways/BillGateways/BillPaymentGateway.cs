using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.BillsDomain;
using LBH.AdultSocialCare.Transactions.Api.V1.Exceptions.CustomExceptions;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Gateways.BillGateways
{
    public class BillPaymentGateway : IBillPaymentGateway
    {
        private readonly DatabaseContext _dbContext;

        public BillPaymentGateway(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BillPaymentDomain> GetBillPayment(long billPaymentId)
        {
            var billPayments = await _dbContext.BillPayments
                .Where(bp => bp.BillPaymentId.Equals(billPaymentId))
                .FirstOrDefaultAsync().ConfigureAwait(false);

            if (billPayments == null)
            {
                throw new EntityNotFoundException($"Unable to locate bill payment {billPaymentId.ToString()}");
            }

            return billPayments?.ToDomain();
        }
    }
}
