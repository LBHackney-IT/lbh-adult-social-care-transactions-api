using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.PayRunModels;
using System;
using System.Linq;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Bills;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.RequestExtensions
{
    public static class GatewayQueryExtensions
    {
        public static IQueryable<PayRun> FilterPayRunSummaryList(this IQueryable<PayRun> payRuns, Guid? payRunId,
            int? payRunTypeId, int? payRunSubTypeId, int? payRunStatusId, DateTimeOffset? dateFrom,
            DateTimeOffset? dateTo) =>
            payRuns.Where(e => (
                    (payRunId == null || e.PayRunId.Equals(payRunId))
                    && (payRunTypeId == null || e.PayRunTypeId.Equals(payRunTypeId))
                    && (payRunSubTypeId == null || e.PayRunSubTypeId.Equals(payRunSubTypeId))
                    && (payRunStatusId == null || e.PayRunStatusId.Equals(payRunStatusId))
                    && (dateFrom == null || e.DateCreated >= dateFrom)
                    && (dateTo == null || e.DateCreated <= dateTo)
                )
            );

        public static IQueryable<Bill> FilterBillSummaryList(this IQueryable<Bill> bills, Guid? packageId, long? supplierId, int? billPaymentStatusId, DateTimeOffset? fromDate,
            DateTimeOffset? toDate) =>
            bills.Where(b =>
                (fromDate.Equals(null) || b.ServiceFromDate >= fromDate) &&
                (toDate.Equals(null) || b.ServiceToDate <= toDate) &&
                (packageId.Equals(null) || b.PackageId == packageId) &&
                (supplierId.Equals(null) || b.SupplierId == supplierId) &&
                (billPaymentStatusId.Equals(null) || b.BillPaymentStatusId == billPaymentStatusId));
    }
}
