using AutoMapper;
using LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.InvoicesDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.PackageTypeDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.PayRunDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.SupplierDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Exceptions.CustomExceptions;
using LBH.AdultSocialCare.Transactions.Api.V1.Extensions;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.InvoiceGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Invoices;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.PayRunModels;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.RequestExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Npgsql.Bulk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Gateways.PayRunGateways
{
    public class PayRunGateway : IPayRunGateway
    {
        private readonly DatabaseContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IInvoiceGateway _invoiceGateway;

        public PayRunGateway(DatabaseContext dbContext, IMapper mapper, IInvoiceGateway invoiceGateway)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _invoiceGateway = invoiceGateway;
        }

        public async Task<DateTimeOffset> GetDateOfLastPayRun(int payRunTypeId, int? payRunSubTypeId = null)
        {
            var lastPayRun = await _dbContext.PayRuns.Where(pr =>
                    pr.PayRunTypeId.Equals(payRunTypeId) &&
                    (pr.PayRunSubTypeId.Equals(payRunSubTypeId) || payRunSubTypeId.Equals(null)))
                .OrderByDescending(pr => pr.DateTo)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);

            return lastPayRun?.DateTo ?? DateTimeOffset.Now.AddDays(-28);
        }

        public async Task<PagedList<PayRunSummaryDomain>> GetPayRunSummaryList(PayRunSummaryListParameters parameters)
        {
            var payRunList = await _dbContext.PayRuns
                .FilterPayRunSummaryList(parameters.PayRunId, parameters.PayRunTypeId, parameters.PayRunSubTypeId,
                    parameters.PayRunStatusId, parameters.DateFrom, parameters.DateTo)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .Select(pr => new PayRunSummaryDomain
                {
                    PayRunId = pr.PayRunId,
                    PayRunNumber = pr.PayRunNumber,
                    PayRunTypeId = pr.PayRunTypeId,
                    PayRunTypeName = pr.PayRunType.TypeName,
                    PayRunSubTypeId = pr.PayRunSubTypeId,
                    PayRunSubTypeName = pr.PayRunSubType.SubTypeName,
                    PayRunStatusId = pr.PayRunStatusId,
                    PayRunStatusName = pr.PayRunStatus.StatusName,
                    TotalAmountPaid =
                        pr.PayRunItems
                            .Where(pri =>
                                pri.Invoice.InvoiceStatusId.Equals(
                                    (int) InvoiceStatusEnum.Paid)).Sum(x => x.PaidAmount),
                    TotalAmountHeld =
                        pr.PayRunItems
                            .Where(pri =>
                                pri.Invoice.InvoiceStatusId.Equals(
                                    (int) InvoiceStatusEnum.Held)).Sum(x => x.Invoice.TotalAmount),
                    DateFrom = pr.DateFrom,
                    DateTo = pr.DateTo,
                    DateCreated = pr.DateCreated
                }).ToListAsync().ConfigureAwait(false);

            var payRunCount = await _dbContext.PayRuns
                .FilterPayRunSummaryList(parameters.PayRunId, parameters.PayRunTypeId, parameters.PayRunSubTypeId,
                    parameters.PayRunStatusId, parameters.DateFrom, parameters.DateTo)
                .CountAsync().ConfigureAwait(false);

            return PagedList<PayRunSummaryDomain>.ToPagedList(payRunList, payRunCount, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<PayRunFlatDomain> GetPayRunFlat(Guid payRunId)
        {
            return await _dbContext.PayRuns.Where(pr => pr.PayRunId.Equals(payRunId))
                .Select(pr => new PayRunFlatDomain
                {
                    PayRunId = pr.PayRunId,
                    PayRunNumber = pr.PayRunNumber,
                    PayRunTypeId = pr.PayRunTypeId,
                    PayRunSubTypeId = pr.PayRunSubTypeId,
                    PayRunStatusId = pr.PayRunStatusId,
                    DateFrom = pr.DateFrom,
                    DateTo = pr.DateTo,
                    CreatorId = pr.CreatorId,
                    UpdaterId = pr.UpdaterId
                })
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
        }

        public async Task<PayRunItem> CheckPayRunItemExists(Guid payRunId, Guid payRunItemId)
        {
            var res = await _dbContext.PayRunItems
                .Where(pri => pri.PayRunId.Equals(payRunId) && pri.PayRunItemId.Equals(payRunItemId))
                .SingleOrDefaultAsync().ConfigureAwait(false);

            if (res == null)
            {
                throw new EntityNotFoundException($"Pay run item with id {payRunItemId} not found in the database");
            }

            return res;
        }

        public async Task<Guid> CreateNewPayRun(PayRun payRunForCreation)
        {
            var entry = await _dbContext.PayRuns.AddAsync(payRunForCreation).ConfigureAwait(false);
            try
            {
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);

                return entry.Entity.PayRunId;
            }
            catch (Exception)
            {
                throw new DbSaveFailedException("Could not save pay run to database");
            }
        }

        public async Task<PagedList<SupplierMinimalDomain>> GetUniqueSuppliersInPayRun(Guid payRunId, SupplierListParameters parameters)
        {
            var supplierList = await _dbContext.PayRunItems.Where(
                    pr => pr.PayRunId.Equals(payRunId)
                          && (parameters.SearchTerm == null || EF.Functions.Like(
                              pr.InvoiceItem.Invoice.Supplier.SupplierName.ToLower(),
                              $"%{parameters.SearchTerm.Trim().ToLower()}%"))
                )
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .Select(ri => new
                {
                    ri.Invoice.Supplier.SupplierId,
                    ri.Invoice.Supplier.SupplierName
                }).Distinct()
                .Select(ri => new SupplierMinimalDomain { SupplierId = ri.SupplierId, SupplierName = ri.SupplierName })
                .ToListAsync()
                .ConfigureAwait(false);

            var supplierCount = await _dbContext.PayRunItems.Where(
                    pr => pr.PayRunId.Equals(payRunId)
                          && (parameters.SearchTerm == null || EF.Functions.Like(
                              pr.InvoiceItem.Invoice.Supplier.SupplierName.ToLower(),
                              $"%{parameters.SearchTerm.Trim().ToLower()}%"))
                )
                .CountAsync()
                .ConfigureAwait(false);

            return PagedList<SupplierMinimalDomain>.ToPagedList(supplierList, supplierCount, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<IEnumerable<PackageTypeDomain>> GetUniquePackageTypesInPayRun(Guid payRunId)
        {
            return await _dbContext.PayRunItems.Where(pr => pr.PayRunId.Equals(payRunId))
                .Select(pr => new
                {
                    pr.Invoice.PackageType.PackageTypeId,
                    pr.Invoice.PackageType.PackageTypeName
                }).Distinct()
                .Select(pr => new PackageTypeDomain
                {
                    PackageTypeId = pr.PackageTypeId,
                    PackageTypeName = pr.PackageTypeName
                })
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<InvoiceStatusDomain>> GetUniqueInvoiceItemPaymentStatusesInPayRun(Guid payRunId)
        {
            return await _dbContext.PayRunItems.Where(pr => pr.PayRunId.Equals(payRunId))
                .Select(pr => new
                {
                    pr.Invoice.InvoiceStatus.Id,
                    pr.Invoice.InvoiceStatus.StatusName,
                    pr.Invoice.InvoiceStatus.DisplayName,
                }).Distinct()
                .Select(pr => new InvoiceStatusDomain
                {
                    StatusId = pr.Id,
                    StatusName = pr.StatusName,
                    DisplayName = pr.DisplayName
                })
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<bool> ChangePayRunStatus(Guid payRunId, int newPayRunStatusId)
        {
            var payRun = await _dbContext.PayRuns.Where(p => p.PayRunId.Equals(payRunId)).SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (payRun == null)
            {
                throw new EntityNotFoundException($"Pay run with id {payRunId} not found");
            }

            if (payRun.PayRunStatusId.Equals((int) PayRunStatusesEnum.Approved))
            {
                throw new ApiException($"Pay run with id {payRunId} is already approved for payment", StatusCodes.Status409Conflict);
            }

            payRun.PayRunStatusId = newPayRunStatusId;

            try
            {
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);

                return true;
            }
            catch (Exception)
            {
                throw new DbSaveFailedException("Could not save pay run status change to database");
            }
        }

        public async Task<bool> ReleaseHeldInvoicePayment(Guid payRunId, Guid invoiceId, Guid? invoiceItemId)
        {
            // Get the invoice item
            var invoice = await _dbContext.PayRunItems
                .Where(pri =>
                    pri.PayRunId.Equals(payRunId) && pri.InvoiceId.Equals(invoiceId) &&
                    (invoiceId == null || pri.InvoiceItemId.Equals(invoiceItemId)))
                .Select(pri => pri.Invoice)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);

            if (invoice == null)
            {
                throw new EntityNotFoundException($"Invoice item with id {invoiceItemId} not found");
            }

            if (invoice.InvoiceStatusId != (int) InvoiceStatusEnum.Held)
            {
                throw new ApiException($"Invoice with id {invoiceId} is not held", StatusCodes.Status422UnprocessableEntity);
            }

            invoice.InvoiceStatusId = (int) InvoiceStatusEnum.Released;

            try
            {
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);

                return true;
            }
            catch (Exception)
            {
                throw new DbSaveFailedException("Could not save release status to database");
            }
        }

        public async Task<PayRun> CheckPayRunExists(Guid payRunId)
        {
            var payRun = await _dbContext.PayRuns.Where(pr => pr.PayRunId.Equals(payRunId))
                .AsNoTracking()
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);

            // if not found return
            if (payRun == null)
            {
                throw new EntityNotFoundException($"Pay run with id {payRunId} not found");
            }

            return payRun;
        }

        public async Task<PayRunInsightsDomain> GetPayRunInsights(Guid payRunId)
        {
            // get this pay run
            var thisPayRun = await CheckPayRunExists(payRunId).ConfigureAwait(false);

            var thisPayRunAmount = await _dbContext.PayRunItems.Where(pr => pr.PayRunId.Equals(payRunId))
                .Select(pr => pr.Invoice.TotalAmount).SumAsync().ConfigureAwait(false);

            // get previous pay run id
            var previousPayRun = await _dbContext.PayRuns.Where(pr => pr.DateTo < thisPayRun.DateFrom)
                .OrderByDescending(pr => pr.DateTo).FirstOrDefaultAsync().ConfigureAwait(false);

            var previousPayRunAmount = new decimal(0.0);

            if (previousPayRun != null)
            {
                previousPayRunAmount = await _dbContext.PayRunItems.Where(pr => pr.PayRunId.Equals(previousPayRun.PayRunId))
                    .Select(pr => pr.Invoice.TotalAmount).SumAsync().ConfigureAwait(false);
            }

            var supplierCount = await _dbContext.PayRunItems.Where(pr => pr.PayRunId.Equals(payRunId))
                .Select(pr => new { pr.Invoice.SupplierId }).Distinct()
                .CountAsync()
                .ConfigureAwait(false);

            var serviceUserCount = await _dbContext.PayRunItems.Where(pr => pr.PayRunId.Equals(payRunId))
                .Select(pr => new { pr.Invoice.ServiceUserId }).Distinct()
                .CountAsync()
                .ConfigureAwait(false);

            var heldInvoiceCount = await _dbContext.PayRunItems.Where(pr =>
                    pr.PayRunId.Equals(payRunId) &&
                    pr.Invoice.InvoiceStatusId.Equals((int) InvoiceStatusEnum.Held))
                .Select(pr => new { pr.Invoice.InvoiceId }).Distinct()
                .CountAsync()
                .ConfigureAwait(false);

            var holdsAmount = await _dbContext.PayRunItems.Where(pr =>
                    pr.PayRunId.Equals(payRunId) &&
                    pr.Invoice.InvoiceStatusId.Equals((int) InvoiceStatusEnum.Held))
                .Select(pr => pr.Invoice.TotalAmount).SumAsync().ConfigureAwait(false);

            return new PayRunInsightsDomain
            {
                PayRunId = payRunId,
                TotalAmount = thisPayRunAmount,
                AmountDifferenceFromLastCycle = thisPayRunAmount - previousPayRunAmount,
                PercentageIncreaseFromLastCycle = CalculationExtensions.CalculatePercentageChange(previousPayRunAmount, thisPayRunAmount),
                TotalSuppliers = supplierCount,
                TotalServiceUsers = serviceUserCount,
                HoldsCount = heldInvoiceCount,
                HoldsTotalAmount = holdsAmount
            };
        }

        public async Task<IEnumerable<InvoiceDomain>> GetAllInvoicesInPayRunUsingInvoiceStatus(Guid payRunId, int invoiceStatusId)
        {
            await CheckPayRunExists(payRunId).ConfigureAwait(false);

            // Get unique invoice ids
            /*var invoiceIds = await _dbContext.PayRunItems
                .Where(ii =>
                    ii.PayRunId.Equals(payRunId) && ii.Invoice.InvoiceStatusId.Equals(invoiceStatusId))
                .Select(ii => ii.InvoiceItem.InvoiceId)
                .Distinct().ToListAsync().ConfigureAwait(false);*/

            /*var invoices = await _dbContext.Invoices.Where(i => invoiceIds.Contains(i.InvoiceId))
                .Include(i => i.InvoiceItems)
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false);*/

            var invoices = await _dbContext.PayRunItems
                .Where(ii =>
                    ii.PayRunId.Equals(payRunId) && ii.Invoice.InvoiceStatusId.Equals(invoiceStatusId))
                .Include(ii => ii.Invoice)
                .ThenInclude(i => i.InvoiceItems)
                .Select(pr => pr.Invoice)
                .Distinct().ToListAsync().ConfigureAwait(false);

            return _mapper.Map<IEnumerable<InvoiceDomain>>(invoices);
        }

        public async Task<IEnumerable<PayRunItem>> GetAllItemsInPayRunUsingInvoiceStatus(Guid payRunId, int invoiceStatusId)
        {
            await CheckPayRunExists(payRunId).ConfigureAwait(false);

            var payRunItems = await _dbContext.PayRunItems
                .Where(ii =>
                    ii.PayRunId.Equals(payRunId) && ii.Invoice.InvoiceStatusId.Equals(invoiceStatusId))
                .AsNoTracking()
                .Distinct().ToListAsync().ConfigureAwait(false);

            return payRunItems;
        }

        public async Task<List<PayRunInvoicePaymentDomain>> GetPayRunInvoicePaymentDetails(Guid payRunId)
        {
            await CheckPayRunExists(payRunId).ConfigureAwait(false);

            var payRunItems = await _dbContext.InvoicePayments
                .Where(ip =>
                    ip.PayRunItem.PayRunId.Equals(payRunId))
                .Select(pi => new PayRunInvoicePaymentDomain
                {
                    InvoicePaymentId = pi.InvoicePaymentId,
                    PayRunItemId = pi.PayRunItemId,
                    PayRunId = pi.PayRunItem.PayRunId,
                    InvoiceId = pi.PayRunItem.InvoiceId,
                    InvoiceItemId = pi.PayRunItem.InvoiceItemId,
                    PaidAmount = pi.PaidAmount,
                    RemainingBalance = pi.RemainingBalance,
                    SupplierId = pi.PayRunItem.Invoice.SupplierId
                })
                .AsNoTracking()
                .Distinct().ToListAsync().ConfigureAwait(false);

            return payRunItems;
        }

        public async Task<bool> ApprovePayRunForPayment(Guid payRunId)
        {
            var payRun = await CheckPayRunExists(payRunId).ConfigureAwait(false);

            // Check if pay run is in submitted for approval stage. That is when it can be approved

            return payRun.PayRunStatusId switch
            {
                (int) PayRunStatusesEnum.Approved => throw new ApiException(
                    $"Pay run with id {payRunId} has already been approved"),
                (int) PayRunStatusesEnum.SubmittedForApproval => throw new ApiException(
                    $"Pay run with id {payRunId} has not been submitted for approval"),
                _ => await RunPayRunInvoicePayments(payRunId).ConfigureAwait(false)
            };
        }

        private async Task<bool> RunPayRunInvoicePayments(Guid payRunId)
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync().ConfigureAwait(false);
            try
            {
                var approvedInvoicesInPayRun =
                    await GetAllItemsInPayRunUsingInvoiceStatus(payRunId, (int) InvoiceStatusEnum.Accepted)
                        .ConfigureAwait(false);

                var invoicesList = approvedInvoicesInPayRun.ToList();

                if (invoicesList.Count.Equals(0))
                {
                    // Move pay run to approved and return
                    await ChangePayRunStatus(payRunId, (int) PayRunStatusesEnum.Approved).ConfigureAwait(false);
                    return true;
                }

                foreach (var payRunItem in invoicesList)
                {
                    payRunItem.PaidAmount = payRunItem.RemainingBalance;
                    payRunItem.RemainingBalance = new decimal(0.0);
                }

                var uploader = new NpgsqlBulkUploader(_dbContext);
                await uploader.UpdateAsync(invoicesList).ConfigureAwait(false);

                var invoicePayments = invoicesList.Select(p => new InvoicePayment
                {
                    PayRunItemId = p.PayRunItemId,
                    PaidAmount = p.PaidAmount,
                    RemainingBalance = p.RemainingBalance
                }).ToList();

                await uploader.InsertAsync(invoicePayments).ConfigureAwait(false);

                var ledgerItems = invoicePayments.Select(ip => new Ledger
                {
                    DateEntered = DateTimeOffset.Now,
                    MoneyIn = ip.PaidAmount,
                    InvoicePaymentId = ip.InvoicePaymentId,
                    MoneyOut = 0,
                    BillPaymentId = null
                }).ToList();

                await uploader.InsertAsync(ledgerItems).ConfigureAwait(false);

                // Get all the invoice payments made in this pay run
                var paidInvoiceDomains =
                    await GetPayRunInvoicePaymentDetails(payRunId)
                        .ConfigureAwait(false);

                // Use domains to create supplier bills
                var uniqueSuppliers = paidInvoiceDomains.Select(i => i.SupplierId).Distinct().ToList();

                var supplierBills = (from uniqueSupplier in uniqueSuppliers
                                     let supplierItems = paidInvoiceDomains.Where(i => i.SupplierId.Equals(uniqueSupplier)).ToList()
                                     let totalPaid = supplierItems.Sum(i => i.PaidAmount)
                                     select new PayRunSupplierBill
                                     {
                                         SupplierId = uniqueSupplier,
                                         TotalAmount = totalPaid,
                                         PayRunSupplierBillItems = supplierItems.Select(si => new PayRunSupplierBillItem
                                         {
                                             PayRunItemId = si.PayRunItemId,
                                             InvoicePaymentId = si.InvoicePaymentId,
                                             InvoiceId = si.InvoiceId,
                                             InvoiceItemId = si.InvoiceItemId,
                                             PaidAmount = si.PaidAmount
                                         })
                                             .ToList()
                                     }).ToList();

                await uploader.InsertAsync(supplierBills).ConfigureAwait(false);

                // Record supplier bills in ledger
                var supplierBillLedgerItems = supplierBills.Select(b => new Ledger
                {
                    DateEntered = DateTimeOffset.Now,
                    MoneyIn = 0,
                    InvoicePaymentId = null,
                    MoneyOut = b.TotalAmount,
                    BillPaymentId = null,
                    PayRunBillId = b.PayRunBillId
                });

                await uploader.InsertAsync(supplierBillLedgerItems).ConfigureAwait(false);

                // Change invoice status to paid
                var invoiceIds = invoicesList.Select(i => i.InvoiceId).Distinct().ToList();
                await _invoiceGateway.ChangeInvoiceListStatus(invoiceIds, (int) InvoiceStatusEnum.Paid).ConfigureAwait(false);

                // Move pay run status to approved
                await ChangePayRunStatus(payRunId, (int) PayRunStatusesEnum.Approved).ConfigureAwait(false);

                return true;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync().ConfigureAwait(false);
                throw new ApiException($"Error encountered in pay run approval: {e.InnerException?.Message}");
            }
        }
    }
}
