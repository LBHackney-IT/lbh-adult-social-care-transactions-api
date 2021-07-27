using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.InvoicesDomains;
using System;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.InvoiceUseCases.Interfaces
{
    public interface IInvoicesUseCase
    {
        Task<DisputedInvoiceFlatResponse> HoldInvoicePaymentUseCase(Guid payRunId, Guid payRunItemId,
            DisputedInvoiceForCreationDomain disputedInvoiceForCreationDomain);

        Task<Guid> CreateDisputedInvoiceChatUseCase(
            DisputedInvoiceChatForCreationDomain disputedInvoiceChatForCreationDomain);

        // Task<bool> ChangeInvoiceStatusUseCase(Guid invoiceId, int invoiceStatusId);

        Task<InvoiceResponse> CreateInvoiceUseCase(InvoiceForCreationDomain invoiceForCreationDomain);
    }
}
