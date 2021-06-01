using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Invoices
{
    public class Invoice : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid InvoiceId { get; set; }

        public int InvoiceStatusId { get; set; }

        public Guid PayRunId { get; set; }

        public Guid InvoiceNumber { get; set; }

        public int PackageTypeId { get; set; }

        public int SupplierId { get; set; }

        public Guid ServiceUserId { get; set; }

        public decimal TotalAmount { get; set; }

        public int UserId { get; set; }

        [ForeignKey(nameof(InvoiceStatusId))]
        public InvoiceStatus InvoiceStatus { get; set; }

        [ForeignKey(nameof(PackageTypeId))]
        public PackageType PackageType { get; set; }

        public Guid CreatorId { get; set; }

        public Guid? UpdaterId { get; set; }

    }
}
