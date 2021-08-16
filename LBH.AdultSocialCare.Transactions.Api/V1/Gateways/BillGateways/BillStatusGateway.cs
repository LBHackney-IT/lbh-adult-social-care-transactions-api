using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.CustomExceptions;
using LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums;
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

        public async Task CheckAndSetBillStatus(long billId)
        {
            var bill = await _databaseContext.Bills
                .Where(b => b.BillId.Equals(billId))
                .SingleOrDefaultAsync().ConfigureAwait(false);

            if (bill == null)
                return;

            var billPaymentPaidAmount = await _databaseContext.BillPayments
                .Where(bp => bp.BillId.Equals(billId))
                .SumAsync(x => x.PaidAmount).ConfigureAwait(false);

            bill.BillPaymentStatusId = (int) BillStatusEnum.OutstandingId;

            if (billPaymentPaidAmount > 0)
                if (billPaymentPaidAmount == bill.TotalBilled)
                    bill.BillPaymentStatusId = (int) BillStatusEnum.PaidId;
                else
                    bill.BillPaymentStatusId = (int) BillStatusEnum.PaidPartiallyId;
            else if (bill.BillDueDate < DateTimeOffset.Now) bill.BillPaymentStatusId = (int) BillStatusEnum.OverdueId;

            try
            {
                await _databaseContext.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw new DbSaveFailedException($"Update status for bill {billId} failed");
            }
        }
    }
}
