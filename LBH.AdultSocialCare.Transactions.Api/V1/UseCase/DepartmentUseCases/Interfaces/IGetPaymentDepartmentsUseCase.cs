using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.DepartmentBoundaries.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.DepartmentUseCases.Interfaces
{
    public interface IGetPaymentDepartmentsUseCase
    {
        Task<IEnumerable<DepartmentResponse>> GetPaymentDepartments();
    }
}
