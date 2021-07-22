using System;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.InvoiceGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.InvoiceUseCases.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums;
using LBH.AdultSocialCare.Transactions.Api.V1.Exceptions.CustomExceptions;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.PayRunGateways;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.InvoiceUseCases.Concrete
{
    public class InvoiceStatusUseCase : IInvoiceStatusUseCase
    {
        private readonly IInvoiceGateway _invoiceGateway;
        private readonly IPayRunGateway _payRunGateway;

        public InvoiceStatusUseCase(IInvoiceGateway invoiceGateway, IPayRunGateway payRunGateway)
        {
            _invoiceGateway = invoiceGateway;
            _payRunGateway = payRunGateway;
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

        public async Task<bool> AcceptInvoiceUseCase(Guid payRunId, Guid invoiceId)
        {
            // Check pay run exists and has correct status
            var payRun = await _payRunGateway.CheckPayRunExists(payRunId).ConfigureAwait(false);

            switch (payRun.PayRunStatusId)
            {
                case (int) PayRunStatusesEnum.SubmittedForApproval:
                    throw new ApiException($"Pay run with id {payRunId} has already been submitted for approval");
                case (int) PayRunStatusesEnum.Approved:
                    throw new ApiException($"Pay run with id {payRunId} has already been approved. Invoice status cannot be changed");
            }

            // Get invoice and check has correct status
            var invoice = await _payRunGateway.GetSingleInvoiceInPayRun(payRunId, invoiceId).ConfigureAwait(false);

            switch (invoice.InvoiceStatusId)
            {
                case (int) InvoiceStatusEnum.Held:
                    throw new ApiException($"Invoice with id {payRunId} has already been held. It must be released first");
                case (int) InvoiceStatusEnum.Released:
                    throw new ApiException($"Released invoice with id {payRunId} will be added to the next pay run");
                default:
                    {
                        // Run change status
                        var res = await _invoiceGateway.ChangeInvoiceStatus(invoiceId, (int) InvoiceStatusEnum.Accepted)
                            .ConfigureAwait(false);

                        return res;
                    }
            }
        }
    }
}
