using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.BillsDomain;
using LBH.AdultSocialCare.Transactions.Api.V1.Exceptions;
using LBH.AdultSocialCare.Transactions.Api.V1.Exceptions.CustomExceptions;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Bills;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.RequestExtensions;
using Microsoft.EntityFrameworkCore;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Gateways.BillGateways
{
    public class BillGateway : IBillGateway
    {
        private readonly DatabaseContext _databaseContext;

        public BillGateway(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<long> CreateBillAsync(Bill bill)
        {
            bill.BillPaymentStatusId = (int) BillStatusEnum.OutstandingId;
            if (bill.BillDueDate.Date < DateTimeOffset.Now)
            {
                bill.BillPaymentStatusId = (int) BillStatusEnum.OverdueId;
            }
            var entry = await _databaseContext.Bills.AddAsync(bill)
                .ConfigureAwait(false);
            try
            {
                await _databaseContext.SaveChangesAsync().ConfigureAwait(false);
                entry.Entity.BillStatus = await _databaseContext.BillStatuses
                    .FirstOrDefaultAsync(item => item.Id == entry.Entity.BillPaymentStatusId)
                    .ConfigureAwait(false);
                return entry.Entity.BillId;
            }
            catch (Exception ex)
            {
                throw new DbSaveFailedException("Could not save bill to database" + ex.Message);
            }
        }

        public async Task<BillDomain> GetBillAsync(long billId)
        {
            var bill = await _databaseContext.Bills
                .Where(b => b.BillId.Equals(billId))
                .AsNoTracking()
                .Include(b => b.BillStatus)
                .Include(b => b.BillItems)
                .SingleOrDefaultAsync().ConfigureAwait(false);

            if (bill == null)
            {
                throw new EntityNotFoundException($"Unable to locate bill {billId.ToString()}");
            }

            return bill.ToDomain();
        }

        public async Task<IEnumerable<BillDomain>> GetBillList()
        {
            var bill = await _databaseContext.Bills
                .Include(dc => dc.BillStatus)
                .AsNoTracking()
                .ToListAsync().ConfigureAwait(false);
            return bill?.ToDomain();
        }

        public async Task<PagedList<BillSummaryDomain>> GetBill(BillSummaryListParameters parameters)
        {
            var billList = await _databaseContext.Bills
                .FilterBillSummaryList(parameters.PackageId, parameters.SupplierId, parameters.BillPaymentStatusId,
                    parameters.FromDate, parameters.ToDate)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .Include(b => b.Supplier)
                .Include(b => b.BillStatus)
                .Select(bs => new BillSummaryDomain()
                {
                    BillId = bs.BillId,
                    SupplierRef = bs.SupplierRef,
                    PackageId = bs.PackageId,
                    SupplierName = bs.Supplier.SupplierName,
                    ServiceFromDate = bs.ServiceFromDate,
                    ServiceToDate = bs.ServiceToDate,
                    DateBilled = bs.DateBilled,
                    BillDueDate = bs.BillDueDate,
                    TotalBilled = bs.TotalBilled,
                    PaidAmount = _databaseContext.BillPayments
                        .Where(bp => bp.BillId.Equals(bs.BillId))
                        .Sum(x => x.PaidAmount),
                    StatusName = bs.BillStatus.StatusName
                })
                .ToListAsync().ConfigureAwait(false);

            var billCount = await _databaseContext.Bills
                .FilterBillSummaryList(parameters.PackageId, parameters.SupplierId, parameters.BillPaymentStatusId,
                    parameters.FromDate, parameters.ToDate)
                .CountAsync().ConfigureAwait(false);

            return PagedList<BillSummaryDomain>.ToPagedList(billList, billCount, parameters.PageNumber, parameters.PageSize);
        }
    }
}
