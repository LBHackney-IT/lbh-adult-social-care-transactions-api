using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.PayRunModels
{
    public class PayRun : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] public Guid PayRunId { get; set; }
        public long PayRunNumber { get; set; }
        public int PayRunTypeId { get; set; }
        public int? PayRunSubTypeId { get; set; } // Release holds are added as sub types
        public int PayRunStatusId { get; set; }
        public DateTimeOffset DateFrom { get; set; }
        public DateTimeOffset DateTo { get; set; }
        public Guid CreatorId { get; set; }
        public Guid? UpdaterId { get; set; }
        [ForeignKey(nameof(PayRunTypeId))] public PayRunType PayRunType { get; set; }
        [ForeignKey(nameof(PayRunSubTypeId))] public PayRunSubType PayRunSubType { get; set; }
        [ForeignKey(nameof(PayRunStatusId))] public PayRunStatus PayRunStatus { get; set; }
        public virtual ICollection<PayRunItem> PayRunItems { get; set; }
    }
}
