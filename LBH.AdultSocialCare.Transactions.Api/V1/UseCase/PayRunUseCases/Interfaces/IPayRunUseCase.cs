using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.PayRunBoundaries.Response;
using System;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Interfaces
{
    public interface IPayRunUseCase
    {
        Task<PayRunInsightsResponse> GetSinglePayRunInsightsUseCase(Guid payRunId);
    }
}
