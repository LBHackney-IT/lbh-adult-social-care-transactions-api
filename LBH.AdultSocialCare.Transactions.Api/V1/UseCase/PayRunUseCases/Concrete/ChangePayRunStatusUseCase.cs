using LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.PayRunGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Interfaces;
using System;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Concrete
{
    public class ChangePayRunStatusUseCase : IChangePayRunStatusUseCase
    {
        private readonly IPayRunGateway _payRunGateway;

        public ChangePayRunStatusUseCase(IPayRunGateway payRunGateway)
        {
            _payRunGateway = payRunGateway;
        }

        public async Task<bool> SubmitPayRunForApproval(Guid payRunId)
        {
            return await _payRunGateway.ChangePayRunStatus(payRunId, (int) PayRunStatusesEnum.SubmittedForApproval)
                .ConfigureAwait(false);
        }

        public async Task<bool> KickBackPayRunToDraft(Guid payRunId)
        {
            return await _payRunGateway.ChangePayRunStatus(payRunId, (int) PayRunStatusesEnum.Draft)
                .ConfigureAwait(false);
        }
    }
}
