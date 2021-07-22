using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.LedgerDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Gateways.LedgerGateways
{
    public interface ILedgerGateway
    {
        Task<LedgerDomain> GetLedger(long ledgerId);

        Task<bool> CreateLedger(Ledger ledgerEntity);
    }
}
