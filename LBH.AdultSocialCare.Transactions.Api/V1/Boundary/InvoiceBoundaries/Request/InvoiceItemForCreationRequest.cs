using System;
using System.ComponentModel.DataAnnotations;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Request
{
    public class InvoiceItemForCreationRequest
    {
        [Required] public string ItemName { get; set; }
        [Required] public decimal? PricePerUnit { get; set; }
        [Required] public decimal? Quantity { get; set; }
        public Guid? SupplierReturnItemId { get; set; }
        [Required] public Guid? CreatorId { get; set; }
    }
}
