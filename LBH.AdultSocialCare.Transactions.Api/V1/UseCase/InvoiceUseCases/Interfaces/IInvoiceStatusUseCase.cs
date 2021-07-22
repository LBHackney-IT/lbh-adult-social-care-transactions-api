using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.InvoiceUseCases.Interfaces
{
    public interface IInvoiceStatusUseCase
    {
        Task<IEnumerable<InvoiceStatusResponse>> GetAllInvoiceStatusesUseCase();

        Task<IEnumerable<InvoiceStatusResponse>> GetInvoicePaymentStatusesUseCase();

        Task<bool> AcceptInvoiceUseCase(Guid payRunId, Guid invoiceId);
    }
}
