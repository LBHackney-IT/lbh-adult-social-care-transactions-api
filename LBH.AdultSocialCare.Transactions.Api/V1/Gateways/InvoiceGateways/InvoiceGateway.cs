using AutoMapper;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.InvoicesDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.PayRunDomains;

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
    }
}
