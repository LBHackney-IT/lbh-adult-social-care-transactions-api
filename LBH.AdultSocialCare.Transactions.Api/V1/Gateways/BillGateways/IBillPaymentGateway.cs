using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.BillsDomain;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Gateways.BillGateways
{
    public interface IBillPaymentGateway
    {
        Task<BillPaymentDomain> GetBillPayment(long billPaymentId);
    }
}
