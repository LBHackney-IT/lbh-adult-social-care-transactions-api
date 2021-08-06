using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.PayRunModels
{
    public class PayRunSubType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int PayRunSubTypeId { get; set; }
        public string SubTypeName { get; set; }
        public int PayRunTypeId { get; set; }

        [ForeignKey(nameof(PayRunTypeId))] public virtual PayRunType PayRunType { get; set; }
    }
}
