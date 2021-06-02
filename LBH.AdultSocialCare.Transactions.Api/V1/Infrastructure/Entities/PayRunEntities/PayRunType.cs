using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.PayRunEntities
{
    public class PayRunType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int PayRunTypeId { get; set; }
        public string TypeName { get; set; }
    }
}
