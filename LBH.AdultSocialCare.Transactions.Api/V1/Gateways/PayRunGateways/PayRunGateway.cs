using AutoMapper;
using LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.InvoicesDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.PackageTypeDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.PayRunDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.SupplierDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Exceptions.CustomExceptions;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.PayRunModels;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.RequestExtensions;
using Microsoft.EntityFrameworkCore;
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

        public PayRunGateway(DatabaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
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
                                pri.InvoiceItem.InvoiceItemPaymentStatusId.Equals(
                                    (int) InvoiceItemPaymentStatusEnum.Paid)).Sum(x => x.PaidAmount),
                    TotalAmountHeld =
                        pr.PayRunItems
                            .Where(pri =>
                                pri.InvoiceItem.InvoiceItemPaymentStatusId.Equals(
                                    (int) InvoiceItemPaymentStatusEnum.Held)).Sum(x => x.InvoiceItem.TotalPrice),
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
                    ri.InvoiceItem.Invoice.Supplier.SupplierId,
                    ri.InvoiceItem.Invoice.Supplier.SupplierName
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
                    pr.InvoiceItem.Invoice.PackageType.PackageTypeId,
                    pr.InvoiceItem.Invoice.PackageType.PackageTypeName
                }).Distinct()
                .Select(pr => new PackageTypeDomain
                {
                    PackageTypeId = pr.PackageTypeId,
                    PackageTypeName = pr.PackageTypeName
                })
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<InvoiceItemPaymentStatusDomain>> GetUniqueInvoiceItemPaymentStatusesInPayRun(Guid payRunId)
        {
            return await _dbContext.PayRunItems.Where(pr => pr.PayRunId.Equals(payRunId))
                .Select(pr => new
                {
                    pr.InvoiceItem.InvoiceItemPaymentStatus.StatusId,
                    pr.InvoiceItem.InvoiceItemPaymentStatus.StatusName,
                    pr.InvoiceItem.InvoiceItemPaymentStatus.DisplayName,
                }).Distinct()
                .Select(pr => new InvoiceItemPaymentStatusDomain
                {
                    StatusId = pr.StatusId,
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

        public async Task<bool> ReleaseHeldInvoiceItemPayment(Guid payRunId, Guid invoiceId, Guid invoiceItemId)
        {
            // Get the invoice item
            var invoiceItem = await _dbContext.PayRunItems
                .Where(pri =>
                    pri.PayRunId.Equals(payRunId) && pri.InvoiceItem.InvoiceId.Equals(invoiceId) &&
                    pri.InvoiceItemId.Equals(invoiceItemId))
                .Select(pri => pri.InvoiceItem)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);

            if (invoiceItem == null)
            {
                throw new EntityNotFoundException($"Invoice item with id {invoiceItemId} not found");
            }

            invoiceItem.InvoiceItemPaymentStatusId = (int) InvoiceItemPaymentStatusEnum.Released;

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
    }
}
