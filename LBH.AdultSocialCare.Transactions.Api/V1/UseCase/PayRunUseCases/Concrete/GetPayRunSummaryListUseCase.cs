using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.PayRunBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.PayRunGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Interfaces;
using System.Collections.Generic;
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

        public async Task<IEnumerable<PayRunSummaryResponse>> Execute()
        {
            var res = await _payRunGateway.GetPayRunSummaryList().ConfigureAwait(false);
            return res.ToResponse();
        }
    }
}
