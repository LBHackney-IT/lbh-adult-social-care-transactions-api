using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.PayRunGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Concrete
{
    public class GetUniqueInvoiceItemPaymentStatusInPayRunUseCase : IGetUniqueInvoiceItemPaymentStatusInPayRunUseCase
    {
        private readonly IPayRunGateway _payRunGateway;

        public GetUniqueInvoiceItemPaymentStatusInPayRunUseCase(IPayRunGateway payRunGateway)
        {
            _payRunGateway = payRunGateway;
        }

        public async Task<IEnumerable<InvoiceStatusResponse>> Execute(Guid payRunId)
        {
            var res = await _payRunGateway.GetUniqueInvoiceItemPaymentStatusesInPayRun(payRunId).ConfigureAwait(false);
            return res?.ToResponse();
        }
    }
}
