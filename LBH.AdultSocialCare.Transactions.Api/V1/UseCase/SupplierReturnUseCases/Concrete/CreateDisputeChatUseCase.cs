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
    public class CreateDisputeChatUseCase : ICreateDisputeChatUseCase
    {
        private readonly ISupplierReturnGateway _supplierReturnGateway;

        public CreateDisputeChatUseCase(ISupplierReturnGateway supplierReturnGateway)
        {
            _supplierReturnGateway = supplierReturnGateway;
        }

        public async Task CreateDisputeChat(SupplierReturnDisputeConversationCreationDomain supplierReturnDisputeConversationCreationDomain)
        {
            var supplierReturnDisputeConversationEntity = supplierReturnDisputeConversationCreationDomain.ToDb();
            await _supplierReturnGateway.CreateDisputeChat(supplierReturnDisputeConversationEntity).ConfigureAwait(false);
        }
    }
}
