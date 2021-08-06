using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundary.Response;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.InvoiceUseCases.Interfaces
{
    public interface IGetUserPendingInvoicesUseCase
    {
        Task<IEnumerable<PendingInvoicesResponse>> GetUserPendingInvoices(Guid serviceUserId);
    }
}
