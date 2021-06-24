using LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Response;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.InvoiceUseCases.Interfaces
{
    public interface IInvoiceStatusUseCase
    {
        Task<IEnumerable<InvoiceStatusResponse>> GetAllInvoiceStatusesUseCase();

        Task<IEnumerable<InvoiceStatusResponse>> GetInvoicePaymentStatusesUseCase();
    }
}
