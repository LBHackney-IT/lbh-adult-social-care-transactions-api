using System;
using System.Collections.Generic;

namespace Infrastructure.Domain.InvoicesDomains
{
    public class InvoiceForCreationDomain
    {
        public long SupplierId { get; set; }
        public int PackageTypeId { get; set; }
        public Guid ServiceUserId { get; set; }
        public DateTimeOffset DateInvoiced { get; set; }
        public decimal TotalAmount { get; set; }
        public float SupplierVATPercent { get; set; }
        public int InvoiceStatusId { get; set; }
        public Guid CreatorId { get; set; }
        public DateTimeOffset DateFrom { get; set; }
        public DateTimeOffset DateTo { get; set; }
        public Guid? PackageId { get; set; }
        public IEnumerable<InvoiceItemForCreationDomain> InvoiceItems { get; set; }
    }
}
