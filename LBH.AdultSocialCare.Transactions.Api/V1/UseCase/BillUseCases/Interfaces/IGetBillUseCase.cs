using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.BillBoundary.Response;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.BillUseCases.Interfaces
{
    public interface IGetBillUseCase
    {
        Task<IEnumerable<BillResponse>> GetBill(Guid packageId, long supplierId, int billPaymentStatusId, DateTimeOffset? fromDate = null, DateTimeOffset? toDate = null);
    }
}
