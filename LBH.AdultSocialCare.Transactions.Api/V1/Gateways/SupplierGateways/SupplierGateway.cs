using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.SupplierDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Exceptions.CustomExceptions;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Suppliers;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.RequestExtensions;
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

        public async Task<PagedList<SupplierDomain>> GetSuppliers(SupplierListParameters parameters)
        {
            var supplierList = await _dbContext.Suppliers
                    .Where(pr =>
                        parameters.SearchTerm != null ? pr.SupplierName.ToLower().Contains(parameters.SearchTerm.ToLower()) : pr.Equals(pr))
                    .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                    .Take(parameters.PageSize)
                    .ToListAsync().ConfigureAwait(false);

            var supplierCount = await _dbContext.Suppliers
                .Where(pr =>
                    parameters.SearchTerm != null ? pr.SupplierName.ToLower().Contains(parameters.SearchTerm.ToLower()) : pr.Equals(pr))
                .CountAsync().ConfigureAwait(false);

            return PagedList<SupplierDomain>.ToPagedList(supplierList.ToDomain(), supplierCount, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<IEnumerable<SupplierTaxRateDomain>> GetSupplierTaxRates(long supplierId)
        {
            var supplierTaxRates = await _dbContext.SupplierTaxRates
                .Where(s => s.SupplierId.Equals(supplierId))
                .AsNoTracking()
                .ToListAsync().ConfigureAwait(false);
            return supplierTaxRates?.ToDomain();
        }

        public async Task<SupplierTaxRateDomain> GetLatestSupplierTaxRate(long supplierId)
        {
            var supplierTaxRate = await _dbContext.SupplierTaxRates
                .Where(s => s.SupplierId.Equals(supplierId))
                .OrderByDescending(s => s.TaxRateId)
                .AsNoTracking()
                .FirstOrDefaultAsync().ConfigureAwait(false);
            return supplierTaxRate?.ToDomain();
        }

        public async Task<Supplier> CheckSupplierExists(long supplierId)
        {
            var supplier = await _dbContext.Suppliers.Where(pr => pr.SupplierId.Equals(supplierId)).SingleOrDefaultAsync()
                .ConfigureAwait(false);

            // if not found return
            if (supplier == null)
            {
                throw new EntityNotFoundException($"Supplier with id {supplierId} not found");
            }

            return supplier;
        }
    }
}
