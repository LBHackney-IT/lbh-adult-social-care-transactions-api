using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Interfaces
{
    public interface ICreatePayRunHeldChatUseCase
    {
        Task<bool> CreatePayRunHeldChat(Guid payRunId, Guid packageId, string message);
    }
}
