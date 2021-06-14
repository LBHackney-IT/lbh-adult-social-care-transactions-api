using System.Collections.Generic;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.PayRunDomains;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Interfaces
{
    public interface IReleaseHeldPaymentsUseCase
    {
        Task<bool> ReleaseHeldInvoiceItemPayment(ReleaseHeldInvoiceItemDomain releaseHeldInvoiceItemDomain);

        Task<bool> ReleaseHeldInvoiceItemPaymentList(IEnumerable<ReleaseHeldInvoiceItemDomain> releaseHeldInvoiceItemDomains);
    }
}
