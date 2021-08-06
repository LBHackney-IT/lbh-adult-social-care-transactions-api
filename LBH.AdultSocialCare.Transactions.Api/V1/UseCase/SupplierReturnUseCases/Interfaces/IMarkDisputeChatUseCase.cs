using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.SupplierReturnUseCases.Interfaces
{
    public interface IMarkDisputeChatUseCase
    {
        Task MarkDisputeChat(Guid packageId, Guid disputeConversationId, bool messageRead);
    }
}
