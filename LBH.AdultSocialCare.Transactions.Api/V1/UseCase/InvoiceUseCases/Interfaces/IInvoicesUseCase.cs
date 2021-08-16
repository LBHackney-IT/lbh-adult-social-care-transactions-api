using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.InvoicesDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.RequestExtensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.InvoiceUseCases.Interfaces
{
    public interface IInvoicesUseCase
    {
        Task<DisputedInvoiceFlatResponse> HoldInvoicePaymentUseCase(Guid payRunId, Guid invoiceId,
            DisputedInvoiceForCreationDomain disputedInvoiceForCreationDomain);

        Task<DisputedInvoiceChatResponse> CreateDisputedInvoiceChatUseCase(
            DisputedInvoiceChatForCreationDomain disputedInvoiceChatForCreationDomain);

        // Task<bool> ChangeInvoiceStatusUseCase(Guid invoiceId, int invoiceStatusId);

        Task<InvoiceResponse> CreateInvoiceUseCase(InvoiceForCreationDomain invoiceForCreationDomain);

        Task<IEnumerable<InvoiceResponse>> BatchCreateInvoicesUseCase(IEnumerable<InvoiceForCreationDomain> invoicesForCreationDomain);

        Task<IEnumerable<InvoiceResponse>> GetInvoicesFlatInPayRunUseCase(Guid payRunId, InvoiceListParameters parameters);
    }
}
