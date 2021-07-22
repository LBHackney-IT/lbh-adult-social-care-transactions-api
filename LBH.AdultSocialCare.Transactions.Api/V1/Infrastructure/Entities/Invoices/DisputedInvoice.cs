using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.PayRunModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Invoices
{
    public class DisputedInvoice
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] public Guid DisputedInvoiceId { get; set; }
        public Guid PayRunItemId { get; set; } // The specific pay run item this invoice is in
        public Guid InvoiceId { get; set; } // Disputing whole invoice
        public Guid? InvoiceItemId { get; set; } // Dispute single item in invoice
        public int ActionRequiredFromId { get; set; }
        public string ReasonForHolding { get; set; }

        [ForeignKey(nameof(ActionRequiredFromId))] public virtual Department ActionRequiredFromDepartment { get; set; }
        [ForeignKey(nameof(InvoiceId))] public virtual Invoice Invoice { get; set; }
        [ForeignKey(nameof(InvoiceItemId))] public virtual InvoiceItem InvoiceItem { get; set; }
        [ForeignKey(nameof(PayRunItemId))] public virtual PayRunItem PayRunItem { get; set; }
        public virtual ICollection<DisputedInvoiceChat> DisputedInvoiceChats { get; set; }
    }
}
