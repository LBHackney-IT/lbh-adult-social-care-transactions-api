using AutoMapper;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.InvoicesDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.PayRunDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.RequestExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Gateways.InvoiceGateways
{
    public class InvoiceGateway : IInvoiceGateway
    {
        private readonly DatabaseContext _dbContext;
        private readonly IMapper _mapper;

        public InvoiceGateway(DatabaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InvoiceDomain>> GetInvoicesUsingItemPaymentStatus(int itemPaymentStatusId, DateTimeOffset? fromDate = null, DateTimeOffset? toDate = null)
        {
            // Get unique invoice ids
            var invoiceIds = await _dbContext.InvoiceItems
                .Where(ii =>
                    (fromDate.Equals(null) || ii.DateCreated >= fromDate) &&
                    (toDate.Equals(null) || ii.DateCreated <= toDate) &&
                    ii.InvoiceItemPaymentStatusId.Equals(itemPaymentStatusId)).Select(ii => ii.InvoiceId)
                .Distinct().ToListAsync().ConfigureAwait(false);

            // Get the invoices and invoice items in payment status
            var invoices = await _dbContext.Invoices.Where(ii => invoiceIds.Contains(ii.InvoiceId))
                .Include(ii =>
                    ii.InvoiceItems.Where(item => item.InvoiceItemPaymentStatusId.Equals(itemPaymentStatusId)))
                .ToListAsync().ConfigureAwait(false);

            return _mapper.Map<IEnumerable<InvoiceDomain>>(invoices);
        }

        public async Task<PagedList<InvoiceDomain>> GetInvoicesInPayRun(Guid payRunId, InvoiceListParameters parameters)
        {
            // Get unique invoice ids
            var invoiceIds = await _dbContext.PayRunItems
                .Where(ii =>
                    (parameters.DateFrom.Equals(null) || ii.InvoiceItem.Invoice.DateCreated >= parameters.DateFrom) &&
                    (parameters.DateTo.Equals(null) || ii.InvoiceItem.Invoice.DateCreated <= parameters.DateTo) &&
                    (parameters.SupplierId.Equals(null) ||
                     ii.InvoiceItem.Invoice.SupplierId.Equals(parameters.SupplierId)) &&
                    (parameters.PackageTypeId.Equals(null) ||
                     ii.InvoiceItem.Invoice.PackageTypeId.Equals(parameters.PackageTypeId)) &&
                    (parameters.InvoiceItemPaymentStatusId.Equals(null) ||
                     ii.InvoiceItem.InvoiceItemPaymentStatusId.Equals(parameters.InvoiceItemPaymentStatusId)) &&
                    ii.PayRunId.Equals(payRunId)
                    && (parameters.SearchTerm == null || EF.Functions.Like(
                        ii.InvoiceItem.Invoice.InvoiceNumber.ToLower(),
                        $"%{parameters.SearchTerm.Trim().ToLower()}%"))).Select(ii => ii.InvoiceItem.InvoiceId)
                .Distinct().ToListAsync().ConfigureAwait(false);

            var invoices = await _dbContext.Invoices.Where(i => invoiceIds.Contains(i.InvoiceId) && i.InvoiceItems.All(
                    ii => parameters.InvoiceItemPaymentStatusId == null ||
                          ii.InvoiceItemPaymentStatusId.Equals(parameters.InvoiceItemPaymentStatusId)))
                .Include(ii =>
                    ii.InvoiceItems)
                /*.Select(i => new Invoice
                {
                    DateCreated = i.DateCreated,
                    DateUpdated = i.DateUpdated,
                    InvoiceId = i.InvoiceId,
                    InvoiceNumber = i.InvoiceNumber,
                    SupplierId = i.SupplierId,
                    PackageTypeId = i.PackageTypeId,
                    ServiceUserId = i.ServiceUserId,
                    DateInvoiced = i.DateInvoiced,
                    TotalAmount = i.TotalAmount,
                    SupplierVATPercent = i.SupplierVATPercent,
                    InvoiceStatusId = i.InvoiceStatusId,
                    CreatorId = i.CreatorId,
                    UpdaterId = i.UpdaterId,
                    InvoiceItems = i.InvoiceItems.Where(item =>
                        parameters.InvoiceItemPaymentStatusId == null ||
                        item.InvoiceItemPaymentStatusId.Equals(parameters.InvoiceItemPaymentStatusId)).ToList()
                })*/
                .OrderBy(i => i.SupplierId)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync()
                .ConfigureAwait(false);

            return PagedList<InvoiceDomain>.ToPagedList(invoices.ToDomain(), invoiceIds.Count, parameters.PageNumber,
                parameters.PageSize);
        }

        public async Task<IEnumerable<InvoiceItemMinimalDomain>> GetInvoiceItemsUsingItemPaymentStatus(int itemPaymentStatusId, DateTimeOffset? fromDate = null, DateTimeOffset? toDate = null)
        {
            var invoiceItems = await _dbContext.InvoiceItems.Where(ii =>
                    (fromDate.Equals(null) || ii.DateCreated >= fromDate) &&
                    (toDate.Equals(null) || ii.DateCreated <= toDate) &&
                    ii.InvoiceItemPaymentStatusId.Equals(itemPaymentStatusId))
                .ToListAsync().ConfigureAwait(false);

            return _mapper.Map<IEnumerable<InvoiceItemMinimalDomain>>(invoiceItems);
        }

        public async Task<IEnumerable<PayRunItemsPaymentsByTypeDomain>> GetInvoiceItemsCountUsingItemPaymentStatus(int itemPaymentStatusId, DateTimeOffset? fromDate = null,
            DateTimeOffset? toDate = null)
        {
            return await _dbContext.PayRunItems.Where(ii =>
                    (fromDate.Equals(null) || ii.DateCreated >= fromDate) &&
                    (toDate.Equals(null) || ii.DateCreated <= toDate) &&
                    ii.InvoiceItem.InvoiceItemPaymentStatusId.Equals(itemPaymentStatusId))
                .GroupBy(pri => pri.PayRun.PayRunType.TypeName)
                .Select(pri => new PayRunItemsPaymentsByTypeDomain { Name = pri.Key, Value = pri.Count() })
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<DateTimeOffset?> GetMinDateOfReleasedInvoiceItem(int itemPaymentStatusId)
        {
            var minDateItem = await _dbContext.InvoiceItems.Where(ii => ii.InvoiceItemPaymentStatusId.Equals(itemPaymentStatusId))
                .OrderBy(ii => ii.DateCreated).FirstOrDefaultAsync().ConfigureAwait(false);
            return minDateItem?.DateCreated;
        }

        public async Task<DateTimeOffset?> GetMaxDateOfReleasedInvoiceItem(int itemPaymentStatusId)
        {
            var maxDateItem = await _dbContext.InvoiceItems.Where(ii => ii.InvoiceItemPaymentStatusId.Equals(itemPaymentStatusId))
                .OrderByDescending(ii => ii.DateCreated).FirstOrDefaultAsync().ConfigureAwait(false);
            return maxDateItem?.DateCreated;
        }

        public async Task<IEnumerable<InvoiceItemPaymentStatusDomain>> GetInvoiceItemPaymentStatuses()
        {
            return await _dbContext.InvoiceItemPaymentStatuses.Select(x => new InvoiceItemPaymentStatusDomain
            {
                StatusId = x.StatusId, StatusName = x.StatusName, DisplayName = x.DisplayName
            }).ToListAsync().ConfigureAwait(false);
        }
    }
}
