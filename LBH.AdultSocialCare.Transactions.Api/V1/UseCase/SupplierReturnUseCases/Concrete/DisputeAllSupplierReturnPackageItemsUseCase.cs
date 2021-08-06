using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.SupplierReturnDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.SupplierReturnGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.SupplierReturnUseCases.Interfaces;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.SupplierReturnUseCases.Concrete
{
    public class DisputeAllSupplierReturnPackageItemsUseCase : IDisputeAllSupplierReturnPackageItemsUseCase
    {
        private readonly ISupplierReturnGateway _supplierReturnGateway;
        private readonly ICreateDisputeItemChatUseCase _createDisputeItemChatUseCase;

        public DisputeAllSupplierReturnPackageItemsUseCase(ISupplierReturnGateway supplierReturnGateway,
            ICreateDisputeItemChatUseCase createDisputeItemChatUseCase)
        {
            _supplierReturnGateway = supplierReturnGateway;
            _createDisputeItemChatUseCase = createDisputeItemChatUseCase;
        }

        public async Task DisputeAllSupplierReturnPackageItems(Guid supplierReturnId, string message)
        {
            await _supplierReturnGateway.DisputeAllSupplierReturnPackageItems(supplierReturnId).ConfigureAwait(false);
            var supplierReturn = await _supplierReturnGateway.GetSupplierReturn(supplierReturnId).ConfigureAwait(false);
            foreach (var supplierReturnItem in supplierReturn.SupplierReturnItem)
            {
                var supplierReturnItemDisputeConversation = new SupplierReturnItemDisputeConversationCreationDomain
                {
                    PackageId = supplierReturn.PackageId,
                    SupplierReturnItemId = supplierReturnItem.ItemId,
                    Message = message,
                    MessageFrom = 1 //todo department id 
                };

                await _createDisputeItemChatUseCase.CreateDisputeChat(supplierReturnItemDisputeConversation).ConfigureAwait(false);
            }
            //todo notify supplier via email
        }
    }
}
