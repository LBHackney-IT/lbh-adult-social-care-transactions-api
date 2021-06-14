using System;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Interfaces
{
    public interface IChangePayRunStatusUseCase
    {
        Task<bool> ApprovePayRun(Guid payRunId);

        Task<bool> SubmitPayRunForApproval(Guid payRunId);

        Task<bool> KickBackPayRunToDraft(Guid payRunId);
    }
}
