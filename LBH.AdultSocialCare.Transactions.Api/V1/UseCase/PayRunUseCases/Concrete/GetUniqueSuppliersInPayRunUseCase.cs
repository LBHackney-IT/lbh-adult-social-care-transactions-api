using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.SupplierBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.PayRunGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.RequestExtensions;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Interfaces;
using System;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Concrete
{
    public class GetUniqueSuppliersInPayRunUseCase : IGetUniqueSuppliersInPayRunUseCase
    {
        private readonly IPayRunGateway _payRunGateway;

        public GetUniqueSuppliersInPayRunUseCase(IPayRunGateway payRunGateway)
        {
            _payRunGateway = payRunGateway;
        }

        public async Task<PagedSupplierMinimalListResponse> Execute(Guid payRunId, SupplierListParameters parameters)
        {
            var res = await _payRunGateway.GetUniqueSuppliersInPayRun(payRunId, parameters).ConfigureAwait(false);
            return new PagedSupplierMinimalListResponse { PagingMetaData = res.PagingMetaData, Data = res.ToResponse() };
        }
    }
}
