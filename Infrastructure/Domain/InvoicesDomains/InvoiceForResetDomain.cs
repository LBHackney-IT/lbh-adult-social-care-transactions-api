using System;

namespace Infrastructure.Domain.InvoicesDomains
{
    public class InvoiceForResetDomain
    {
        public Guid InvoiceId { get; set; }
        public int PackageTypeId { get; set; }
        public Guid ServiceUserId { get; set; }
        public long SupplierId { get; set; }
        public Guid? PackageId { get; set; }
    }
}
