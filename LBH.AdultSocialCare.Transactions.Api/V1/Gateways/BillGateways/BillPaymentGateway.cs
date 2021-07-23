using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.BillsDomain;
using LBH.AdultSocialCare.Transactions.Api.V1.Exceptions.CustomExceptions;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Bills;
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

        public async Task<BillPaymentDomain> GetBillPaymentByBillId(long billId)
        {
            var billPayments = await _dbContext.BillPayments
                .Where(bp => bp.BillId.Equals(billId))
                .FirstOrDefaultAsync().ConfigureAwait(false);

            if (billPayments == null)
            {
                throw new EntityNotFoundException($"Unable to locate bill payment by bill id {billId.ToString()}");
            }

            return billPayments?.ToDomain();
        }

        public async Task<decimal> GetTotalBillPayment(long billId)
        {
            return await _dbContext.BillPayments
                .Where(bp => bp.BillId.Equals(billId))
                .SumAsync(x => x.PaidAmount).ConfigureAwait(false);
        }

        public async Task<long> CreateBillPayment(BillPayment billPayment)
        {
            var entry = await _dbContext.BillPayments.AddAsync(billPayment).ConfigureAwait(false);
            try
            {
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
                return entry.Entity.BillPaymentId;
            }
            catch (Exception ex)
            {
                throw new DbSaveFailedException($"Could not save bill payment to database {ex.Message}");
            }
        }
    }
}
