using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.SupplierReturnUseCases.Interfaces
{
    public interface IMarkDisputeItemChatUseCase
    {
        Task MarkDisputeItemChat(Guid packageId, Guid supplierReturnItemId, Guid disputeConversationId, bool messageRead);
    }
}
