using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.SupplierBoundary.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.SupplierGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.SupplierUseCases.Interfaces;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.SupplierUseCases.Concrete
{
    public class GetSuppliersUseCase : IGetSuppliersUseCase
    {
        private readonly ISupplierGateway _supplierGateway;

        public GetSuppliersUseCase(ISupplierGateway supplierGateway)
        {
            _supplierGateway = supplierGateway;
        }

        public async Task<IEnumerable<SupplierResponse>> GetSupplierUseCase(string searchTerm)
        {
            var result = await _supplierGateway.GetSuppliers(searchTerm).ConfigureAwait(false);
            return result?.ToResponse();
        }
    }
}
