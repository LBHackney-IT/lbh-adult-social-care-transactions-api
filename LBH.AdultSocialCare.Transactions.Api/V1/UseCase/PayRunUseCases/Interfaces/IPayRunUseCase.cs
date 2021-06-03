using System;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Interfaces
{
    public interface IPayRunUseCase
    {
        Task<Guid> CreateResidentialRecurringPayRunUseCase();
        Task<Guid> CreateDirectPaymentsPayRunUseCase();
        Task<Guid> CreateHomeCarePayRunUseCase();
        Task<Guid> CreateResidentialReleaseHoldsPayRunUseCase();
        Task<Guid> CreateDirectPaymentsReleaseHoldsPayRunUseCase();
        Task<Guid> CreateNewPayRunUseCase(string payRunType);
    }
}
