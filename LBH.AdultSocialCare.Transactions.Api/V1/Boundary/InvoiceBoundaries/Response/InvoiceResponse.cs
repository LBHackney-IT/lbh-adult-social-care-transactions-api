using System;
using System.Collections.Generic;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Response
{
    public class InvoiceResponse
    {
        public Guid InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public long SupplierId { get; set; }
        public int PackageTypeId { get; set; }
        public string PackageTypeName { get; set; }
        public Guid ServiceUserId { get; set; }
        public DateTimeOffset DateInvoiced { get; set; }
        public decimal TotalAmount { get; set; } // Total amount when invoice is being created
        public float SupplierVATPercent { get; set; }
        public int InvoiceStatusId { get; set; }
        public Guid CreatorId { get; set; }
        public Guid? UpdaterId { get; set; }
        public IEnumerable<InvoiceItemMinimalResponse> InvoiceItems { get; set; }
        public IEnumerable<DisputedInvoiceChatResponse> DisputedInvoiceChat { get; set; }
    }
}
