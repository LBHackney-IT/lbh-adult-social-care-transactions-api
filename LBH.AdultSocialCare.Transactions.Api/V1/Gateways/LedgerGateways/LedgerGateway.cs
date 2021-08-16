using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.CustomExceptions;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.LedgerDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Gateways.LedgerGateways
{
    public class LedgerGateway : ILedgerGateway
    {
        private readonly DatabaseContext _dbContext;

        public LedgerGateway(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<LedgerDomain> GetLedger(long ledgerId)
        {
            var ledgerEntity = await _dbContext.Ledgers
                .Where(l => l.LedgerId.Equals(ledgerId))
                .FirstOrDefaultAsync().ConfigureAwait(false);

            if (ledgerEntity == null)
            {
                throw new EntityNotFoundException($"Unable to locate ledger {ledgerId.ToString()}");
            }

            return ledgerEntity?.ToDomain();
        }

        public async Task<bool> CreateLedger(Ledger ledgerEntity)
        {
            var entry = await _dbContext.Ledgers.AddAsync(ledgerEntity).ConfigureAwait(false);
            try
            {
                bool isSuccess = await _dbContext.SaveChangesAsync().ConfigureAwait(false) == 1;
                return isSuccess;
            }
            catch (Exception)
            {
                throw new DbSaveFailedException("Could not save approval history to database");
            }
        }
    }
}
