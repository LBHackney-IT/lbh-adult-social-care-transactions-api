using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Domain.InvoicesDomains
{
    public class PendingInvoicesDomain
    {
        public Guid InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public long SupplierId { get; set; }
        public int PackageTypeId { get; set; }
        public Guid ServiceUserId { get; set; }
        public DateTimeOffset DateInvoiced { get; set; }
        public decimal TotalAmount { get; set; } // Total amount when invoice is being created
        public float SupplierVATPercent { get; set; }
        public int InvoiceStatusId { get; set; }
        public IEnumerable<InvoiceItemDomain> InvoiceItems { get; set; }
    }
}
