using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.BillsDomain;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Bills;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Gateways.BillGateways
{
    public interface IBillGateway
    {
        Task<BillDomain> CreateBillAsync(Bill bill);

        Task<BillDomain> GetBillAsync(Guid billId);

        Task<IEnumerable<BillDomain>> GetBillList();
    }
}