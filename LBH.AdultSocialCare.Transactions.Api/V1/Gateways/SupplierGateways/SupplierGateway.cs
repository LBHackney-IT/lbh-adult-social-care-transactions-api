using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.SupplierDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Exceptions.CustomExceptions;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Suppliers;
using Microsoft.EntityFrameworkCore;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Gateways.SupplierGateways
{
    public class SupplierGateway : ISupplierGateway
    {
        private readonly DatabaseContext _dbContext;

        public SupplierGateway(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<long> CreateCreditNotes(SupplierCreditNote supplierCreditNote)
        {
            var entry = await _dbContext.SupplierCreditNotes.AddAsync(supplierCreditNote).ConfigureAwait(false);
            try
            {
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
                return entry.Entity.CreditNoteId;
            }
            catch (Exception)
            {
                throw new DbSaveFailedException("Could not save supplier credit note to database");
            }
        }

        public async Task<IEnumerable<SupplierDomain>> GetSuppliers(string searchTerm)
        {
            var res = await _dbContext.Suppliers
                    .Where(pr =>
                       searchTerm != null ? pr.SupplierName.ToLower().Contains(searchTerm.ToLower()) : pr.Equals(pr))
                .ToListAsync().ConfigureAwait(false);
            return res?.ToDomain();
        }

        public async Task<IEnumerable<SupplierTaxRateDomain>> GetSupplierTaxRates(long supplierId)
        {
            var supplierTaxRates = await _dbContext.SupplierTaxRates
                .Where(s => s.SupplierId.Equals(supplierId))
                .AsNoTracking()
                .ToListAsync().ConfigureAwait(false);
            return supplierTaxRates?.ToDomain();
        }
    }
}
