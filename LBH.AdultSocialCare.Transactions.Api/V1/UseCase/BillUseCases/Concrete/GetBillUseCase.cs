using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.BillBoundary.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.BillGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.RequestExtensions;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.BillUseCases.Interfaces;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.BillUseCases.Concrete
{
    public class GetBillUseCase : IGetBillUseCase
    {
        private readonly IBillGateway _billGateway;

        public GetBillUseCase(IBillGateway billGateway)
        {
            _billGateway = billGateway;
        }

        public async Task<PagedBillSummaryResponse> GetBill(BillSummaryListParameters parameters)
        {
            var result = await _billGateway.GetBill(parameters).ConfigureAwait(false);
            return new PagedBillSummaryResponse
            {
                PagingMetaData = result.PagingMetaData,
                Data = result.ToResponse()
            };
        }
    }
}
