using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.BillsDomain;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Bills;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.RequestExtensions;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Gateways.BillGateways
{
    public interface IBillGateway
    {
        Task<long> CreateBillAsync(Bill bill);

        Task<BillDomain> GetBillAsync(long billId);

        Task<IEnumerable<BillDomain>> GetBillList();

        Task<PagedList<BillSummaryDomain>> GetBill(BillSummaryListParameters parameters);
    }
}
