using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.PayRunBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.RequestExtensions;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Interfaces
{
    public interface IGetPayRunSummaryListUseCase
    {
        Task<PagedPayRunSummaryResponse> Execute(PayRunSummaryListParameters parameters);
    }
}
