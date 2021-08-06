using LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.PayRunBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.PayRunDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Exceptions.CustomExceptions;
using LBH.AdultSocialCare.Transactions.Api.V1.Extensions;
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

        public async Task<bool> DeleteDraftPayRunUseCase(Guid payRunId)
        {
            return await _payRunGateway.DeleteDraftPayRun(payRunId).ConfigureAwait(false);
        }

        public async Task<PayRunDateSummaryResponse> GetDateOfLastPayRunUseCase(string payRunType)
        {
            if (!PayRunTypeEnum.ResidentialRecurring.EnumIsDefined(payRunType) && !PayRunSubTypeEnum.DirectPaymentsReleaseHolds.EnumIsDefined(payRunType))
            {
                throw new EntityNotFoundException("The pay run type is not valid. Please check and try again");
            }

            PayRunDateSummaryDomain res;
            switch (payRunType)
            {
                case nameof(PayRunTypeEnum.ResidentialRecurring):
                    res = await _payRunGateway.GetDateOfLastPayRunSummary((int) PayRunTypeEnum.ResidentialRecurring, null)
                        .ConfigureAwait(false);
                    return res?.ToResponse();

                case nameof(PayRunTypeEnum.DirectPayments):
                    res = await _payRunGateway.GetDateOfLastPayRunSummary((int) PayRunTypeEnum.DirectPayments, null)
                        .ConfigureAwait(false);
                    return res?.ToResponse();

                case nameof(PayRunTypeEnum.HomeCare):
                    res = await _payRunGateway.GetDateOfLastPayRunSummary((int) PayRunTypeEnum.HomeCare, null)
                        .ConfigureAwait(false);
                    return res?.ToResponse();

                case nameof(PayRunSubTypeEnum.ResidentialReleaseHolds):
                    res = await _payRunGateway.GetDateOfLastPayRunSummary((int) PayRunTypeEnum.ResidentialRecurring, (int) PayRunSubTypeEnum.ResidentialReleaseHolds)
                        .ConfigureAwait(false);
                    return res?.ToResponse();

                case nameof(PayRunSubTypeEnum.DirectPaymentsReleaseHolds):
                    res = await _payRunGateway.GetDateOfLastPayRunSummary((int) PayRunTypeEnum.DirectPayments, (int) PayRunSubTypeEnum.DirectPaymentsReleaseHolds)
                        .ConfigureAwait(false);
                    return res?.ToResponse();

                default:
                    return null;
            }
        }

        public async Task<IEnumerable<PayRunTypeResponse>> GetAllPayRunTypesUseCase()
        {
            var res = await _payRunGateway.GetAllPayRunTypes().ConfigureAwait(false);
            return res.ToResponse();
        }

        public async Task<IEnumerable<PayRunSubTypeResponse>> GetAllPayRunSubTypesUseCase()
        {
            var res = await _payRunGateway.GetAllPayRunSubTypes().ConfigureAwait(false);
            return res.ToResponse();
        }

        public async Task<IEnumerable<PayRunStatusResponse>> GetAllUniquePayRunStatusesUseCase()
        {
            var res = await _payRunGateway.GetAllUniquePayRunStatuses().ConfigureAwait(false);
            return res.ToResponse();
        }
    }
}
