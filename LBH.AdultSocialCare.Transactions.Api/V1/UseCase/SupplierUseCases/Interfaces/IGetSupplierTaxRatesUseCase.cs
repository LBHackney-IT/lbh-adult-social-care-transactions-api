using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.SupplierBoundary.Response;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.SupplierUseCases.Interfaces
{
    public interface IGetSupplierTaxRatesUseCase
    {
        Task<IEnumerable<SupplierTaxRateResponse>> GetSupplierTaxRates(long supplierId);
    }
}
