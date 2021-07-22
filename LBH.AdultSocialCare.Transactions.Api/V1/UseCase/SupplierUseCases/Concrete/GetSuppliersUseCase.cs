using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.SupplierBoundary.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.SupplierGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.RequestExtensions;
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

        public async Task<PagedSupplierResponse> GetSupplierUseCase(SupplierListParameters parameters)
        {
            var result = await _supplierGateway.GetSuppliers(parameters).ConfigureAwait(false);
            return new PagedSupplierResponse
            {
                PagingMetaData = result.PagingMetaData,
                Data = result.ToResponse()
            };
        }
    }
}
