using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.SupplierReturnBoundary.Response;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.SupplierReturnUseCases.Interfaces
{
    public interface IGetDisputeItemChatUseCase
    {
        Task<IEnumerable<SupplierReturnItemDisputeConversationResponse>> GetDisputeItemChat(Guid packageId, Guid supplierReturnItemId);
    }
}
