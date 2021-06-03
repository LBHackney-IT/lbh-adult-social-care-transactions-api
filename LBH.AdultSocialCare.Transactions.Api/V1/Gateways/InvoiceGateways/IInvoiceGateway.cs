using System;
using System.Collections.Generic;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.BillsDomain.InvoicesDomains;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Gateways.InvoiceGateways
{
    public interface IInvoiceGateway
    {
        Task<IEnumerable<InvoiceDomain>> GetInvoicesUsingItemPaymentStatus(int itemPaymentStatusId, DateTimeOffset? fromDate = null);
    }
}
