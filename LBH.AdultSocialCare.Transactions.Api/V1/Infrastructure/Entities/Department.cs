using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities
{
    public class Department
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }
    }
}
