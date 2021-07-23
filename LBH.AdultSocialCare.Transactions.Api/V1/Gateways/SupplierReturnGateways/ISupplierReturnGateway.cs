using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.SupplierReturnDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.SupplierReturns;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Gateways.SupplierReturnGateways
{
    public interface ISupplierReturnGateway
    {
        Task<SupplierReturnDomain> GetSupplierReturn(Guid supplierReturnId);
        Task CreateDisputeItemChat(SupplierReturnItemDisputeConversation supplierReturnItemDisputeConversation);
        Task MarkDisputeItemChat(Guid packageId, Guid supplierReturnItemId, Guid disputeConversationId, bool messageRead);
        Task<IEnumerable<SupplierReturnItemDisputeConversationDomain>> GetDisputeItemChat(Guid packageId, Guid supplierReturnItemId);
        Task AcceptAllSupplierReturnPackageItems(Guid supplierReturnId);
        Task DisputeAllSupplierReturnPackageItems(Guid supplierReturnId);
        Task ChangeSupplierReturnPackageValues(Guid supplierReturnId, Guid supplierReturnItemId, float hoursDelivered, float actualVisits, string comment);
        Task<SupplierReturnInsightsDomain> GetSingleSupplierReturnInsights(Guid suppliersReturnsId);
    }
}
