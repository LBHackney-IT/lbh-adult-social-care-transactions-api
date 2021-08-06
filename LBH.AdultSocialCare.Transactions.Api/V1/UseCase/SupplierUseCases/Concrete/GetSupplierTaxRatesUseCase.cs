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
    public class GetSupplierTaxRatesUseCase : IGetSupplierTaxRatesUseCase
    {
        private readonly ISupplierGateway _supplierGateway;

        public GetSupplierTaxRatesUseCase(ISupplierGateway supplierGateway)
        {
            _supplierGateway = supplierGateway;
        }

        public async Task<IEnumerable<SupplierTaxRateResponse>> GetSupplierTaxRates(long supplierId)
        {
            var result = await _supplierGateway.GetSupplierTaxRates(supplierId).ConfigureAwait(false);
            return result?.ToResponse();
        }
    }
}
