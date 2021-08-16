using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.PayRunBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.InvoiceGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.PayRunGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.RequestExtensions;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Interfaces;
using System;
using System.Threading.Tasks;
using Common.CustomExceptions;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Concrete
{
    public class GetSinglePayRunDetailsUseCase : IGetSinglePayRunDetailsUseCase
    {
        private readonly IPayRunGateway _payRunGateway;
        private readonly IInvoiceGateway _invoiceGateway;

        public GetSinglePayRunDetailsUseCase(IPayRunGateway payRunGateway, IInvoiceGateway invoiceGateway)
        {
            _payRunGateway = payRunGateway;
            _invoiceGateway = invoiceGateway;
        }

        public async Task<PayRunDetailsResponse> Execute(Guid payRunId, InvoiceListParameters parameters)
        {
            var payRunDetails = await _payRunGateway.GetPayRunFlat(payRunId).ConfigureAwait(false);
            if (payRunDetails == null)
            {
                throw new EntityNotFoundException($"Pay run with id {payRunId} not found");
            }

            var invoices = await _invoiceGateway.GetInvoicesInPayRun(payRunId, parameters).ConfigureAwait(false);

            return new PayRunDetailsResponse
            {
                PayRunDetails = payRunDetails.ToResponse(),
                Invoices = new PagedInvoiceResponse
                {
                    PagingMetaData = invoices.PagingMetaData,
                    Invoices = invoices.ToResponse()
                }
            };
        }
    }
}
