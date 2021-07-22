using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Invoices
{
    public class InvoiceStatus
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }

        [Required] public string StatusName { get; set; }
        [Required] public string DisplayName { get; set; }
        [Required] public bool ApprovalStatus { get; set; }
    }
}
