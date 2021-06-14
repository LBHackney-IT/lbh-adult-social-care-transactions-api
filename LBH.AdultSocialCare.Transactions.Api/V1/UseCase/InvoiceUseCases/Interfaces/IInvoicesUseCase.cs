using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.InvoicesDomains;
using System;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.InvoiceUseCases.Interfaces
{
    public interface IInvoicesUseCase
    {
        Task<DisputedInvoiceFlatResponse> HoldInvoicePaymentUseCase(
            DisputedInvoiceForCreationDomain disputedInvoiceForCreationDomain);

        Task<bool> ChangeInvoiceStatusUseCase(Guid invoiceId, int invoiceStatusId);

        Task<bool> ChangeInvoiceItemPaymentStatusUseCase(Guid payRunId, Guid invoiceItemId, int invoiceItemPaymentStatusId);
    }
}
