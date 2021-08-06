using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.SupplierBoundary.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.RequestExtensions;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.SupplierUseCases.Interfaces
{
    public interface IGetSuppliersUseCase
    {
        Task<PagedSupplierResponse> GetSupplierUseCase(SupplierListParameters parameters);
    }
}
