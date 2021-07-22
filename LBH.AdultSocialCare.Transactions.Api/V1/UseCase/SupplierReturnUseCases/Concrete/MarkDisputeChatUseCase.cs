using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.SupplierReturnGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.SupplierReturnUseCases.Interfaces;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.SupplierReturnUseCases.Concrete
{
    public class MarkDisputeChatUseCase : IMarkDisputeChatUseCase
    {
        private readonly ISupplierReturnGateway _supplierReturnGateway;

        public MarkDisputeChatUseCase(ISupplierReturnGateway supplierReturnGateway)
        {
            _supplierReturnGateway = supplierReturnGateway;
        }

        public async Task MarkDisputeChat(Guid packageId, Guid disputeConversationId, bool messageRead)
        {
            await _supplierReturnGateway.MarkDisputeChat(packageId, disputeConversationId, messageRead).ConfigureAwait(false);
        }
    }
}
