using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.BillBoundary.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.BillsDomain;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.BillUseCases.Interfaces
{
    public interface ICreateSupplierBillUseCase
    {
        public Task<BillResponse> ExecuteAsync(BillCreationDomain billCreationDomains);
    }
}
