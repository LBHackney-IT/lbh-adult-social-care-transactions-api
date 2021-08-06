using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Invoices
{
    public class DisputedInvoiceChat : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] public Guid DisputedInvoiceChatId { get; set; }
        public Guid DisputedInvoiceId { get; set; }
        public bool MessageRead { get; set; }
        public string Message { get; set; }
        public int? MessageFromId { get; set; }
        public int ActionRequiredFromId { get; set; }

        [ForeignKey(nameof(DisputedInvoiceId))] public virtual DisputedInvoice DisputedInvoice { get; set; }
        [ForeignKey(nameof(MessageFromId))] public virtual Department MessageFromDepartment { get; set; }
        [ForeignKey(nameof(ActionRequiredFromId))] public virtual Department ActionRequiredFromDepartment { get; set; }
    }
}
