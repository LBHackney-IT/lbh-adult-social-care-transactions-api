using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Invoices
{
    public class InvoiceItemPaymentStatus // Service user payment status
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int StatusId { get; set; }
        public string StatusName { get; set; }
        public string DisplayName { get; set; } // Name displayed in FE
    }
}
