using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.PackageTypeBoundaries.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Interfaces
{
    public interface IGetUniquePackageTypesInPayRunUseCase
    {
        Task<IEnumerable<PackageTypeResponse>> Execute(Guid payRunId);
    }
}
