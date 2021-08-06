using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.SupplierReturnDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.SupplierReturnGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.SupplierReturnUseCases.Interfaces;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.SupplierReturnUseCases.Concrete
{
    public class CreateDisputeItemChatUseCase : ICreateDisputeItemChatUseCase
    {
        private readonly ISupplierReturnGateway _supplierReturnGateway;

        public CreateDisputeItemChatUseCase(ISupplierReturnGateway supplierReturnGateway)
        {
            _supplierReturnGateway = supplierReturnGateway;
        }

        public async Task CreateDisputeChat(
            SupplierReturnItemDisputeConversationCreationDomain supplierReturnItemDisputeConversationCreationDomain)
        {
            var supplierReturnItemDisputeConversationEntity = supplierReturnItemDisputeConversationCreationDomain.ToDb();
            await _supplierReturnGateway.CreateDisputeItemChat(supplierReturnItemDisputeConversationEntity).ConfigureAwait(false);
        }
    }
}
