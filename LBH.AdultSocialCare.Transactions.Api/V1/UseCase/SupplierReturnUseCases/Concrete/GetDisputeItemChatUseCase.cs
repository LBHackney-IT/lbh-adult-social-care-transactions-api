using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.SupplierReturnBoundary.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.SupplierReturnGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.SupplierReturnUseCases.Interfaces;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.SupplierReturnUseCases.Concrete
{
    public class GetDisputeItemChatUseCase : IGetDisputeItemChatUseCase
    {
        private readonly ISupplierReturnGateway _supplierReturnGateway;

        public GetDisputeItemChatUseCase(ISupplierReturnGateway supplierReturnGateway)
        {
            _supplierReturnGateway = supplierReturnGateway;
        }

        public async Task<IEnumerable<SupplierReturnItemDisputeConversationResponse>> GetDisputeItemChat(Guid packageId, Guid supplierReturnItemId)
        {
            var result = await _supplierReturnGateway.GetDisputeItemChat(packageId, supplierReturnItemId).ConfigureAwait(false);
            return result.ToResponse();
        }
    }
}
