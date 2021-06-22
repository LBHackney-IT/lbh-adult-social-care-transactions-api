using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.PayRunBoundaries.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Interfaces
{
    public interface IPayRunUseCase
    {
        Task<PayRunInsightsResponse> GetSinglePayRunInsightsUseCase(Guid payRunId);

        Task<IEnumerable<HeldInvoiceResponse>> GetHeldInvoicePaymentsUseCase();

        Task<bool> ApprovePayRunForPaymentUseCase(Guid payRunId);
    }
}
