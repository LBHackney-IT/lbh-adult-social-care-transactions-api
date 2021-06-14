using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.PayRunModels;
using System;
using System.Linq;

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
    }
}
