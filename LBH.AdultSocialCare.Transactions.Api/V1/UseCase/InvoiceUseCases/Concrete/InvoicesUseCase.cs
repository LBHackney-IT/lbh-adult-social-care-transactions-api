using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.InvoicesDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.InvoiceGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.InvoiceUseCases.Interfaces;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.InvoiceUseCases.Concrete
{
    public class InvoicesUseCase : IInvoicesUseCase
    {
        private readonly IInvoiceGateway _invoiceGateway;

        public InvoicesUseCase(IInvoiceGateway invoiceGateway)
        {
            _invoiceGateway = invoiceGateway;
        }

        public async Task<DisputedInvoiceFlatResponse> HoldInvoicePaymentUseCase(DisputedInvoiceForCreationDomain disputedInvoiceForCreationDomain)
        {
            var res = await _invoiceGateway.CreateDisputedInvoice(disputedInvoiceForCreationDomain.ToDb()).ConfigureAwait(false);
            return res.ToResponse();
        }
    }
}
