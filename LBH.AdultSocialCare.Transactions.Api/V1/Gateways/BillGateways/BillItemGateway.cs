using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.BillsDomain;
using LBH.AdultSocialCare.Transactions.Api.V1.Exceptions;
using LBH.AdultSocialCare.Transactions.Api.V1.Exceptions.CustomExceptions;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Bills;
using Microsoft.EntityFrameworkCore;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Gateways.BillGateways
{
    public class BillItemGateway : IBillItemGateway
    {
        private readonly DatabaseContext _databaseContext;

        public BillItemGateway(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<BillItemDomain> CreateBillItemAsync(BillItem billItem)
        {
            var entry = await _databaseContext.BillItems.AddAsync(billItem).ConfigureAwait(false);
            try
            {
                await _databaseContext.SaveChangesAsync().ConfigureAwait(false);
                return entry.Entity.ToDomain();
            }
            catch (Exception)
            {
                throw new DbSaveFailedException("Could not save supplier to database");
            }
        }

        public async Task<IEnumerable<BillItemDomain>> GetBillItemList(Guid billId)
        {
            var billItem = await _databaseContext.BillItems
                .Where(b => b.BillId.Equals(billId))
                .AsNoTracking()
                .ToListAsync().ConfigureAwait(false);
            return billItem?.ToDomain();
        }
    }
}
