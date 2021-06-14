using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.InvoiceGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.InvoiceUseCases.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.InvoiceUseCases.Concrete
{
    public class GetInvoiceItemPaymentStatusesUseCase : IGetInvoiceItemPaymentStatusesUseCase
    {
        private readonly IInvoiceGateway _invoiceGateway;

        public GetInvoiceItemPaymentStatusesUseCase(IInvoiceGateway invoiceGateway)
        {
            _invoiceGateway = invoiceGateway;
        }

        public async Task<IEnumerable<InvoiceItemPaymentStatusResponse>> Execute()
        {
            var res = await _invoiceGateway.GetInvoiceItemPaymentStatuses().ConfigureAwait(false);
            return res.ToResponse();
        }
    }
}
