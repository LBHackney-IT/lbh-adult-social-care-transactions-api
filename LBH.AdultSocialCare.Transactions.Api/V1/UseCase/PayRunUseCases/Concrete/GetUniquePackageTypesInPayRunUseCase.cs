using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.PackageTypeBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.PayRunGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Concrete
{
    public class GetUniquePackageTypesInPayRunUseCase : IGetUniquePackageTypesInPayRunUseCase
    {
        private readonly IPayRunGateway _payRunGateway;

        public GetUniquePackageTypesInPayRunUseCase(IPayRunGateway payRunGateway)
        {
            _payRunGateway = payRunGateway;
        }

        public async Task<IEnumerable<PackageTypeResponse>> Execute(Guid payRunId)
        {
            var res = await _payRunGateway.GetUniquePackageTypesInPayRun(payRunId).ConfigureAwait(false);
            return res?.ToResponse();
        }
    }
}
