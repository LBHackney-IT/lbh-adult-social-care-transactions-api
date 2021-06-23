using LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.InvoicesDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.PayRunDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Exceptions.CustomExceptions;
using LBH.AdultSocialCare.Transactions.Api.V1.Extensions;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.InvoiceGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.PayRunGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Concrete
{
    public class CreatePayRunUseCase : ICreatePayRunUseCase
    {
        private readonly IInvoiceGateway _invoiceGateway;
        private readonly IPayRunGateway _payRunGateway;

        public CreatePayRunUseCase(IInvoiceGateway invoiceGateway, IPayRunGateway payRunGateway)
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

            var invoiceDomains = await GetInvoicesForPayRun((int) InvoiceStatusEnum.Draft, dateFrom, dateTo).ConfigureAwait(false);

            // Add range to a new pay run. Date end is date today
            var newPayRunDomain = GeneratePayRunForCreationDomain(dateFrom, dateTo,
                new Guid("1f825b5f-5c65-41fb-8d9e-9d36d78fd6d8"), invoiceDomains, payRunTypeId, null);

            return await _payRunGateway.CreateNewPayRun(newPayRunDomain.ToDb()).ConfigureAwait(false);
        }

        private static PayRunForCreationDomain GeneratePayRunForCreationDomain(DateTimeOffset dateFrom,
            DateTimeOffset dateTo, Guid creatorId, IEnumerable<InvoiceDomain> invoiceDomains, int payRunTypeId, int? payRunSubTypeId = null)
        {
            return new PayRunForCreationDomain
            {
                PayRunTypeId = payRunTypeId,
                PayRunSubTypeId = payRunSubTypeId,
                DateFrom = dateFrom,
                DateTo = dateTo,
                CreatorId = creatorId,
                UpdaterId = null,
                Invoices = invoiceDomains,
                InvoiceItems = new List<InvoiceItemMinimalDomain>()
            };
        }

        private async Task<List<InvoiceDomain>> GetInvoicesForPayRun(int invoiceStatusId, DateTimeOffset dateFrom, DateTimeOffset dateTo)
        {
            // Get invoices from date of last pay run with status new - fresh from supplier returns, never in a pay run before.

            var invoices = await _invoiceGateway
                .GetInvoiceListUsingInvoiceStatus(invoiceStatusId, dateFrom, dateTo)
                .ConfigureAwait(false);

            // If no invoices do not create pay run
            var invoiceDomains = invoices.ToList();
            if (!invoiceDomains.Any())
            {
                throw new EntityNotFoundException("No pending invoices to add to pay run");
            }

            return invoiceDomains;
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

            var invoiceDomains = await GetInvoicesForPayRun((int) InvoiceStatusEnum.Draft, dateFrom, dateTo).ConfigureAwait(false);

            // Add range to a new pay run. Date end is date today
            var newPayRunDomain = GeneratePayRunForCreationDomain(dateFrom, dateTo,
                new Guid("1f825b5f-5c65-41fb-8d9e-9d36d78fd6d8"), invoiceDomains, payRunTypeId, null);

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

            var invoiceDomains = await GetInvoicesForPayRun((int) InvoiceStatusEnum.Draft, dateFrom, dateTo).ConfigureAwait(false);

            // Add range to a new pay run. Date end is date today
            var newPayRunDomain = GeneratePayRunForCreationDomain(dateFrom, dateTo,
                new Guid("1f825b5f-5c65-41fb-8d9e-9d36d78fd6d8"), invoiceDomains, payRunTypeId, null);

            return await _payRunGateway.CreateNewPayRun(newPayRunDomain.ToDb()).ConfigureAwait(false);
        }

        public async Task<Guid> CreateResidentialReleaseHoldsPayRunUseCase()
        {
            const int payRunTypeId = (int) PayRunTypeEnum.ResidentialRecurring;
            const int payRunSubTypeId = (int) PayRunSubTypeEnum.ResidentialReleaseHolds;
            // Get date of last pay run. If none there are no holds so return
            //TODO: Change the date logic here. Account for a possible large list of released invoice items. dateFrom = min date invoice item was created. dateTo = max date invoice item was created.
            var dateFrom = await _invoiceGateway
                .GetMinDateOfReleasedInvoice((int) InvoiceStatusEnum.Released).ConfigureAwait(false);
            var dateTo = await _invoiceGateway
                .GetMaxDateOfReleasedInvoice((int) InvoiceStatusEnum.Released).ConfigureAwait(false);
            if (dateFrom == null || dateTo == null)
            {
                throw new EntityNotFoundException("There are no held invoices at this time");
            }
            // Get invoice items from date of last pay run with status new - fresh from supplier returns, never in a pay run before.
            var invoiceDomains = await GetInvoicesForPayRun((int) InvoiceStatusEnum.Released, (DateTimeOffset) dateFrom, (DateTimeOffset) dateTo).ConfigureAwait(false);

            // Add range to a new pay run. Date end is date today
            var newPayRunDomain = GeneratePayRunForCreationDomain((DateTimeOffset) dateFrom, (DateTimeOffset) dateTo,
                new Guid("1f825b5f-5c65-41fb-8d9e-9d36d78fd6d8"), invoiceDomains, payRunTypeId, payRunSubTypeId);

            return await _payRunGateway.CreateNewPayRun(newPayRunDomain.ToDb()).ConfigureAwait(false);
        }

        public async Task<Guid> CreateDirectPaymentsReleaseHoldsPayRunUseCase()
        {
            const int payRunTypeId = (int) PayRunTypeEnum.ResidentialRecurring;
            const int payRunSubTypeId = (int) PayRunSubTypeEnum.DirectPaymentsReleaseHolds;
            // Get date of last pay run. If none there are no holds so return
            //TODO: Change the date logic here. Account for a possible large list of released invoice items. dateFrom = min date invoice item was created. dateTo = max date invoice item was created.
            var dateFrom = await _invoiceGateway.GetMinDateOfReleasedInvoice((int) InvoiceStatusEnum.Released).ConfigureAwait(false);
            var dateTo = await _invoiceGateway.GetMaxDateOfReleasedInvoice((int) InvoiceStatusEnum.Released).ConfigureAwait(false);
            if (dateFrom == null || dateTo == null)
            {
                throw new EntityNotFoundException("There are no held invoices at this time");
            }
            // Get invoice items from date of last pay run with status new - fresh from supplier returns, never in a pay run before.
            var invoiceDomains = await GetInvoicesForPayRun((int) InvoiceStatusEnum.Released, (DateTimeOffset) dateFrom, (DateTimeOffset) dateTo).ConfigureAwait(false);

            // Add range to a new pay run. Date end is date today
            var newPayRunDomain = GeneratePayRunForCreationDomain((DateTimeOffset) dateFrom, (DateTimeOffset) dateTo,
                new Guid("1f825b5f-5c65-41fb-8d9e-9d36d78fd6d8"), invoiceDomains, payRunTypeId, payRunSubTypeId);

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
