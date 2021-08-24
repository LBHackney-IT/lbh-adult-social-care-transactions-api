using AutoMapper;
using Common.CustomExceptions;
using Infrastructure.Domain.InvoicesDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.PayRunDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Extensions;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Invoices;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.RequestExtensions;
using Microsoft.EntityFrameworkCore;
using Npgsql.Bulk;
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

        public async Task<PagedList<InvoiceDomain>> GetInvoicesInPayRun(Guid payRunId, InvoiceListParameters parameters)
        {
            // Get unique invoice ids
            var invoiceIds = await _dbContext.PayRunItems
                .Where(pri =>
                    (parameters.DateFrom.Equals(null) || pri.Invoice.DateCreated >= parameters.DateFrom) &&
                    (parameters.DateTo.Equals(null) || pri.Invoice.DateCreated <= parameters.DateTo) &&
                    (parameters.SupplierId.Equals(null) ||
                     pri.Invoice.SupplierId.Equals(parameters.SupplierId)) &&
                    (parameters.ServiceUserId.Equals(null) ||
                     pri.Invoice.ServiceUserId.Equals(parameters.ServiceUserId)) &&
                    (parameters.PackageTypeId.Equals(null) ||
                     pri.Invoice.PackageTypeId.Equals(parameters.PackageTypeId)) &&
                    (parameters.InvoiceStatusId.Equals(null) ||
                     pri.Invoice.InvoiceStatusId.Equals(parameters.InvoiceStatusId)) &&
                    pri.PayRunId.Equals(payRunId)
                    && (parameters.InvoiceNumber == null || EF.Functions.Like(
                        pri.Invoice.InvoiceNumber.ToLower(),
                        $"%{parameters.InvoiceNumber.Trim().ToLower()}%"))).Select(pri => pri.InvoiceId)
                .Distinct().ToListAsync().ConfigureAwait(false);

            var invoices = await _dbContext.Invoices.Where(i => invoiceIds.Contains(i.InvoiceId))
                .Include(ii =>
                    ii.InvoiceItems)
                .OrderBy(i => i.SupplierId).ThenByDescending(i => i.DateCreated)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .Select(i => new InvoiceDomain
                {
                    InvoiceId = i.InvoiceId,
                    InvoiceNumber = i.InvoiceNumber,
                    SupplierId = i.SupplierId,
                    PackageTypeId = i.PackageTypeId,
                    PackageTypeName = i.PackageType.PackageTypeName,
                    ServiceUserId = i.ServiceUserId,
                    DateInvoiced = i.DateInvoiced,
                    TotalAmount = i.TotalAmount,
                    SupplierVATPercent = i.SupplierVATPercent,
                    InvoiceStatusId = i.InvoiceStatusId,
                    CreatorId = i.CreatorId,
                    UpdaterId = i.UpdaterId,
                    InvoiceItems = i.InvoiceItems.Select(
                        ii => new InvoiceItemMinimalDomain
                        {
                            InvoiceItemId = ii.InvoiceItemId,
                            InvoiceId = ii.InvoiceId,
                            ItemName = ii.ItemName,
                            PricePerUnit = ii.PricePerUnit,
                            Quantity = ii.Quantity,
                            SubTotal = ii.SubTotal,
                            VatAmount = ii.VatAmount,
                            TotalPrice = ii.TotalPrice,
                            PriceEffect = ii.PriceEffect,
                            ClaimedBy = ii.ClaimedBy,
                            ReclaimedFrom = ii.ReclaimedFrom,
                            SupplierReturnItemId = ii.SupplierReturnItemId,
                            CreatorId = ii.CreatorId,
                            UpdaterId = ii.UpdaterId
                        })
                })
                .ToListAsync()
                .ConfigureAwait(false);

            return PagedList<InvoiceDomain>.ToPagedList(invoices, invoiceIds.Count, parameters.PageNumber,
                parameters.PageSize);
        }

        /// <summary>
        /// Get invoices without the invoice items. One use case is get the invoice list for filter using invoice number
        /// </summary>
        /// <param name="payRunId"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<PagedList<InvoiceDomain>> GetInvoicesFlatInPayRunAsync(Guid payRunId, InvoiceListParameters parameters)
        {
            // Get unique invoice ids
            var invoiceIds = await _dbContext.PayRunItems
                .Where(pri =>
                    (parameters.DateFrom.Equals(null) || pri.Invoice.DateCreated >= parameters.DateFrom) &&
                    (parameters.DateTo.Equals(null) || pri.Invoice.DateCreated <= parameters.DateTo) &&
                    (parameters.SupplierId.Equals(null) ||
                     pri.Invoice.SupplierId.Equals(parameters.SupplierId)) &&
                    (parameters.ServiceUserId.Equals(null) ||
                     pri.Invoice.ServiceUserId.Equals(parameters.ServiceUserId)) &&
                    (parameters.PackageTypeId.Equals(null) ||
                     pri.Invoice.PackageTypeId.Equals(parameters.PackageTypeId)) &&
                    (parameters.InvoiceStatusId.Equals(null) ||
                     pri.Invoice.InvoiceStatusId.Equals(parameters.InvoiceStatusId)) &&
                    pri.PayRunId.Equals(payRunId)
                    && (parameters.InvoiceNumber == null || EF.Functions.Like(
                        pri.Invoice.InvoiceNumber.ToLower(),
                        $"%{parameters.InvoiceNumber.Trim().ToLower()}%"))).Select(pri => pri.InvoiceId)
                .Distinct().ToListAsync().ConfigureAwait(false);

            var invoices = await _dbContext.Invoices.Where(i => invoiceIds.Contains(i.InvoiceId))
                .Include(ii =>
                    ii.InvoiceItems)
                .OrderBy(i => i.SupplierId).ThenByDescending(i => i.DateCreated)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .Select(i => new InvoiceDomain
                {
                    InvoiceId = i.InvoiceId,
                    InvoiceNumber = i.InvoiceNumber,
                    SupplierId = i.SupplierId,
                    PackageTypeId = i.PackageTypeId,
                    PackageTypeName = i.PackageType.PackageTypeName,
                    ServiceUserId = i.ServiceUserId,
                    DateInvoiced = i.DateInvoiced,
                    TotalAmount = i.TotalAmount,
                    SupplierVATPercent = i.SupplierVATPercent,
                    InvoiceStatusId = i.InvoiceStatusId,
                    CreatorId = i.CreatorId,
                    UpdaterId = i.UpdaterId
                })
                .ToListAsync()
                .ConfigureAwait(false);

            return PagedList<InvoiceDomain>.ToPagedList(invoices, invoiceIds.Count, parameters.PageNumber,
                parameters.PageSize);
        }

        public async Task<IEnumerable<Invoice>> GetInvoicesFlatInPayRunAsync(Guid payRunId)
        {
            var invoices = await _dbContext.PayRunItems.Where(pri => pri.PayRunId.Equals(payRunId))
                .Select(pri => pri.Invoice)
                .Distinct()
                .ToListAsync()
                .ConfigureAwait(false);

            return invoices;
        }

        public async Task<IEnumerable<Invoice>> GetInvoicesInList(IEnumerable<Guid> invoiceIds)
        {
            var invoices = await _dbContext.Invoices.Where(i => invoiceIds.Contains(i.InvoiceId))
                .Distinct()
                .ToListAsync()
                .ConfigureAwait(false);

            return invoices;
        }

        public async Task<PagedList<HeldInvoiceDomain>> GetHeldInvoicePayments(HeldInvoicePaymentParameters parameters)
        {
            var payRunsWithHeldInvoicesIds = await _dbContext.DisputedInvoices
                .Where(pr =>
                    pr.Invoice.InvoiceStatusId.Equals((int) InvoiceStatusEnum.Held) &&
                    (parameters.DateFrom.Equals(null) || pr.PayRunItem.PayRun.DateFrom >= parameters.DateFrom) &&
                    (parameters.DateTo.Equals(null) || pr.PayRunItem.PayRun.DateTo <= parameters.DateTo) &&
                    (parameters.PackageTypeId.Equals(null) || pr.PayRunItem.Invoice.PackageTypeId.Equals(parameters.PackageTypeId)) &&
                    (parameters.WaitingOnId.Equals(null) || pr.DisputedInvoiceChats.OrderByDescending(c => c.DateCreated).First().ActionRequiredFromId.Equals(parameters.WaitingOnId)) &&
                    (parameters.ServiceUserId.Equals(null) || pr.PayRunItem.Invoice.ServiceUserId.Equals(parameters.ServiceUserId)) &&
                    (parameters.SupplierId.Equals(null) || pr.PayRunItem.Invoice.SupplierId.Equals(parameters.SupplierId))
                    )
                .OrderByDescending(pr => pr.PayRunItem.PayRun.DateCreated)
                .Select(pr => pr.PayRunItem.PayRunId)
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

            var selectedPayRunIds = payRunsWithHeldInvoicesIds
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize);

            var heldInvoices = await _dbContext.PayRuns.Where(p => selectedPayRunIds.Contains(p.PayRunId))
                .OrderByDescending(pr => pr.DateCreated)
                .Select(pr => new HeldInvoiceDomain
                {
                    PayRunId = pr.PayRunId,
                    PayRunDate = pr.DateCreated,
                    DateFrom = pr.DateFrom,
                    DateTo = pr.DateTo,
                    Invoices = pr.PayRunItems.Where(pri =>
                            pri.Invoice.InvoiceStatusId.Equals((int) InvoiceStatusEnum.Held) &&
                            (parameters.PackageTypeId.Equals(null) || pri.Invoice.PackageTypeId.Equals(parameters.PackageTypeId)) &&
                            (parameters.ServiceUserId.Equals(null) || pri.Invoice.ServiceUserId.Equals(parameters.ServiceUserId)) &&
                            (parameters.SupplierId.Equals(null) || pri.Invoice.SupplierId.Equals(parameters.SupplierId))
                            )
                        .Select(pri => new InvoiceDomain
                        {
                            InvoiceId = pri.InvoiceId,
                            InvoiceNumber = pri.Invoice.InvoiceNumber,
                            SupplierId = pri.Invoice.SupplierId,
                            PackageTypeId = pri.Invoice.PackageTypeId,
                            PackageTypeName = pri.Invoice.PackageType.PackageTypeName,
                            ServiceUserId = pri.Invoice.ServiceUserId,
                            DateInvoiced = pri.Invoice.DateInvoiced,
                            TotalAmount = pri.Invoice.TotalAmount,
                            SupplierVATPercent = pri.Invoice.SupplierVATPercent,
                            InvoiceStatusId = pri.Invoice.InvoiceStatusId,
                            CreatorId = pri.Invoice.CreatorId,
                            UpdaterId = pri.Invoice.UpdaterId,
                            InvoiceItems = pri.Invoice.InvoiceItems.Where(ii =>
                                ii.Invoice.InvoiceStatusId.Equals((int) InvoiceStatusEnum.Held)).Select(
                                ii => new InvoiceItemMinimalDomain
                                {
                                    InvoiceItemId = ii.InvoiceItemId,
                                    InvoiceId = ii.InvoiceId,
                                    ItemName = ii.ItemName,
                                    PricePerUnit = ii.PricePerUnit,
                                    Quantity = ii.Quantity,
                                    SubTotal = ii.SubTotal,
                                    VatAmount = ii.VatAmount,
                                    TotalPrice = ii.TotalPrice,
                                    PriceEffect = ii.PriceEffect,
                                    ClaimedBy = ii.ClaimedBy,
                                    ReclaimedFrom = ii.ReclaimedFrom,
                                    SupplierReturnItemId = ii.SupplierReturnItemId,
                                    CreatorId = ii.CreatorId,
                                    UpdaterId = ii.UpdaterId
                                }),
                            DisputedInvoiceChat =
                                pri.DisputedInvoice.DisputedInvoiceChats.Where(di =>
                                    di.DisputedInvoice.InvoiceId.Equals(pri.InvoiceId) &&
                                    di.DisputedInvoice.PayRunItemId.Equals(pri.PayRunItemId))
                                    .OrderBy(dic => dic.DateCreated)
                                    .Select(dic =>
                                    new DisputedInvoiceChatDomain
                                    {
                                        DisputedInvoiceChatId = dic.DisputedInvoiceChatId,
                                        DisputedInvoiceId = dic.DisputedInvoiceId,
                                        MessageRead = dic.MessageRead,
                                        Message = dic.Message,
                                        MessageFromId = dic.MessageFromId,
                                        ActionRequiredFromId = dic.ActionRequiredFromId,
                                        ActionRequiredFromName = dic.ActionRequiredFromDepartment.DepartmentName ?? "",
                                    })
                        })
                }).ToListAsync()
                .ConfigureAwait(false);

            return PagedList<HeldInvoiceDomain>.ToPagedList(heldInvoices, payRunsWithHeldInvoicesIds.Count, parameters.PageNumber,
                parameters.PageSize);
        }

        public async Task<IEnumerable<InvoiceDomain>> GetInvoiceListUsingPackageTypeAndInvoiceStatus(List<int> packageTypeIds, int invoiceStatusId, DateTimeOffset? fromDate = null, DateTimeOffset? toDate = null)
        {
            var invoiceItems = await _dbContext.Invoices.Where(ii =>
                    (packageTypeIds.Contains(ii.PackageTypeId)) &&
                    ((fromDate.Equals(null) || ii.DateCreated >= fromDate.GetValueOrDefault())) &&
                    // ((toDate.Equals(null) || toDate.GetValueOrDefault() > ii.DateCreated)) &&
                    ii.InvoiceStatusId.Equals(invoiceStatusId))
                .ToListAsync().ConfigureAwait(false);

            return _mapper.Map<IEnumerable<InvoiceDomain>>(invoiceItems);
        }

        public async Task<IEnumerable<PayRunItemsPaymentsByTypeDomain>> GetInvoicesCountUsingStatus(int invoiceStatusId, DateTimeOffset? fromDate = null,
            DateTimeOffset? toDate = null)
        {
            return await _dbContext.PayRunItems.Where(ii =>
                    (fromDate.Equals(null) || ii.DateCreated >= fromDate) &&
                    (toDate.Equals(null) || ii.DateCreated <= toDate) &&
                    ii.Invoice.InvoiceStatusId.Equals(invoiceStatusId))
                .GroupBy(pri => pri.PayRun.PayRunType.TypeName)
                .Select(pri => new PayRunItemsPaymentsByTypeDomain { Name = pri.Key, Value = pri.Count() })
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<DateTimeOffset?> GetMinDateOfReleasedInvoice(int itemPaymentStatusId)
        {
            var minDateItem = await _dbContext.Invoices.Where(i => i.InvoiceStatusId.Equals(itemPaymentStatusId))
                .OrderBy(ii => ii.DateCreated).FirstOrDefaultAsync().ConfigureAwait(false);
            return minDateItem?.DateCreated;
        }

        public async Task<DateTimeOffset?> GetMaxDateOfReleasedInvoice(int itemPaymentStatusId)
        {
            var maxDateItem = await _dbContext.Invoices.Where(i => i.InvoiceStatusId.Equals(itemPaymentStatusId))
                .OrderByDescending(ii => ii.DateCreated).FirstOrDefaultAsync().ConfigureAwait(false);
            return maxDateItem?.DateCreated;
        }

        public async Task<IEnumerable<InvoiceStatusDomain>> GetAllInvoiceStatuses()
        {
            return await _dbContext.InvoiceStatuses.Select(x => new InvoiceStatusDomain
            {
                StatusId = x.Id,
                StatusName = x.StatusName,
                DisplayName = x.DisplayName
            }).ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<InvoiceStatusDomain>> GetInvoicePaymentStatuses()
        {
            return await _dbContext.InvoiceStatuses.Where(s => s.ApprovalStatus.Equals(true))
                .Select(x => new InvoiceStatusDomain
                {
                    StatusId = x.Id,
                    StatusName = x.StatusName,
                    DisplayName = x.DisplayName
                }).ToListAsync().ConfigureAwait(false);
        }

        public async Task<InvoiceDomain> CreateInvoice(Invoice newInvoice)
        {
            //TODO: Invoice number
            var invoiceNumber = await _dbContext.InvoiceNumbers.Where(inu => inu.InvoiceNumberId.Equals(1))
                .SingleOrDefaultAsync().ConfigureAwait(false);

            newInvoice.InvoiceNumber = $"{invoiceNumber.Prefix} {invoiceNumber.CurrentInvoiceNumber}";
            var entry = await _dbContext.Invoices.AddAsync(newInvoice).ConfigureAwait(false);

            // Increment invoice number
            invoiceNumber.CurrentInvoiceNumber += 1;
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

        public async Task<IEnumerable<InvoiceDomain>> BatchCreateInvoices(IEnumerable<Invoice> invoices)
        {
            var savedInvoices = new List<InvoiceDomain>();

            await using (var transaction = await _dbContext.Database.BeginTransactionAsync().ConfigureAwait(false))
            {
                try
                {
                    foreach (var invoice in invoices)
                    {
                        var invoiceDomain = await CreateInvoice(invoice).ConfigureAwait(false);
                        savedInvoices.Add(invoiceDomain);
                    }

                    await transaction.CommitAsync().ConfigureAwait(false);
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync().ConfigureAwait(false);
                    throw;
                }
            }

            return savedInvoices;
        }

        public async Task<DisputedInvoiceFlatDomain> CreateDisputedInvoice(DisputedInvoice newDisputedInvoice)
        {
            var entry = await _dbContext.DisputedInvoices.AddAsync(newDisputedInvoice).ConfigureAwait(false);
            try
            {
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);

                // Change invoice status to held
                await ChangeInvoiceStatus(newDisputedInvoice.InvoiceId, (int) InvoiceStatusEnum.Held)
                    .ConfigureAwait(false);

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

        public async Task<bool> ChangeInvoiceListStatus(List<Guid> invoiceIds, int invoiceStatusId)
        {
            var invoices = await _dbContext.Invoices.Where(i => invoiceIds.Contains(i.InvoiceId)).ToListAsync()
                .ConfigureAwait(false);

            foreach (var invoice in invoices)
            {
                invoice.InvoiceStatusId = invoiceStatusId;
            }

            try
            {
                var uploader = new NpgsqlBulkUploader(_dbContext);
                await uploader.UpdateAsync(invoices).ConfigureAwait(false);
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

        public async Task<IEnumerable<PendingInvoicesDomain>> GetUserPendingInvoices(Guid serviceUserId)
        {
            var invoices = await _dbContext.Invoices.Where(ii => ii.ServiceUserId.Equals(serviceUserId))
                .Include(ii =>
                    ii.InvoiceItems)
                .ToListAsync().ConfigureAwait(false);

            return invoices.ToPendingInvoiceDomain();
        }

        public async Task<Invoice> CheckInvoiceExists(Guid invoiceId)
        {
            var invoice = await _dbContext.Invoices.Where(i => i.InvoiceId.Equals(invoiceId)).SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (invoice == null)
            {
                throw new EntityNotFoundException($"Invoice with id {invoiceId} not found");
            }

            return invoice;
        }

        public async Task<IEnumerable<InvoiceForResetDomain>> GetInvoicesForReset(Guid payRunId)
        {
            var allowedInvoiceStatuses = new List<int>
            {
                (int) InvoiceStatusEnum.Draft,
                (int) InvoiceStatusEnum.Approved,
                (int) InvoiceStatusEnum.InPayRun,
                (int) InvoiceStatusEnum.Held,
                (int) InvoiceStatusEnum.Accepted
            };

            var invoiceIds = await _dbContext.PayRunItems
                .Where(pri => pri.PayRunId.Equals(payRunId))
                .Select(pri => pri.InvoiceId)
                .Distinct().ToListAsync().ConfigureAwait(false);

            var invoices = await _dbContext.Invoices.Where(i => invoiceIds.Contains(i.InvoiceId) && allowedInvoiceStatuses.Contains(i.InvoiceStatusId))
                .Select(i => new InvoiceForResetDomain
                {
                    InvoiceId = i.InvoiceId,
                    PackageTypeId = i.PackageTypeId,
                    ServiceUserId = i.ServiceUserId,
                    SupplierId = i.SupplierId,
                    PackageId = i.PackageId
                }).ToListAsync().ConfigureAwait(false);

            return invoices;
        }
    }
}
