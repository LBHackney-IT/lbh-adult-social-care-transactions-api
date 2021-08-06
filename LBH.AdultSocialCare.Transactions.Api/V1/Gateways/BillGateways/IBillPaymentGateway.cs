using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.BillsDomain;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Bills;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Gateways.BillGateways
{
    public interface IBillPaymentGateway
    {
        Task<BillPaymentDomain> GetBillPayment(long billPaymentId);
        Task<BillPaymentDomain> GetBillPaymentByBillId(long billId);
        Task<decimal> GetTotalBillPayment(long billId);

        Task<long> CreateBillPayment(BillPayment billPayment);
    }
}
