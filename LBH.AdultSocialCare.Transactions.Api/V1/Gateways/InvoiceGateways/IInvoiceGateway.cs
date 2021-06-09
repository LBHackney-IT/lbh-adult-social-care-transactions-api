using LBH.AdultSocialCare.Transactions.Api.V1.Domain.InvoicesDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.PayRunDomains;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Gateways.InvoiceGateways
{
    public interface IInvoiceGateway
    {
        Task<IEnumerable<InvoiceDomain>> GetInvoicesUsingItemPaymentStatus(int itemPaymentStatusId, DateTimeOffset? fromDate = null, DateTimeOffset? toDate = null);

        Task<IEnumerable<InvoiceItemMinimalDomain>> GetInvoiceItemsUsingItemPaymentStatus(int itemPaymentStatusId, DateTimeOffset? fromDate = null, DateTimeOffset? toDate = null);

        Task<IEnumerable<PayRunItemsPaymentsByTypeDomain>> GetInvoiceItemsCountUsingItemPaymentStatus(int itemPaymentStatusId, DateTimeOffset? fromDate = null, DateTimeOffset? toDate = null);

        Task<DateTimeOffset?> GetMinDateOfReleasedInvoiceItem(int itemPaymentStatusId);

        Task<DateTimeOffset?> GetMaxDateOfReleasedInvoiceItem(int itemPaymentStatusId);
    }
}
