using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.PayRunEntities
{
    public class PayRun : BaseEntity
    {
        public Guid PayRunId { get; set; }
        public long PayRunNumber { get; set; }
        public int PayRunTypeId { get; set; }
        public int PayRunStatusId { get; set; }
        public DateTimeOffset DateFrom { get; set; }
        public DateTimeOffset DateTo { get; set; }
        public Guid CreatorId { get; set; }
        public Guid? UpdaterId { get; set; }
        [ForeignKey(nameof(PayRunTypeId))] public PayRunType PayRunType { get; set; }
        [ForeignKey(nameof(PayRunStatusId))] public PayRunStatus PayRunStatus { get; set; }
    }
}
