using LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.InvoiceGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Concrete
{
    public class GetReleasedHoldsUseCase : IGetReleasedHoldsUseCase
    {
        private readonly IInvoiceGateway _invoiceGateway;

        public GetReleasedHoldsUseCase(IInvoiceGateway invoiceGateway)
        {
            _invoiceGateway = invoiceGateway;
        }

        public async Task<IEnumerable<InvoiceResponse>> Execute(DateTimeOffset? fromDate = null, DateTimeOffset? toDate = null)
        {
            var res = await _invoiceGateway
                .GetInvoiceListUsingInvoiceStatus((int) InvoiceStatusEnum.Released, fromDate, toDate)
                .ConfigureAwait(false);
            return res?.ToResponse();
        }
    }
}
