using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.PayRunBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.RequestExtensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Interfaces
{
    public interface IPayRunUseCase
    {
        Task<PayRunInsightsResponse> GetSinglePayRunInsightsUseCase(Guid payRunId);

        Task<PagedHeldInvoiceResponse> GetHeldInvoicePaymentsUseCase(HeldInvoicePaymentParameters parameters);

        Task<bool> ApprovePayRunForPaymentUseCase(Guid payRunId);

        Task<bool> DeleteDraftPayRunUseCase(Guid payRunId);

        Task<PayRunDateSummaryResponse> GetDateOfLastPayRunUseCase(string payRunType);

        Task<IEnumerable<PayRunTypeResponse>> GetAllPayRunTypesUseCase();

        Task<IEnumerable<PayRunSubTypeResponse>> GetAllPayRunSubTypesUseCase();

        Task<IEnumerable<PayRunStatusResponse>> GetAllUniquePayRunStatusesUseCase();

        Task<bool> RejectInvoiceInPayRun(Guid payRunId, Guid invoiceId);
    }
}
