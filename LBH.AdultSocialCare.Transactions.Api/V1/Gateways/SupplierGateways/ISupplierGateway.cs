using LBH.AdultSocialCare.Transactions.Api.V1.Domain.SupplierDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Suppliers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Gateways.SupplierGateways
{
    public interface ISupplierGateway
    {
        Task<long> CreateCreditNotes(SupplierCreditNote supplierCreditNote);

        Task<IEnumerable<SupplierDomain>> GetSuppliers(string searchTerm);

        Task<IEnumerable<SupplierTaxRateDomain>> GetSupplierTaxRates(long supplierId);

        Task<SupplierTaxRateDomain> GetLatestSupplierTaxRate(long supplierId);

        Task<Supplier> CheckSupplierExists(long supplierId);
    }
}
