using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.InvoicesDomains;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.InvoiceUseCases.Interfaces
{
    public interface IInvoicesUseCase
    {
        Task<DisputedInvoiceFlatResponse> HoldInvoicePaymentUseCase(
            DisputedInvoiceForCreationDomain disputedInvoiceForCreationDomain);
    }
}
