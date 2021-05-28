using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.BillsDomain;
using LBH.AdultSocialCare.Transactions.Api.V1.Exceptions;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Bills;
using Microsoft.EntityFrameworkCore;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Gateways.BillGateways
{
    public class BillGateway : IBillGateway
    {
        private readonly DatabaseContext _databaseContext;

        public BillGateway(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<BillDomain> CreateBillAsync(Bill bill)
        {
            var entry = await _databaseContext.Bills.AddAsync(bill)
                .ConfigureAwait(false);
            try
            {
                await _databaseContext.SaveChangesAsync().ConfigureAwait(false);
                entry.Entity.BillStatus = await _databaseContext.BillStatuses
                    .FirstOrDefaultAsync(item => item.Id == entry.Entity.BillStatusId)
                    .ConfigureAwait(false);
                return entry.Entity.ToDomain();
            }
            catch (Exception ex)
            {
                throw new DbSaveFailedException("Could not save supplier to database" + ex.Message);
            }
        }

        public async Task<BillDomain> GetBillAsync(Guid billId)
        {
            var bill = await _databaseContext.Bills
                .Where(b => b.BillId.Equals(billId))
                .AsNoTracking()
                .Include(b => b.BillStatus)
                .SingleOrDefaultAsync().ConfigureAwait(false);

            if (bill == null)
            {
                throw new EntityNotFoundException($"Unable to locate bill {billId.ToString()}");
            }

            return bill.ToDomain();
        }

        public async Task<IEnumerable<BillDomain>> GetBillList()
        {
            var bill = await _databaseContext.Bills
                .Include(dc => dc.BillStatus)
                .AsNoTracking()
                .ToListAsync().ConfigureAwait(false);
            return bill?.ToDomain();
        }
    }
}
