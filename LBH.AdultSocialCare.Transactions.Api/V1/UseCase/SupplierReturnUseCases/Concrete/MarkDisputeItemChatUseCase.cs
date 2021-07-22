using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.SupplierReturnGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.SupplierReturnUseCases.Interfaces;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.SupplierReturnUseCases.Concrete
{
    public class MarkDisputeItemChatUseCase : IMarkDisputeItemChatUseCase
    {
        private readonly ISupplierReturnGateway _supplierReturnGateway;

        public MarkDisputeItemChatUseCase(ISupplierReturnGateway supplierReturnGateway)
        {
            _supplierReturnGateway = supplierReturnGateway;
        }

        public async Task MarkDisputeItemChat(Guid packageId, Guid supplierReturnItemId, Guid disputeConversationId, bool messageRead)
        {
            await _supplierReturnGateway.MarkDisputeItemChat(packageId, supplierReturnItemId, disputeConversationId, messageRead).ConfigureAwait(false);
        }
    }
}
