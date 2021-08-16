using HttpServices.Models.Response;
using Infrastructure.Domain.InvoicesDomains;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HttpServices.Contracts
{
    public interface IAdultSocialCareApiService
    {
        // Reset invoice created up to date
        Task<GenericSuccessResponse> ResetInvoiceCreatedUpTo(IEnumerable<InvoiceForResetDomain> invoiceForResetDomains);
    }
}
