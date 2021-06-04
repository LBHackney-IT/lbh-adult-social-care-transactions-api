using LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.PayRunDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Exceptions.CustomExceptions;
using LBH.AdultSocialCare.Transactions.Api.V1.Extensions;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.InvoiceGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.PayRunGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Interfaces;
using System;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Concrete
{
    public class PayRunUseCase : IPayRunUseCase
    {
        private readonly IInvoiceGateway _invoiceGateway;
        private readonly IPayRunGateway _payRunGateway;

        public PayRunUseCase(IInvoiceGateway invoiceGateway, IPayRunGateway payRunGateway)
        {
            _invoiceGateway = invoiceGateway;
            _payRunGateway = payRunGateway;
        }

        public async Task<Guid> CreateResidentialRecurringPayRunUseCase()
        {
            const int payRunTypeId = (int) PayRunTypeEnum.ResidentialRecurring;
            // Get date of last pay run. If none make it today-28 days
            var dateFrom = await _payRunGateway.GetDateOfLastPayRun(payRunTypeId).ConfigureAwait(false);
            var dateTo = dateFrom.AddDays(28);
            // Get invoice items from date of last pay run with status new - fresh from supplier returns, never in a pay run before.
            var invoiceItems = await _invoiceGateway
                .GetInvoiceItemsUsingItemPaymentStatus((int) InvoiceItemPaymentStatusEnum.New, dateFrom, dateTo)
                .ConfigureAwait(false);
            // Add range to a new pay run. Date end is date today
            var newPayRunDomain = new PayRunForCreationDomain
            {
                PayRunTypeId = payRunTypeId,
                PayRunSubTypeId = null,
                DateFrom = dateFrom,
                DateTo = dateTo,
                CreatorId = new Guid("1f825b5f-5c65-41fb-8d9e-9d36d78fd6d8"),
                UpdaterId = null,
                InvoiceItems = invoiceItems
            };

            return await _payRunGateway.CreateNewPayRun(newPayRunDomain.ToDb()).ConfigureAwait(false);
        }

        public async Task<Guid> CreateDirectPaymentsPayRunUseCase()
        {
            const int payRunTypeId = (int) PayRunTypeEnum.DirectPayments;
            // Get date of last pay run. If none make it today-28 days
            var dateFrom = await _payRunGateway.GetDateOfLastPayRun(payRunTypeId).ConfigureAwait(false);
            var dateTo = dateFrom.AddDays(28);
            // Get invoice items from date of last pay run with status new - fresh from supplier returns, never in a pay run before.
            var invoiceItems = await _invoiceGateway
                .GetInvoiceItemsUsingItemPaymentStatus((int) InvoiceItemPaymentStatusEnum.New, dateFrom, dateTo)
                .ConfigureAwait(false);
            // Add range to a new pay run. Date end is date today
            var newPayRunDomain = new PayRunForCreationDomain
            {
                PayRunTypeId = payRunTypeId,
                PayRunSubTypeId = null,
                DateFrom = dateFrom,
                DateTo = dateTo,
                CreatorId = new Guid("1f825b5f-5c65-41fb-8d9e-9d36d78fd6d8"),
                UpdaterId = null,
                InvoiceItems = invoiceItems
            };

            return await _payRunGateway.CreateNewPayRun(newPayRunDomain.ToDb()).ConfigureAwait(false);
        }

        public async Task<Guid> CreateHomeCarePayRunUseCase()
        {
            const int payRunTypeId = (int) PayRunTypeEnum.HomeCare;
            // Get date of last pay run. If none make it today-28 days
            var dateFrom = await _payRunGateway.GetDateOfLastPayRun(payRunTypeId).ConfigureAwait(false);
            var dateTo = dateFrom.AddDays(28);
            // Get invoice items from date of last pay run with status new - fresh from supplier returns, never in a pay run before.
            var invoiceItems = await _invoiceGateway
                .GetInvoiceItemsUsingItemPaymentStatus((int) InvoiceItemPaymentStatusEnum.New, dateFrom, dateTo)
                .ConfigureAwait(false);
            // Add range to a new pay run. Date end is date today
            var newPayRunDomain = new PayRunForCreationDomain
            {
                PayRunTypeId = payRunTypeId,
                PayRunSubTypeId = null,
                DateFrom = dateFrom,
                DateTo = dateTo,
                CreatorId = new Guid("1f825b5f-5c65-41fb-8d9e-9d36d78fd6d8"),
                UpdaterId = null,
                InvoiceItems = invoiceItems
            };

            return await _payRunGateway.CreateNewPayRun(newPayRunDomain.ToDb()).ConfigureAwait(false);
        }

        public async Task<Guid> CreateResidentialReleaseHoldsPayRunUseCase()
        {
            const int payRunTypeId = (int) PayRunTypeEnum.ResidentialRecurring;
            const int payRunSubTypeId = (int) PayRunSubTypeEnum.ResidentialReleaseHolds;
            // Get date of last pay run. If none make it today-28 days
            var dateFrom = await _payRunGateway.GetDateOfLastPayRun(payRunTypeId).ConfigureAwait(false);
            var dateTo = dateFrom.AddDays(28);
            // Get invoice items from date of last pay run with status new - fresh from supplier returns, never in a pay run before.
            var invoiceItems = await _invoiceGateway
                .GetInvoiceItemsUsingItemPaymentStatus((int) InvoiceItemPaymentStatusEnum.Released, dateFrom, dateTo)
                .ConfigureAwait(false);
            // Add range to a new pay run. Date end is date today
            var newPayRunDomain = new PayRunForCreationDomain
            {
                PayRunTypeId = payRunTypeId,
                PayRunSubTypeId = payRunSubTypeId,
                DateFrom = dateFrom,
                DateTo = dateTo,
                CreatorId = new Guid("1f825b5f-5c65-41fb-8d9e-9d36d78fd6d8"),
                UpdaterId = null,
                InvoiceItems = invoiceItems
            };

            return await _payRunGateway.CreateNewPayRun(newPayRunDomain.ToDb()).ConfigureAwait(false);
        }

        public async Task<Guid> CreateDirectPaymentsReleaseHoldsPayRunUseCase()
        {
            const int payRunTypeId = (int) PayRunTypeEnum.ResidentialRecurring;
            const int payRunSubTypeId = (int) PayRunSubTypeEnum.DirectPaymentsReleaseHolds;
            // Get date of last pay run. If none make it today-28 days
            var dateFrom = await _payRunGateway.GetDateOfLastPayRun(payRunTypeId).ConfigureAwait(false);
            var dateTo = dateFrom.AddDays(28);
            // Get invoice items from date of last pay run with status new - fresh from supplier returns, never in a pay run before.
            var invoiceItems = await _invoiceGateway
                .GetInvoiceItemsUsingItemPaymentStatus((int) InvoiceItemPaymentStatusEnum.Released, dateFrom, dateTo)
                .ConfigureAwait(false);
            // Add range to a new pay run. Date end is date today
            var newPayRunDomain = new PayRunForCreationDomain
            {
                PayRunTypeId = payRunTypeId,
                PayRunSubTypeId = payRunSubTypeId,
                DateFrom = dateFrom,
                DateTo = dateTo,
                CreatorId = new Guid("1f825b5f-5c65-41fb-8d9e-9d36d78fd6d8"),
                UpdaterId = null,
                InvoiceItems = invoiceItems
            };

            return await _payRunGateway.CreateNewPayRun(newPayRunDomain.ToDb()).ConfigureAwait(false);
        }

        public async Task<Guid> CreateNewPayRunUseCase(string payRunType)
        {
            if (!PayRunTypeEnum.ResidentialRecurring.EnumIsDefined(payRunType) && !PayRunSubTypeEnum.DirectPaymentsReleaseHolds.EnumIsDefined(payRunType))
            {
                throw new EntityNotFoundException("The pay run type is not valid. Please check and try again");
            }

            return payRunType switch
            {
                nameof(PayRunTypeEnum.ResidentialRecurring) => await CreateResidentialRecurringPayRunUseCase()
                    .ConfigureAwait(false),
                nameof(PayRunTypeEnum.DirectPayments) => await CreateDirectPaymentsPayRunUseCase()
                    .ConfigureAwait(false),
                nameof(PayRunTypeEnum.HomeCare) => await CreateHomeCarePayRunUseCase()
                    .ConfigureAwait(false),
                nameof(PayRunSubTypeEnum.ResidentialReleaseHolds) => await CreateResidentialReleaseHoldsPayRunUseCase()
                    .ConfigureAwait(false),
                nameof(PayRunSubTypeEnum.DirectPaymentsReleaseHolds) => await
                    CreateDirectPaymentsReleaseHoldsPayRunUseCase()
                        .ConfigureAwait(false),
                _ => throw new EntityNotFoundException("The pay run type is not valid. Please check and try again")
            };
        }
    }
}
