using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.PayRunModels
{
    public class PayRunSupplierBill : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] public Guid PayRunBillId { get; set; }
        [Required] public long SupplierId { get; set; }
        [Required] public decimal TotalAmount { get; set; }

        public virtual ICollection<PayRunSupplierBillItem> PayRunSupplierBillItems { get; set; }
    }
}
