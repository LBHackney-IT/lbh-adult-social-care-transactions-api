using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.PayRunBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.InvoiceGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.PayRunGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Concrete
{
    public class PayRunUseCase : IPayRunUseCase
    {
        private readonly IPayRunGateway _payRunGateway;
        private readonly IInvoiceGateway _invoiceGateway;

        public PayRunUseCase(IPayRunGateway payRunGateway, IInvoiceGateway invoiceGateway)
        {
            _payRunGateway = payRunGateway;
            _invoiceGateway = invoiceGateway;
        }

        public async Task<PayRunInsightsResponse> GetSinglePayRunInsightsUseCase(Guid payRunId)
        {
            var res = await _payRunGateway.GetPayRunInsights(payRunId).ConfigureAwait(false);
            return res.ToResponse();
        }

        public async Task<IEnumerable<HeldInvoiceResponse>> GetHeldInvoicePaymentsUseCase()
        {
            var res = await _invoiceGateway.GetHeldInvoicePayments().ConfigureAwait(false);
            return res.ToResponse();
        }

        public async Task<bool> ApprovePayRunForPaymentUseCase(Guid payRunId)
        {
            return await _payRunGateway.ApprovePayRunForPayment(payRunId).ConfigureAwait(false);
        }
    }
}
