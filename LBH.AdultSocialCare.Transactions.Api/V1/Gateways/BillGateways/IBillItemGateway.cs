using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.BillsDomain;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Bills;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Gateways.BillGateways
{
    public interface IBillItemGateway
    {
        Task<BillItemDomain> CreateBillItemAsync(BillItem billItem);

        Task<IEnumerable<BillItemDomain>> GetBillItemList(long billId);
    }
}
