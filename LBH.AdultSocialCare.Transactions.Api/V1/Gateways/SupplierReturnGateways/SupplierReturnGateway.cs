using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.SupplierReturnDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Exceptions.CustomExceptions;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.SupplierReturns;
using Microsoft.EntityFrameworkCore;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Gateways.SupplierReturnGateways
{
    public class SupplierReturnGateway : ISupplierReturnGateway
    {
        private readonly DatabaseContext _dbContext;

        public SupplierReturnGateway(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SupplierReturnDomain> GetSupplierReturn(Guid supplierReturnId)
        {
            var supplierReturn = await _dbContext.SupplierReturns
                .Where(s => s.SupplierReturnId.Equals(supplierReturnId))
                .AsNoTracking()
                .Include(s => s.SupplierReturnItems)
                .SingleOrDefaultAsync().ConfigureAwait(false);

            if (supplierReturn == null)
            {
                throw new EntityNotFoundException($"Unable to locate supplier return {supplierReturnId.ToString()}");
            }

            return supplierReturn.ToDomain();
        }

        public async Task CreateDisputeItemChat(SupplierReturnItemDisputeConversation supplierReturnItemDisputeConversation)
        {
            await _dbContext.SupplierReturnItemDisputeConversations.AddAsync(supplierReturnItemDisputeConversation).ConfigureAwait(false);
            try
            {
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw new DbSaveFailedException("Could not save supplier dispute item chat to database");
            }
        }

        public async Task MarkDisputeItemChat(Guid packageId, Guid supplierReturnItemId, Guid disputeItemConversationId, bool messageRead)
        {
            var supplierReturnItemDisputeConversation = await _dbContext.SupplierReturnItemDisputeConversations
                .Where(s => s.PackageId.Equals(packageId) &&
                       s.SupplierReturnItemId.Equals(supplierReturnItemId) &&
                       s.DisputeItemConversationId.Equals(disputeItemConversationId))
                .SingleOrDefaultAsync().ConfigureAwait(false);

            if (supplierReturnItemDisputeConversation == null)
                throw new EntityNotFoundException($"Unable to locate chat message");
            supplierReturnItemDisputeConversation.MessageRead = messageRead;
            try
            {
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw new DbSaveFailedException($"Update chat message failed");
            }
        }

        public async Task<IEnumerable<SupplierReturnItemDisputeConversationDomain>> GetDisputeItemChat(Guid packageId, Guid supplierReturnItemId)
        {
            var supplierReturnItemDisputeConversations = await _dbContext.SupplierReturnItemDisputeConversations
                .Where(s => s.PackageId.Equals(packageId) &&
                       s.SupplierReturnItemId.Equals(supplierReturnItemId))
                .AsNoTracking()
                .ToListAsync().ConfigureAwait(false);
            return supplierReturnItemDisputeConversations?.ToDomain();
        }

        public async Task AcceptAllSupplierReturnPackageItems(Guid supplierReturnId)
        {
            var supplierReturn = await _dbContext.SupplierReturns
                .Where(s => s.SupplierReturnId.Equals(supplierReturnId))
                .SingleOrDefaultAsync().ConfigureAwait(false);

            if (supplierReturn == null)
                throw new EntityNotFoundException($"Unable to locate supplier return");

            var supplierReturnItems = await _dbContext.SupplierReturnItems
                .Where(s => s.SupplierReturnId.Equals(supplierReturnId))
                .ToListAsync().ConfigureAwait(false);

            foreach (var supplierReturnItem in supplierReturnItems)
            {
                supplierReturnItem.SupplierReturnItemStatusId = SupplierReturnItemStatusType.AcceptedId;
            }

            try
            {
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw new DbSaveFailedException($"Update supplier return item status failed");
            }
        }

        public async Task DisputeAllSupplierReturnPackageItems(Guid supplierReturnId)
        {
            var supplierReturn = await _dbContext.SupplierReturns
                .Where(s => s.SupplierReturnId.Equals(supplierReturnId))
                .SingleOrDefaultAsync().ConfigureAwait(false);

            if (supplierReturn == null)
                throw new EntityNotFoundException($"Unable to locate supplier return");

            var supplierReturnItems = await _dbContext.SupplierReturnItems
                .Where(s => s.SupplierReturnId.Equals(supplierReturnId))
                .ToListAsync().ConfigureAwait(false);

            foreach (var supplierReturnItem in supplierReturnItems)
                supplierReturnItem.SupplierReturnItemStatusId = SupplierReturnItemStatusType.DisputedId;

            try
            {
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw new DbSaveFailedException($"Update supplier return item status failed");
            }
        }

        public async Task ChangeSupplierReturnPackageValues(Guid supplierReturnId, Guid supplierReturnItemId, float hoursDelivered,
            float actualVisits, string comment)
        {
            var supplierReturnItem = await _dbContext.SupplierReturnItems
                .Where(s => s.SupplierReturnId.Equals(supplierReturnId) &&
                       s.ItemId.Equals(supplierReturnItemId))
                .SingleOrDefaultAsync().ConfigureAwait(false);

            if (supplierReturnItem == null)
                throw new EntityNotFoundException($"Unable to locate supplier return item");

            supplierReturnItem.HoursDelivered = hoursDelivered;
            supplierReturnItem.ActualVisits = actualVisits;
            supplierReturnItem.Comment = comment;
            supplierReturnItem.SupplierReturnItemStatusId = SupplierReturnItemStatusType.AcceptedId;

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (supplierReturnItem.PackageHours != supplierReturnItem.HoursDelivered ||
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                supplierReturnItem.PackageVisits != supplierReturnItem.ActualVisits)
                supplierReturnItem.SupplierReturnItemStatusId = SupplierReturnItemStatusType.DisputedId;

            try
            {
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw new DbSaveFailedException($"Update supplier return item failed");
            }
        }

        public async Task<SupplierReturnInsightsDomain> GetSingleSupplierReturnInsights(Guid suppliersReturnsId)
        {
            var supplierCount = await _dbContext.SupplierReturns
                .Where(sr => sr.SuppliersReturnsId.Equals(suppliersReturnsId))
                .Select(sr => new { sr.SupplierId }).Distinct()
                .CountAsync()
                .ConfigureAwait(false);

            var packageCount = await _dbContext.SupplierReturns
                .Where(sr => sr.SuppliersReturnsId.Equals(suppliersReturnsId))
                .Select(sr => new {sr.PackageId})
                .CountAsync()
                .ConfigureAwait(false);

            var totalValue = await _dbContext.SupplierReturns
                .Where(sr => sr.SuppliersReturnsId.Equals(suppliersReturnsId))
                .Select(sr => sr.TotalAmount).SumAsync()
                .ConfigureAwait(false);

            var supplierReturnIds = await _dbContext.SupplierReturns
                .Where(sr => sr.SuppliersReturnsId.Equals(suppliersReturnsId))
                .Select(sr => sr.SupplierReturnId )
                .ToListAsync()
                .ConfigureAwait(false);

            var returnedCount = await _dbContext.SupplierReturnItems
                .Where(sr => supplierReturnIds.Contains(sr.SupplierReturnId))
                .CountAsync()
                .ConfigureAwait(false);

            var inDisputeCount = await _dbContext.SupplierReturnItems
                .Where(sr => supplierReturnIds.Contains(sr.SupplierReturnId) &&
                       sr.SupplierReturnItemStatusId == SupplierReturnItemStatusType.DisputedId)
                .CountAsync()
                .ConfigureAwait(false);

            var acceptedCount = await _dbContext.SupplierReturnItems
                .Where(sr => supplierReturnIds.Contains(sr.SupplierReturnId) &&
                             sr.SupplierReturnItemStatusId == SupplierReturnItemStatusType.AcceptedId)
                .CountAsync()
                .ConfigureAwait(false);

            //TODO pay run paid total ?
            var supplierReturnInsights = new SupplierReturnInsightsDomain()
            {
                SuppliersReturnsId = suppliersReturnsId,
                Suppliers = supplierCount,
                TotalPackages = packageCount,
                TotalValue = totalValue,
                Accepted = acceptedCount,
                Returned = returnedCount,
                InDispute = inDisputeCount,
                TotalPaid = 0 
            };
            
            return supplierReturnInsights;
        }
    }
}
