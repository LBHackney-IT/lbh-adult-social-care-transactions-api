using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.PayRunBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.PayRunGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.RequestExtensions;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Interfaces;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Concrete
{
    public class GetPayRunSummaryListUseCase : IGetPayRunSummaryListUseCase
    {
        private readonly IPayRunGateway _payRunGateway;

        public GetPayRunSummaryListUseCase(IPayRunGateway payRunGateway)
        {
            _payRunGateway = payRunGateway;
        }

        public async Task<PagedPayRunSummaryResponse> Execute(PayRunSummaryListParameters parameters)
        {
            var res = await _payRunGateway.GetPayRunSummaryList(parameters).ConfigureAwait(false);
            return new PagedPayRunSummaryResponse
            {
                PagingMetaData = res.PagingMetaData,
                Data = res.ToResponse()
            };
        }
    }
}
