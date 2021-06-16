using System;
using System.Collections.Generic;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Domain.InvoicesDomains
{
    public class InvoiceForCreationDomain
    {
        public string InvoiceNumber { get; set; }
        public long SupplierId { get; set; }
        public int PackageTypeId { get; set; }
        public Guid ServiceUserId { get; set; }
        public DateTimeOffset DateInvoiced { get; set; }
        public decimal TotalAmount { get; set; }
        public float SupplierVATPercent { get; set; }
        public int InvoiceStatusId { get; set; }
        public Guid CreatorId { get; set; }
        public IEnumerable<InvoiceItemForCreationDomain> InvoiceItems { get; set; }
    }
}
