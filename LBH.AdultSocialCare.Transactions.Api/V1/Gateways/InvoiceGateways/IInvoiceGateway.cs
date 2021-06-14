using LBH.AdultSocialCare.Transactions.Api.V1.Domain.InvoicesDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.PayRunDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.RequestExtensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Invoices;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Gateways.InvoiceGateways
{
    public interface IInvoiceGateway
    {
        Task<IEnumerable<InvoiceDomain>> GetInvoicesUsingItemPaymentStatus(int itemPaymentStatusId, DateTimeOffset? fromDate = null, DateTimeOffset? toDate = null);

        Task<PagedList<InvoiceDomain>> GetInvoicesInPayRun(Guid payRunId, InvoiceListParameters parameters);

        Task<IEnumerable<InvoiceItemMinimalDomain>> GetInvoiceItemsUsingItemPaymentStatus(int itemPaymentStatusId, DateTimeOffset? fromDate = null, DateTimeOffset? toDate = null);

        Task<IEnumerable<PayRunItemsPaymentsByTypeDomain>> GetInvoiceItemsCountUsingItemPaymentStatus(int itemPaymentStatusId, DateTimeOffset? fromDate = null, DateTimeOffset? toDate = null);

        Task<DateTimeOffset?> GetMinDateOfReleasedInvoiceItem(int itemPaymentStatusId);

        Task<DateTimeOffset?> GetMaxDateOfReleasedInvoiceItem(int itemPaymentStatusId);

        Task<IEnumerable<InvoiceItemPaymentStatusDomain>> GetInvoiceItemPaymentStatuses();

        Task<DisputedInvoiceFlatDomain> CreateDisputedInvoice(DisputedInvoice newDisputedInvoice);

        Task<bool> ChangeInvoiceStatus(Guid invoiceId, int invoiceStatusId);
    }
}
