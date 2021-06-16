using AutoMapper;
using LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.InvoicesDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.PayRunDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Exceptions.CustomExceptions;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Invoices;
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
                .OrderBy(i => i.SupplierId)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync()
                .ConfigureAwait(false);

            return PagedList<InvoiceDomain>.ToPagedList(invoices.ToInvoiceDomain(), invoiceIds.Count, parameters.PageNumber,
                parameters.PageSize);
        }

        public async Task<IEnumerable<HeldInvoiceDomain>> GetHeldInvoicePayments()
        {
            var payRunsWithHeldInvoicesIds = await _dbContext.PayRunItems.Where(pr =>
                    pr.Invoice.InvoiceStatusId.Equals((int) InvoiceStatusEnum.Held) ||
                    pr.InvoiceItem.InvoiceItemPaymentStatusId.Equals((int) InvoiceItemPaymentStatusEnum.Held))
                .Select(pr => pr.PayRunId)
                .Distinct()
                .ToListAsync()
                .ConfigureAwait(false);

            /*var res = (from pr in _dbContext.PayRuns
                join pri in _dbContext.PayRunItems on pr.PayRunId equals pri.PayRun.PayRunId
                join i in _dbContext.Invoices on pri.Invoice.InvoiceId equals i.InvoiceId
                join ii in _dbContext.InvoiceItems on i.InvoiceId equals ii.InvoiceId
                join di in _dbContext.DisputedInvoiceChats on i.InvoiceId equals di.DisputedInvoice.InvoiceId
                where payRunsWithHeldInvoicesIds.Contains(pr.PayRunId) && pri.Invoice.InvoiceStatusId.Equals((int) InvoiceStatusEnum.Held)
                       select pr).ToList();*/

            var heldInvoices = await _dbContext.PayRuns.Where(p => payRunsWithHeldInvoicesIds.Contains(p.PayRunId))
                .Select(pr => new HeldInvoiceDomain
                {
                    PayRunId = pr.PayRunId,
                    PayRunItemId = pr.PayRunId,
                    PayRunDate = pr.DateCreated,
                    Invoices = pr.PayRunItems.Where(pri =>
                            pri.Invoice.InvoiceStatusId.Equals((int) InvoiceStatusEnum.Held) ||
                            pri.InvoiceItem.InvoiceItemPaymentStatusId.Equals((int) InvoiceItemPaymentStatusEnum.Held))
                        .Select(pri => new InvoiceDomain
                        {
                            InvoiceId = pri.InvoiceId,
                            InvoiceNumber = pri.Invoice.InvoiceNumber,
                            SupplierId = pri.Invoice.SupplierId,
                            PackageTypeId = pri.Invoice.PackageTypeId,
                            ServiceUserId = pri.Invoice.ServiceUserId,
                            DateInvoiced = pri.Invoice.DateInvoiced,
                            TotalAmount = pri.Invoice.TotalAmount,
                            SupplierVATPercent = pri.Invoice.SupplierVATPercent,
                            InvoiceStatusId = pri.Invoice.InvoiceStatusId,
                            CreatorId = pri.Invoice.CreatorId,
                            UpdaterId = pri.Invoice.UpdaterId,
                            InvoiceItems = pri.Invoice.InvoiceItems.Where(ii =>
                                ii.Invoice.InvoiceStatusId.Equals((int) InvoiceStatusEnum.Held) ||
                                ii.InvoiceItemPaymentStatusId.Equals((int) InvoiceItemPaymentStatusEnum.Held)).Select(
                                ii => new InvoiceItemMinimalDomain
                                {
                                    InvoiceItemId = ii.InvoiceItemId,
                                    InvoiceId = ii.InvoiceId,
                                    InvoiceItemPaymentStatusId = ii.InvoiceItemPaymentStatusId,
                                    ItemName = ii.ItemName,
                                    PricePerUnit = ii.PricePerUnit,
                                    Quantity = ii.Quantity,
                                    SubTotal = ii.SubTotal,
                                    VatAmount = ii.VatAmount,
                                    TotalPrice = ii.TotalPrice,
                                    SupplierReturnItemId = ii.SupplierReturnItemId,
                                    CreatorId = ii.CreatorId,
                                    UpdaterId = ii.UpdaterId
                                }),
                            DisputedInvoiceChat =
                                pri.Invoice.DisputedInvoiceChat.Where(di =>
                                    di.DisputedInvoice.InvoiceId.Equals(pri.InvoiceId) &&
                                    di.DisputedInvoice.InvoiceItemId.Equals(pri.InvoiceItemId)).Select(dic =>
                                    new DisputedInvoiceChatDomain
                                    {
                                        DisputedInvoiceChatId = dic.DisputedInvoiceChatId,
                                        DisputedInvoiceId = dic.DisputedInvoiceId,
                                        MessageRead = dic.MessageRead,
                                        Message = dic.Message,
                                        MessageFromId = dic.MessageFromId,
                                        ActionRequiredFromId = dic.ActionRequiredFromId
                                    })
                        })
                }).ToListAsync()
                .ConfigureAwait(false);

            return heldInvoices;
        }

        public async Task<IEnumerable<InvoiceDomain>> GetInvoiceListUsingInvoiceStatus(int invoiceStatusId, DateTimeOffset? fromDate = null, DateTimeOffset? toDate = null)
        {
            var invoiceItems = await _dbContext.Invoices.Where(ii =>
                    (fromDate.Equals(null) || ii.DateCreated >= fromDate) &&
                    (toDate.Equals(null) || ii.DateCreated <= toDate) &&
                    ii.InvoiceStatusId.Equals(invoiceStatusId))
                .ToListAsync().ConfigureAwait(false);

            return _mapper.Map<IEnumerable<InvoiceDomain>>(invoiceItems);
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
                StatusId = x.StatusId,
                StatusName = x.StatusName,
                DisplayName = x.DisplayName
            }).ToListAsync().ConfigureAwait(false);
        }

        public async Task<InvoiceDomain> CreateInvoice(Invoice newInvoice)
        {
            var entry = await _dbContext.Invoices.AddAsync(newInvoice).ConfigureAwait(false);
            try
            {
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);

                return entry.Entity.ToInvoiceDomain();
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new DbSaveFailedException($"Could not save invoice to DB: {dbUpdateException.InnerException?.Message}");
            }
            catch (Exception e)
            {
                // Console.WriteLine(e.GetType());
                throw new DbSaveFailedException($"Could not save invoice to DB: {e.InnerException?.Message}");
            }
        }

        public async Task<DisputedInvoiceFlatDomain> CreateDisputedInvoice(DisputedInvoice newDisputedInvoice)
        {
            var entry = await _dbContext.DisputedInvoices.AddAsync(newDisputedInvoice).ConfigureAwait(false);
            try
            {
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);

                return entry.Entity.ToDomain();
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new DbSaveFailedException($"Could not save disputed invoice to DB: {dbUpdateException.InnerException?.Message}");
            }
            catch (Exception e)
            {
                // Console.WriteLine(e.GetType());
                throw new DbSaveFailedException($"Could not save disputed invoice to DB: {e.InnerException?.Message}");
            }
        }

        public async Task<bool> ChangeInvoiceStatus(Guid invoiceId, int invoiceStatusId)
        {
            var invoice = await _dbContext.Invoices.Where(i => i.InvoiceId.Equals(invoiceId)).SingleOrDefaultAsync().ConfigureAwait(false);
            if (invoice == null)
            {
                throw new EntityNotFoundException($"Invoice with id {invoiceId} not found in the database");
            }

            invoice.InvoiceStatusId = invoiceStatusId;

            try
            {
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);

                return true;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new DbSaveFailedException($"Could not update invoice status: {dbUpdateException.InnerException?.Message}");
            }
            catch (Exception e)
            {
                throw new DbSaveFailedException($"Could not update invoice status: {e.InnerException?.Message}");
            }
        }

        public async Task<bool> ChangeInvoiceItemPaymentStatus(Guid payRunId, Guid invoiceItemId, int invoiceItemPaymentStatusId)
        {
            var res = await _dbContext.PayRunItems
                .Where(pr => pr.PayRunId.Equals(payRunId) && pr.InvoiceItemId.Equals(invoiceItemId))
                .Select(pr => pr.InvoiceItem).SingleOrDefaultAsync().ConfigureAwait(false);

            if (res == null)
            {
                throw new EntityNotFoundException(
                    $"Invoice item with id {invoiceItemId} not found in pay run with id {payRunId}");
            }

            res.InvoiceItemPaymentStatusId = invoiceItemPaymentStatusId;

            try
            {
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);

                return true;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new DbSaveFailedException($"Could not update invoice item payment status: {dbUpdateException.InnerException?.Message}");
            }
            catch (Exception e)
            {
                throw new DbSaveFailedException($"Could not update invoice item payment status: {e.InnerException?.Message}");
            }
        }

        public async Task<IEnumerable<PendingInvoicesDomain>> GetUserPendingInvoices(Guid serviceUserId)
        {
            var invoices = await _dbContext.Invoices.Where(ii => ii.ServiceUserId.Equals(serviceUserId))
                .Include(ii =>
                    ii.InvoiceItems)
                .ToListAsync().ConfigureAwait(false);

            return invoices.ToPendingInvoiceDomain();
        }
    }
}
