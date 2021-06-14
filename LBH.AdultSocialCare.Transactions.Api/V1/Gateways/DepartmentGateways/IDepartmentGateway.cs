using LBH.AdultSocialCare.Transactions.Api.V1.Domain.DepartmentDomains;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Gateways.DepartmentGateways
{
    public interface IDepartmentGateway
    {
        Task<IEnumerable<DepartmentDomain>> GetPaymentDepartments();
    }
}
