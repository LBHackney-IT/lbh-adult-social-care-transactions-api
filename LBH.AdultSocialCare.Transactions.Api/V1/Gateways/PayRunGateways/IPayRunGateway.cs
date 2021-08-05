using LBH.AdultSocialCare.Transactions.Api.V1.Domain.InvoicesDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.PackageTypeDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.PayRunDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.SupplierDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Invoices;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.PayRunModels;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.RequestExtensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Gateways.PayRunGateways
{
    public interface IPayRunGateway
    {
        Task<DateTimeOffset> GetDateOfLastPayRun(int payRunTypeId, int? payRunSubTypeId = null);

        Task<PayRunDateSummaryDomain> GetDateOfLastPayRunSummary(int payRunTypeId, int? payRunSubTypeId = null);

        Task<PagedList<PayRunSummaryDomain>> GetPayRunSummaryList(PayRunSummaryListParameters parameters);

        Task<PayRunFlatDomain> GetPayRunFlat(Guid payRunId);

        Task<PayRunItem> CheckPayRunItemExists(Guid payRunId, Guid payRunItemId);

        Task<PayRun> CheckPayRunExists(Guid payRunId);

        Task<DisputedInvoice> CheckDisputedInvoiceExists(Guid payRunId, Guid payRunItemId);

        Task<InvoiceDomain> GetSingleInvoiceInPayRun(Guid payRunId, Guid invoiceId);

        Task<bool> CheckAllInvoicesInPayRunInStatusList(Guid payRunId, List<int> invoiceStatusIds);

        Task<Guid> CreateNewPayRun(PayRun payRunForCreation);

        Task<PagedList<SupplierMinimalDomain>> GetUniqueSuppliersInPayRun(Guid payRunId, SupplierListParameters parameters);

        Task<IEnumerable<PackageTypeDomain>> GetUniquePackageTypesInPayRun(Guid payRunId);

        Task<IEnumerable<InvoiceStatusDomain>> GetUniqueInvoiceItemPaymentStatusesInPayRun(Guid payRunId);

        Task<bool> ChangePayRunStatus(Guid payRunId, int newPayRunStatusId);

        Task<bool> ReleaseHeldInvoicePayment(Guid payRunId, Guid invoiceId, Guid? invoiceItemId);

        Task<PayRunInsightsDomain> GetPayRunInsights(Guid payRunId);

        Task<IEnumerable<InvoiceDomain>> GetAllInvoicesInPayRunUsingInvoiceStatus(Guid payRunId, int invoiceStatusId);

        Task<bool> ApprovePayRunForPayment(Guid payRunId);

        Task<bool> DeleteDraftPayRun(Guid payRunId);

        Task<DisputedInvoiceChatDomain> CreateDisputedInvoiceChat(DisputedInvoiceChat disputedInvoiceChat);

        Task<IEnumerable<PayRunTypeDomain>> GetAllPayRunTypes();

        Task<IEnumerable<PayRunSubTypeDomain>> GetAllPayRunSubTypes();
    }
}
