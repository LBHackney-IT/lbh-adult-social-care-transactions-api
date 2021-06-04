using LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.PayRunDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Exceptions.CustomExceptions;
using LBH.AdultSocialCare.Transactions.Api.V1.Extensions;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.InvoiceGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.PayRunGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Interfaces;
using System;
using System.Linq;
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

            // If date to is greater than today, make dateTo today's date
            if (dateTo > DateTimeOffset.Now)
            {
                dateTo = DateTimeOffset.Now;
            }

            // Get invoice items from date of last pay run with status new - fresh from supplier returns, never in a pay run before.
            var invoiceItems = await _invoiceGateway
                .GetInvoiceItemsUsingItemPaymentStatus((int) InvoiceItemPaymentStatusEnum.New, dateFrom, dateTo)
                .ConfigureAwait(false);

            // If no invoice items do not create pay run
            var invoiceItemMinimalDomains = invoiceItems.ToList();
            if (!invoiceItemMinimalDomains.Any())
            {
                throw new EntityNotFoundException("No pending invoices to add to pay run");
            }

            // Add range to a new pay run. Date end is date today
            var newPayRunDomain = new PayRunForCreationDomain
            {
                PayRunTypeId = payRunTypeId,
                PayRunSubTypeId = null,
                DateFrom = dateFrom,
                DateTo = dateTo,
                CreatorId = new Guid("1f825b5f-5c65-41fb-8d9e-9d36d78fd6d8"),
                UpdaterId = null,
                InvoiceItems = invoiceItemMinimalDomains
            };

            return await _payRunGateway.CreateNewPayRun(newPayRunDomain.ToDb()).ConfigureAwait(false);
        }

        public async Task<Guid> CreateDirectPaymentsPayRunUseCase()
        {
            const int payRunTypeId = (int) PayRunTypeEnum.DirectPayments;
            // Get date of last pay run. If none make it today-28 days
            var dateFrom = await _payRunGateway.GetDateOfLastPayRun(payRunTypeId).ConfigureAwait(false);
            var dateTo = dateFrom.AddDays(28);

            // If date to is greater than today, make dateTo today's date
            if (dateTo > DateTimeOffset.Now)
            {
                dateTo = DateTimeOffset.Now;
            }

            // Get invoice items from date of last pay run with status new - fresh from supplier returns, never in a pay run before.
            var invoiceItems = await _invoiceGateway
                .GetInvoiceItemsUsingItemPaymentStatus((int) InvoiceItemPaymentStatusEnum.New, dateFrom, dateTo)
                .ConfigureAwait(false);

            // If no invoice items do not create pay run
            var invoiceItemMinimalDomains = invoiceItems.ToList();
            if (!invoiceItemMinimalDomains.Any())
            {
                throw new EntityNotFoundException("No pending invoices to add to pay run");
            }

            // Add range to a new pay run. Date end is date today
            var newPayRunDomain = new PayRunForCreationDomain
            {
                PayRunTypeId = payRunTypeId,
                PayRunSubTypeId = null,
                DateFrom = dateFrom,
                DateTo = dateTo,
                CreatorId = new Guid("1f825b5f-5c65-41fb-8d9e-9d36d78fd6d8"),
                UpdaterId = null,
                InvoiceItems = invoiceItemMinimalDomains
            };

            return await _payRunGateway.CreateNewPayRun(newPayRunDomain.ToDb()).ConfigureAwait(false);
        }

        public async Task<Guid> CreateHomeCarePayRunUseCase()
        {
            const int payRunTypeId = (int) PayRunTypeEnum.HomeCare;
            // Get date of last pay run. If none make it today-28 days
            var dateFrom = await _payRunGateway.GetDateOfLastPayRun(payRunTypeId).ConfigureAwait(false);
            var dateTo = dateFrom.AddDays(28);

            // If date to is greater than today, make dateTo today's date
            if (dateTo > DateTimeOffset.Now)
            {
                dateTo = DateTimeOffset.Now;
            }

            // Get invoice items from date of last pay run with status new - fresh from supplier returns, never in a pay run before.
            var invoiceItems = await _invoiceGateway
                .GetInvoiceItemsUsingItemPaymentStatus((int) InvoiceItemPaymentStatusEnum.New, dateFrom, dateTo)
                .ConfigureAwait(false);

            // If no invoice items do not create pay run
            var invoiceItemMinimalDomains = invoiceItems.ToList();
            if (!invoiceItemMinimalDomains.Any())
            {
                throw new EntityNotFoundException("No pending invoices to add to pay run");
            }

            // Add range to a new pay run. Date end is date today
            var newPayRunDomain = new PayRunForCreationDomain
            {
                PayRunTypeId = payRunTypeId,
                PayRunSubTypeId = null,
                DateFrom = dateFrom,
                DateTo = dateTo,
                CreatorId = new Guid("1f825b5f-5c65-41fb-8d9e-9d36d78fd6d8"),
                UpdaterId = null,
                InvoiceItems = invoiceItemMinimalDomains
            };

            return await _payRunGateway.CreateNewPayRun(newPayRunDomain.ToDb()).ConfigureAwait(false);
        }

        public async Task<Guid> CreateResidentialReleaseHoldsPayRunUseCase()
        {
            const int payRunTypeId = (int) PayRunTypeEnum.ResidentialRecurring;
            const int payRunSubTypeId = (int) PayRunSubTypeEnum.ResidentialReleaseHolds;
            // Get date of last pay run. If none there are no holds so return
            //TODO: Change the date logic here. Account for a possible large list of released invoice items. dateFrom = min date invoice item was created. dateTo = max date invoice item was created.
            var dateFrom = await _invoiceGateway.GetMinDateOfReleasedInvoiceItem((int) InvoiceItemPaymentStatusEnum.Released).ConfigureAwait(false);
            var dateTo = await _invoiceGateway.GetMaxDateOfReleasedInvoiceItem((int) InvoiceItemPaymentStatusEnum.Released).ConfigureAwait(false);
            if (dateFrom == null || dateTo == null)
            {
                throw new EntityNotFoundException("There are no held invoices at this time");
            }
            // Get invoice items from date of last pay run with status new - fresh from supplier returns, never in a pay run before.
            var invoiceItems = await _invoiceGateway
                .GetInvoiceItemsUsingItemPaymentStatus((int) InvoiceItemPaymentStatusEnum.Released, dateFrom, dateTo)
                .ConfigureAwait(false);
            // Add range to a new pay run. Date end is date today
            var newPayRunDomain = new PayRunForCreationDomain
            {
                PayRunTypeId = payRunTypeId,
                PayRunSubTypeId = payRunSubTypeId,
                DateFrom = (DateTimeOffset) dateFrom,
                DateTo = (DateTimeOffset) dateTo,
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
            // Get date of last pay run. If none there are no holds so return
            //TODO: Change the date logic here. Account for a possible large list of released invoice items. dateFrom = min date invoice item was created. dateTo = max date invoice item was created.
            var dateFrom = await _invoiceGateway.GetMinDateOfReleasedInvoiceItem((int) InvoiceItemPaymentStatusEnum.Released).ConfigureAwait(false);
            var dateTo = await _invoiceGateway.GetMaxDateOfReleasedInvoiceItem((int) InvoiceItemPaymentStatusEnum.Released).ConfigureAwait(false);
            if (dateFrom == null || dateTo == null)
            {
                throw new EntityNotFoundException("There are no held invoices at this time");
            }
            // Get invoice items from date of last pay run with status new - fresh from supplier returns, never in a pay run before.
            var invoiceItems = await _invoiceGateway
                .GetInvoiceItemsUsingItemPaymentStatus((int) InvoiceItemPaymentStatusEnum.Released, dateFrom, dateTo)
                .ConfigureAwait(false);
            // Add range to a new pay run. Date end is date today
            var newPayRunDomain = new PayRunForCreationDomain
            {
                PayRunTypeId = payRunTypeId,
                PayRunSubTypeId = payRunSubTypeId,
                DateFrom = (DateTimeOffset) dateFrom,
                DateTo = (DateTimeOffset) dateTo,
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