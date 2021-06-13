using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.BillBoundary.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.BillGateways;
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

        public async Task<IEnumerable<BillResponse>> GetBill(Guid packageId, long supplierId, int billPaymentStatusId, DateTimeOffset? fromDate = null,
            DateTimeOffset? toDate = null)
        {
            var result = await _billGateway.GetBill(packageId, supplierId, billPaymentStatusId, fromDate, toDate).ConfigureAwait(false);
            return result?.ToResponse();
        }
    }
}
