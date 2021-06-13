using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.SupplierDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Suppliers;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Gateways.SupplierGateways
{
    public interface ISupplierGateway
    {
        Task<long> CreateCreditNotes(SupplierCreditNote supplierCreditNote);

        Task<IEnumerable<SupplierDomain>> GetSuppliers(string searchTerm);

        Task<IEnumerable<SupplierTaxRateDomain>> GetSupplierTaxRates(long supplierId);
    }
}
