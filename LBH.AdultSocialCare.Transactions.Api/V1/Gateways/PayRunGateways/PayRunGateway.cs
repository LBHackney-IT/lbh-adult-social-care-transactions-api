using AutoMapper;
using LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.PayRunDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.SupplierDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Exceptions.CustomExceptions;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.PayRunModels;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.RequestExtensions;
using Microsoft.EntityFrameworkCore;
using System;
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
    }
}
