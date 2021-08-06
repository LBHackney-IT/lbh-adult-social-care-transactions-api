using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Invoices
{
    public class InvoiceNumber
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int InvoiceNumberId { get; set; }
        public string Prefix { get; set; }
        public long CurrentInvoiceNumber { get; set; }
        public string PostFix { get; set; }
    }
}
