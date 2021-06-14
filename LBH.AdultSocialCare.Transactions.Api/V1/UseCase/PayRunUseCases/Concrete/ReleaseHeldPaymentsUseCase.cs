using LBH.AdultSocialCare.Transactions.Api.V1.Domain.PayRunDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.PayRunGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Concrete
{
    public class ReleaseHeldPaymentsUseCase : IReleaseHeldPaymentsUseCase
    {
        private readonly IPayRunGateway _payRunGateway;

        public ReleaseHeldPaymentsUseCase(IPayRunGateway payRunGateway)
        {
            _payRunGateway = payRunGateway;
        }

        public async Task<bool> ReleaseHeldInvoiceItemPayment(ReleaseHeldInvoiceItemDomain releaseHeldInvoiceItemDomain)
        {
            return await _payRunGateway.ReleaseHeldInvoiceItemPayment(releaseHeldInvoiceItemDomain.PayRunId,
                    releaseHeldInvoiceItemDomain.InvoiceId, releaseHeldInvoiceItemDomain.InvoiceItemId)
                .ConfigureAwait(false);
        }

        public async Task<bool> ReleaseHeldInvoiceItemPaymentList(IEnumerable<ReleaseHeldInvoiceItemDomain> releaseHeldInvoiceItemDomains)
        {
            foreach (var heldInvoiceItemDomain in releaseHeldInvoiceItemDomains)
            {
                await ReleaseHeldInvoiceItemPayment(heldInvoiceItemDomain).ConfigureAwait(false);
            }

            return true;
        }
    }
}
