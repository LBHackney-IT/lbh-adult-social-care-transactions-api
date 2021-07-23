using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.SupplierReturnGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.SupplierReturnUseCases.Interfaces;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.SupplierReturnUseCases.Concrete
{
    public class ChangeSupplierReturnPackageValuesUseCase : IChangeSupplierReturnPackageValuesUseCase
    {
        private readonly ISupplierReturnGateway _supplierReturnGateway;

        public ChangeSupplierReturnPackageValuesUseCase(ISupplierReturnGateway supplierReturnGateway)
        {
            _supplierReturnGateway = supplierReturnGateway;
        }

        public async Task ChangeSupplierReturnPackageValues(Guid supplierReturnId, Guid supplierReturnItemId, float hoursDelivered,
            float actualVisits, string comment)
        {
            await _supplierReturnGateway.ChangeSupplierReturnPackageValues(supplierReturnId, supplierReturnItemId, hoursDelivered, actualVisits, comment).ConfigureAwait(false);
        }
    }
}
