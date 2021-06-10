using LBH.AdultSocialCare.Transactions.Api.V1.Domain.InvoicesDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.PackageTypeDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.PayRunDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.SupplierDomains;
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

        Task<PagedList<PayRunSummaryDomain>> GetPayRunSummaryList(PayRunSummaryListParameters parameters);

        Task<PayRunFlatDomain> GetPayRunFlat(Guid payRunId);

        Task<Guid> CreateNewPayRun(PayRun payRunForCreation);

        Task<PagedList<SupplierMinimalDomain>> GetUniqueSuppliersInPayRun(Guid payRunId, SupplierListParameters parameters);

        Task<IEnumerable<PackageTypeDomain>> GetUniquePackageTypesInPayRun(Guid payRunId);

        Task<IEnumerable<InvoiceItemPaymentStatusDomain>> GetUniqueInvoiceItemPaymentStatusesInPayRun(Guid payRunId);

        Task<bool> ChangePayRunStatus(Guid payRunId, int newPayRunStatusId);

        Task<bool> ReleaseHeldInvoiceItemPayment(Guid payRunId, Guid invoiceId, Guid invoiceItemId);
    }
}
