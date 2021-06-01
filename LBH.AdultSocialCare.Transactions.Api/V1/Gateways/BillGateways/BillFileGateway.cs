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
    public class BillFileGateway : IBillFileGateway
    {
        private readonly DatabaseContext _databaseContext;

        public BillFileGateway(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<BillFileDomain> CreateAsync(BillFile billFile)
        {
            var entry = await _databaseContext.BillFiles.AddAsync(billFile).ConfigureAwait(false);
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

        public async Task<BillFileDomain> GetBillFileAsync(Guid billId)
        {
            var billFile = await _databaseContext.BillFiles
                .Where(b => b.BillId.Equals(billId))
                .AsNoTracking()
                .SingleOrDefaultAsync().ConfigureAwait(false);

            if (billFile == null)
            {
                throw new EntityNotFoundException($"Unable to locate bill file {billId.ToString()}");
            }

            return billFile.ToDomain();
        }
    }
}
