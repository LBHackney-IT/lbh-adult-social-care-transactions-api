using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.InvoiceGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Domain.BillsDomain.InvoicesDomains
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
        public async Task<IEnumerable<InvoiceDomain>> GetInvoicesUsingItemPaymentStatus(int itemPaymentStatusId, DateTimeOffset? fromDate = null)
        {
            // Get unique invoice ids
            var invoiceIds = await _dbContext.InvoiceItems
                .Where(ii =>
                    (ii.DateCreated.Equals(null) || ii.DateCreated >= fromDate) &&
                    ii.InvoiceItemPaymentStatusId.Equals(itemPaymentStatusId)).Select(ii => ii.InvoiceId)
                .Distinct().ToListAsync().ConfigureAwait(false);

            // Get the invoices and invoice items in payment status
            var invoices = await _dbContext.Invoices.Where(ii => invoiceIds.Contains(ii.InvoiceId))
                .Include(ii =>
                    ii.InvoiceItems.Where(item => item.InvoiceItemPaymentStatusId.Equals(itemPaymentStatusId)))
                .ToListAsync().ConfigureAwait(false);

            return _mapper.Map<IEnumerable<InvoiceDomain>>(invoices);
        }
    }
}
