using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.BillBoundary.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.RequestExtensions;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.BillUseCases.Interfaces
{
    public interface IGetBillUseCase
    {
        Task<PagedBillSummaryResponse> GetBill(BillSummaryListParameters parameters);
    }
}
