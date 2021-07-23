using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.SupplierReturnDomains;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.SupplierReturnUseCases.Interfaces
{
    public interface ICreateDisputeChatUseCase
    {
         Task CreateDisputeChat(SupplierReturnDisputeConversationCreationDomain supplierReturnDisputeConversationCreationDomain);
    }
}
