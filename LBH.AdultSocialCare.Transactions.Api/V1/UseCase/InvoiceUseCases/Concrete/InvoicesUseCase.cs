using LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.InvoicesDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Exceptions.CustomExceptions;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.InvoiceGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.InvoiceUseCases.Interfaces;
using System;
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

        public async Task<bool> ChangeInvoiceStatusUseCase(Guid invoiceId, int invoiceStatusId)
        {
            if (invoiceStatusId == (int) InvoiceStatusEnum.Held)
            {
                throw new ApiException("Update action not allowed");
            }

            return await _invoiceGateway.ChangeInvoiceStatus(invoiceId, invoiceStatusId).ConfigureAwait(false);
        }

        public async Task<bool> ChangeInvoiceItemPaymentStatusUseCase(Guid payRunId, Guid invoiceItemId, int invoiceItemPaymentStatusId)
        {
            if (invoiceItemPaymentStatusId == (int) InvoiceItemPaymentStatusEnum.Held)
            {
                throw new ApiException("Update action not allowed");
            }

            return await _invoiceGateway
                .ChangeInvoiceItemPaymentStatus(payRunId, invoiceItemId, invoiceItemPaymentStatusId)
                .ConfigureAwait(false);
        }
    }
}
