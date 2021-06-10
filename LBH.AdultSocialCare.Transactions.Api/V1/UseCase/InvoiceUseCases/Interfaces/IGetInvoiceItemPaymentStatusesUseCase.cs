using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.InvoiceUseCases.Interfaces
{
    public interface IGetInvoiceItemPaymentStatusesUseCase
    {
        Task<IEnumerable<InvoiceItemPaymentStatusResponse>> Execute();
    }
}
