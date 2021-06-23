using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Request;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.InvoicesDomains;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.InvoiceUseCases.Interfaces
{
    public interface IInvoicesUseCase
    {
        Task<DisputedInvoiceFlatResponse> HoldInvoicePaymentUseCase(Guid payRunId, Guid payRunItemId,
            DisputedInvoiceForCreationDomain disputedInvoiceForCreationDomain);

        Task<bool> ChangeInvoiceStatusUseCase(Guid invoiceId, int invoiceStatusId);

        Task<bool> ReleaseSingleInvoiceUseCase(Guid invoiceId);

        Task<bool> ReleaseMultipleInvoicesUseCase(IEnumerable<Guid> invoiceIds);

        Task<InvoiceResponse> CreateInvoiceUseCase(InvoiceForCreationDomain invoiceForCreationDomain);
    }
}
