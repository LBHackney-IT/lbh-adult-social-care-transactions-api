using Common.CustomExceptions;
using Infrastructure.Domain.InvoicesDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.PayRunDomains;
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

        public async Task<Guid> CreateResidentialRecurringPayRunUseCase(DateTimeOffset dateTo)
        {
            const int payRunTypeId = (int) PayRunTypeEnum.ResidentialRecurring;

            var draftPayRunCount = await _payRunGateway.GetDraftPayRunCount(payRunTypeId).ConfigureAwait(false);
            if (draftPayRunCount > 0)
                throw new ApiException($"A Pay Run with draft status for Residential Recurring already exists!");

            // Get date of last pay run. If none make it today-28 days
            var lastPayRunDate = await _payRunGateway.GetDateOfLastPayRun(payRunTypeId).ConfigureAwait(false);
            // var dateTo = dateFrom.AddDays(28);

            // If date to is greater than today, make dateTo today's date
            if (dateTo > DateTimeOffset.Now)
            {
                dateTo = DateTimeOffset.Now;
            }

            var validPackageTypeIds = new List<int>
            {
                (int) PackageTypeEnum.NursingCarePackage, (int) PackageTypeEnum.ResidentialCarePackage
            };

            // Date from null to get all invoices before date to
            var invoiceDomains = await GetInvoicesForPayRun(validPackageTypeIds, (int) InvoiceStatusEnum.Draft, null, dateTo).ConfigureAwait(false);

            // Set date from as the smallest date on invoice
            var dateFrom = invoiceDomains
                .OrderBy(i => i.DateInvoiced)
                .Select(i => i.DateInvoiced).First();

            return await CreatePayRun(invoiceDomains, lastPayRunDate, dateTo, payRunTypeId).ConfigureAwait(false);
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

        private async Task<List<InvoiceDomain>> GetInvoicesForPayRun(List<int> packageTypeIds, int invoiceStatusId, DateTimeOffset? dateFrom, DateTimeOffset dateTo)
        {
            // Get invoices from date of last pay run with status new/draft

            var invoices = await _invoiceGateway
                .GetInvoiceListUsingPackageTypeAndInvoiceStatus(packageTypeIds, invoiceStatusId, dateFrom, dateTo)
                .ConfigureAwait(false);

            // If no invoices do not create pay run
            var invoiceDomains = invoices.ToList();
            if (!invoiceDomains.Any())
            {
                throw new EntityNotFoundException("No pending invoices to add to pay run");
            }

            return invoiceDomains;
        }

        public async Task<Guid> CreateDirectPaymentsPayRunUseCase(DateTimeOffset dateTo)
        {
            const int payRunTypeId = (int) PayRunTypeEnum.DirectPayments;
            // Get date of last pay run. If none make it today-28 days
            var dateFrom = await _payRunGateway.GetDateOfLastPayRun(payRunTypeId).ConfigureAwait(false);
            // var dateTo = dateFrom.AddDays(28);

            // If date to is greater than today, make dateTo today's date
            if (dateTo > DateTimeOffset.Now)
            {
                dateTo = DateTimeOffset.Now;
            }

            var validPackageTypeIds = new List<int>
            {
                (int) PackageTypeEnum.DayCarePackage
            };

            var invoiceDomains = await GetInvoicesForPayRun(validPackageTypeIds, (int) InvoiceStatusEnum.Draft, dateFrom, dateTo).ConfigureAwait(false);

            return await CreatePayRun(invoiceDomains, dateFrom, dateTo, payRunTypeId).ConfigureAwait(false);
        }

        public async Task<Guid> CreateHomeCarePayRunUseCase(DateTimeOffset dateTo)
        {
            const int payRunTypeId = (int) PayRunTypeEnum.HomeCare;
            // Get date of last pay run. If none make it today-28 days
            var dateFrom = await _payRunGateway.GetDateOfLastPayRun(payRunTypeId).ConfigureAwait(false);
            // var dateTo = dateFrom.AddDays(28);

            // If date to is greater than today, make dateTo today's date
            if (dateTo > DateTimeOffset.Now)
            {
                dateTo = DateTimeOffset.Now;
            }

            var validPackageTypeIds = new List<int>
            {
                (int) PackageTypeEnum.HomeCarePackage
            };

            var invoiceDomains = await GetInvoicesForPayRun(validPackageTypeIds, (int) InvoiceStatusEnum.Draft, dateFrom, dateTo).ConfigureAwait(false);

            return await CreatePayRun(invoiceDomains, dateFrom, dateTo, payRunTypeId).ConfigureAwait(false);
        }

        public async Task<Guid> CreateResidentialReleaseHoldsPayRunUseCase(DateTimeOffset dateTo)
        {
            const int payRunTypeId = (int) PayRunTypeEnum.ResidentialRecurring;
            const int payRunSubTypeId = (int) PayRunSubTypeEnum.ResidentialReleaseHolds;
            // Get date of last pay run. If none there are no holds so return
            //TODO: Change the date logic here. Account for a possible large list of released invoice items. dateFrom = min date invoice item was created. dateTo = max date invoice item was created.
            var dateFrom = await _invoiceGateway
                .GetMinDateOfReleasedInvoice((int) InvoiceStatusEnum.Released).ConfigureAwait(false);
            /*var dateTo = await _invoiceGateway
                .GetMaxDateOfReleasedInvoice((int) InvoiceStatusEnum.Released).ConfigureAwait(false);*/
            if (dateFrom == null || dateTo == null)
            {
                throw new EntityNotFoundException("There are no held invoices at this time");
            }

            var validPackageTypeIds = new List<int>
            {
                (int) PackageTypeEnum.NursingCarePackage, (int) PackageTypeEnum.ResidentialCarePackage
            };
            // Get invoice items from date of last pay run with status new - fresh from supplier returns, never in a pay run before.
            var invoiceDomains = await GetInvoicesForPayRun(validPackageTypeIds, (int) InvoiceStatusEnum.Released, (DateTimeOffset) dateFrom, (DateTimeOffset) dateTo).ConfigureAwait(false);

            return await CreatePayRun(invoiceDomains, (DateTimeOffset) dateFrom, (DateTimeOffset) dateTo, payRunTypeId, payRunSubTypeId).ConfigureAwait(false);
        }

        public async Task<Guid> CreateDirectPaymentsReleaseHoldsPayRunUseCase(DateTimeOffset dateTo)
        {
            const int payRunTypeId = (int) PayRunTypeEnum.ResidentialRecurring;
            const int payRunSubTypeId = (int) PayRunSubTypeEnum.DirectPaymentsReleaseHolds;
            // Get date of last pay run. If none there are no holds so return
            //TODO: Change the date logic here. Account for a possible large list of released invoice items. dateFrom = min date invoice item was created. dateTo = max date invoice item was created.
            var dateFrom = await _invoiceGateway.GetMinDateOfReleasedInvoice((int) InvoiceStatusEnum.Released).ConfigureAwait(false);
            // var dateTo = await _invoiceGateway.GetMaxDateOfReleasedInvoice((int) InvoiceStatusEnum.Released).ConfigureAwait(false);
            if (dateFrom == null || dateTo == null)
            {
                throw new EntityNotFoundException("There are no held invoices at this time");
            }
            var validPackageTypeIds = new List<int>
            {
                (int) PackageTypeEnum.HomeCarePackage, (int) PackageTypeEnum.DayCarePackage
            };
            // Get invoice items from date of last pay run with status new - fresh from supplier returns, never in a pay run before.
            var invoiceDomains = await GetInvoicesForPayRun(validPackageTypeIds, (int) InvoiceStatusEnum.Released, (DateTimeOffset) dateFrom, (DateTimeOffset) dateTo).ConfigureAwait(false);

            return await CreatePayRun(invoiceDomains, (DateTimeOffset) dateFrom, (DateTimeOffset) dateTo, payRunTypeId, payRunSubTypeId).ConfigureAwait(false);
        }

        private async Task<Guid> CreatePayRun(IList<InvoiceDomain> invoiceDomains, DateTimeOffset dateFrom, DateTimeOffset dateTo, int payRunTypeId, int? payRunSubTypeId = null)
        {
            // Add range to a new pay run. Date end is date today
            var newPayRunDomain = GeneratePayRunForCreationDomain(dateFrom, dateTo,
                new Guid("1f825b5f-5c65-41fb-8d9e-9d36d78fd6d8"), invoiceDomains, payRunTypeId, payRunSubTypeId);

            var res = await _payRunGateway.CreateNewPayRun(newPayRunDomain.ToDb()).ConfigureAwait(false);
            var invoiceIds = invoiceDomains.Select(i => i.InvoiceId).Distinct().ToList();
            await _invoiceGateway.ChangeInvoiceListStatus(invoiceIds, (int) InvoiceStatusEnum.InPayRun).ConfigureAwait(false);
            return res;
        }

        public async Task<Guid> CreateNewPayRunUseCase(string payRunType, DateTimeOffset dateTo)
        {
            if (!PayRunTypeEnum.ResidentialRecurring.EnumIsDefined(payRunType) && !PayRunSubTypeEnum.DirectPaymentsReleaseHolds.EnumIsDefined(payRunType))
            {
                throw new EntityNotFoundException("The pay run type is not valid. Please check and try again");
            }

            return payRunType switch
            {
                nameof(PayRunTypeEnum.ResidentialRecurring) => await CreateResidentialRecurringPayRunUseCase(dateTo)
                    .ConfigureAwait(false),
                nameof(PayRunTypeEnum.DirectPayments) => await CreateDirectPaymentsPayRunUseCase(dateTo)
                    .ConfigureAwait(false),
                nameof(PayRunTypeEnum.HomeCare) => await CreateHomeCarePayRunUseCase(dateTo)
                    .ConfigureAwait(false),
                nameof(PayRunSubTypeEnum.ResidentialReleaseHolds) => await CreateResidentialReleaseHoldsPayRunUseCase(dateTo)
                    .ConfigureAwait(false),
                nameof(PayRunSubTypeEnum.DirectPaymentsReleaseHolds) => await
                    CreateDirectPaymentsReleaseHoldsPayRunUseCase(dateTo)
                        .ConfigureAwait(false),
                _ => throw new EntityNotFoundException("The pay run type is not valid. Please check and try again")
            };
        }
    }
}
