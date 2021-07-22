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
    public class GetDisputeChatUseCase : IGetDisputeChatUseCase
    {
        private readonly ISupplierReturnGateway _supplierReturnGateway;

        public GetDisputeChatUseCase(ISupplierReturnGateway supplierReturnGateway)
        {
            _supplierReturnGateway = supplierReturnGateway;
        }

        public async Task<IEnumerable<SupplierReturnDisputeConversationResponse>> GetDisputeChat(Guid packageId)
        {
            var result = await _supplierReturnGateway.GetDisputeChat(packageId).ConfigureAwait(false);
            return result.ToResponse();
        }
    }
}
