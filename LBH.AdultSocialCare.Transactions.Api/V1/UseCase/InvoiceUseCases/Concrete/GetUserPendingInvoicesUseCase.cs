using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundary.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.InvoiceGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.InvoiceUseCases.Interfaces;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.InvoiceUseCases.Concrete
{
    public class GetUserPendingInvoicesUseCase : IGetUserPendingInvoicesUseCase
    {
        private readonly IInvoiceGateway _invoiceGateway;

        public GetUserPendingInvoicesUseCase(IInvoiceGateway invoiceGateway)
        {
            _invoiceGateway = invoiceGateway;
        }

        public async Task<IEnumerable<PendingInvoicesResponse>> GetUserPendingInvoices(Guid serviceUserId)
        {
            var result = await _invoiceGateway.GetUserPendingInvoices(serviceUserId).ConfigureAwait(false);
            return result.ToResponse();
        }
    }
}
