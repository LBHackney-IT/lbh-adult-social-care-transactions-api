using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.PayRunBoundaries.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Interfaces
{
    public interface IGetReleasedHoldsCountUseCase
    {
        Task<IEnumerable<ReleasedHoldsByTypeResponse>> Execute(DateTimeOffset? fromDate = null, DateTimeOffset? toDate = null);
    }
}
