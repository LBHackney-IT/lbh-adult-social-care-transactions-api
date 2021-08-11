using LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums;
using LBH.AdultSocialCare.Transactions.Api.V1.Exceptions.CustomExceptions;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.PayRunGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Concrete
{
    public class ChangePayRunStatusUseCase : IChangePayRunStatusUseCase
    {
        private readonly IPayRunGateway _payRunGateway;

        public ChangePayRunStatusUseCase(IPayRunGateway payRunGateway)
        {
            _payRunGateway = payRunGateway;
        }

        public async Task<bool> SubmitPayRunForApproval(Guid payRunId)
        {
            // Pay run must be in draft to be submitted for approval
            var payRun = await _payRunGateway.CheckPayRunExists(payRunId).ConfigureAwait(false);

            if (payRun.PayRunStatusId != (int) PayRunStatusesEnum.Draft)
            {
                throw new ApiException(
                    $"Pay run with id {payRunId} is not in draft", StatusCodes.Status422UnprocessableEntity);
            }

            var validInvoiceIds = new List<int>() { (int) InvoiceStatusEnum.Held, (int) InvoiceStatusEnum.Accepted, (int) InvoiceStatusEnum.Rejected };

            var validInvoiceStatuses = await _payRunGateway.CheckAllInvoicesInPayRunInStatusList(payRunId, validInvoiceIds).ConfigureAwait(false);

            if (!validInvoiceStatuses)
            {
                throw new ApiException(
                    $"All invoices in pay run must be accepted, held or rejected before submitting for approval. Please check and try again");
            }

            return await _payRunGateway.ChangePayRunStatus(payRunId, (int) PayRunStatusesEnum.SubmittedForApproval)
                .ConfigureAwait(false);
        }

        public async Task<bool> KickBackPayRunToDraft(Guid payRunId)
        {
            // Check if pay run is in submitted for approval stage. That is when it can be kicked back to draft
            var payRun = await _payRunGateway.CheckPayRunExists(payRunId).ConfigureAwait(false);

            return payRun.PayRunStatusId switch
            {
                (int) PayRunStatusesEnum.Draft => throw new ApiException(
                    $"Pay run with id {payRunId} is already in draft", StatusCodes.Status422UnprocessableEntity),
                (int) PayRunStatusesEnum.Approved => throw new ApiException(
                    $"Pay run with id {payRunId} is already approved and cannot be send back to draft",
                    StatusCodes.Status422UnprocessableEntity),
                _ => await _payRunGateway.ChangePayRunStatus(payRunId, (int) PayRunStatusesEnum.Draft)
                    .ConfigureAwait(false)
            };
        }
    }
}
