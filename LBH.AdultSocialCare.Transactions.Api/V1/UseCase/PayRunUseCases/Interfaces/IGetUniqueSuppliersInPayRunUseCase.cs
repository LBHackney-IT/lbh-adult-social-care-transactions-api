using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.SupplierBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.RequestExtensions;
using System;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Interfaces
{
    public interface IGetUniqueSuppliersInPayRunUseCase
    {
        Task<PagedSupplierMinimalListResponse> Execute(Guid payRunId, SupplierListParameters parameters);
    }
}
