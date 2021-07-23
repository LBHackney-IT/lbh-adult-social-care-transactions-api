using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.PayRunGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Interfaces;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Concrete
{
    public class CreatePayRunHeldChatUseCase : ICreatePayRunHeldChatUseCase
    {
        private readonly IPayRunGateway _payRunGateway;

        public CreatePayRunHeldChatUseCase(IPayRunGateway payRunGateway)
        {
            _payRunGateway = payRunGateway;
        }

        public async Task<bool> CreatePayRunHeldChat(Guid payRunId, Guid packageId, string message)
        {
            return await _payRunGateway.CreatePayRunHeldChat(payRunId, packageId, message).ConfigureAwait(false);
        }
    }
}
