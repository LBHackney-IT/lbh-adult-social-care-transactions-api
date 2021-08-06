using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.SupplierReturns
{
    public class SuppliersReturns
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid SuppliersReturnsId { get; set; }
        public DateTimeOffset WeekCommencing { get; set; }
        public int TotalAmountWhenCreating { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public Guid CreatedById { get; set; }
        public int SupplierReturnsStatusesId { get; set; }
    }
}
