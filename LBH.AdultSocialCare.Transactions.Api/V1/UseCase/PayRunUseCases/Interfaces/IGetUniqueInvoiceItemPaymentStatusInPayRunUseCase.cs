using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Interfaces
{
    public interface IGetUniqueInvoiceItemPaymentStatusInPayRunUseCase
    {
        Task<IEnumerable<InvoiceStatusResponse>> Execute(Guid payRunId);
    }
}
