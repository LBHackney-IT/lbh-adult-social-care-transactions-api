using LBH.AdultSocialCare.Transactions.Api.V1.Domain.InvoicesDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.PayRunDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Invoices;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.RequestExtensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Gateways.InvoiceGateways
{
    public interface IInvoiceGateway
    {
        Task<PagedList<InvoiceDomain>> GetInvoicesInPayRun(Guid payRunId, InvoiceListParameters parameters);

        Task<IEnumerable<HeldInvoiceDomain>> GetHeldInvoicePayments();

        Task<IEnumerable<InvoiceDomain>> GetInvoiceListUsingInvoiceStatus(int invoiceStatusId, DateTimeOffset? fromDate = null, DateTimeOffset? toDate = null);

        Task<IEnumerable<PayRunItemsPaymentsByTypeDomain>> GetInvoicesCountUsingStatus(int invoiceStatusId, DateTimeOffset? fromDate = null, DateTimeOffset? toDate = null);

        Task<DateTimeOffset?> GetMinDateOfReleasedInvoice(int itemPaymentStatusId);

        Task<DateTimeOffset?> GetMaxDateOfReleasedInvoice(int itemPaymentStatusId);

        Task<IEnumerable<InvoicePaymentStatusDomain>> GetInvoiceItemPaymentStatuses();

        Task<InvoiceDomain> CreateInvoice(Invoice newInvoice);

        Task<DisputedInvoiceFlatDomain> CreateDisputedInvoice(DisputedInvoice newDisputedInvoice);

        Task<bool> ChangeInvoiceStatus(Guid invoiceId, int invoiceStatusId);

        Task<IEnumerable<PendingInvoicesDomain>> GetUserPendingInvoices(Guid serviceUserId);
    }
}
