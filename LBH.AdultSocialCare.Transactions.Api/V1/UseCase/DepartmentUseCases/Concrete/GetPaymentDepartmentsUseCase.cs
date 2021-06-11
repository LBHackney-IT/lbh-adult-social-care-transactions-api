using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.DepartmentBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.DepartmentGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.DepartmentUseCases.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.DepartmentUseCases.Concrete
{
    public class GetPaymentDepartmentsUseCase : IGetPaymentDepartmentsUseCase
    {
        private readonly IDepartmentGateway _departmentGateway;

        public GetPaymentDepartmentsUseCase(IDepartmentGateway departmentGateway)
        {
            _departmentGateway = departmentGateway;
        }

        public async Task<IEnumerable<DepartmentResponse>> GetPaymentDepartments()
        {
            var res = await _departmentGateway.GetPaymentDepartments().ConfigureAwait(false);
            return res.ToResponse();
        }
    }
}
