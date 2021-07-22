using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.SupplierReturnBoundary.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.SupplierReturnGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.SupplierReturnUseCases.Interfaces;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.SupplierReturnUseCases.Concrete
{
    public class GetSingleSupplierReturnInsightsUseCase : IGetSingleSupplierReturnInsightsUseCase
    {
        private readonly ISupplierReturnGateway _supplierReturnGateway;

        public GetSingleSupplierReturnInsightsUseCase(ISupplierReturnGateway supplierReturnGateway)
        {
            _supplierReturnGateway = supplierReturnGateway;
        }

        public async Task<SupplierReturnInsightsResponse> GetSingleSupplierReturnInsights(Guid suppliersReturnsId)
        {
           var result = await _supplierReturnGateway.GetSingleSupplierReturnInsights(suppliersReturnsId).ConfigureAwait(false);
           return result.ToResponse();
        }
    }
}
