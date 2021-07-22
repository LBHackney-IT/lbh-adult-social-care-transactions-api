using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.SupplierReturnGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.SupplierReturnUseCases.Interfaces;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.SupplierReturnUseCases.Concrete
{
    public class AcceptAllSupplierReturnPackageItemsUseCase : IAcceptAllSupplierReturnPackageItemsUseCase
    {
        private readonly ISupplierReturnGateway _supplierReturnGateway;

        public AcceptAllSupplierReturnPackageItemsUseCase(ISupplierReturnGateway supplierReturnGateway)
        {
            _supplierReturnGateway = supplierReturnGateway;
        }

        public async Task AcceptAllSupplierReturnPackageItems(Guid supplierReturnId)
        {
            await _supplierReturnGateway.AcceptAllSupplierReturnPackageItems(supplierReturnId).ConfigureAwait(false);
        }
    }
}
