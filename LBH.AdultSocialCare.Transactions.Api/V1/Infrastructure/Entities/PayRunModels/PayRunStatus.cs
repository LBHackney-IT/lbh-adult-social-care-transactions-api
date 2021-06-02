using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.PayRunModels
{
    public class PayRunStatus
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int PayRunStatusId { get; set; }
        public string StatusName { get; set; }
    }
}
