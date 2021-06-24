using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.InvoiceGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.InvoiceUseCases.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.InvoiceUseCases.Concrete
{
    public class InvoiceStatusUseCase : IInvoiceStatusUseCase
    {
        private readonly IInvoiceGateway _invoiceGateway;

        public InvoiceStatusUseCase(IInvoiceGateway invoiceGateway)
        {
            _invoiceGateway = invoiceGateway;
        }

        public async Task<IEnumerable<InvoiceStatusResponse>> GetAllInvoiceStatusesUseCase()
        {
            var invoiceStatuses = await _invoiceGateway.GetAllInvoiceStatuses().ConfigureAwait(false);
            return invoiceStatuses.ToResponse();
        }

        public async Task<IEnumerable<InvoiceStatusResponse>> GetInvoicePaymentStatusesUseCase()
        {
            var invoiceStatuses = await _invoiceGateway.GetInvoicePaymentStatuses().ConfigureAwait(false);
            return invoiceStatuses.ToResponse();
        }
    }
}
