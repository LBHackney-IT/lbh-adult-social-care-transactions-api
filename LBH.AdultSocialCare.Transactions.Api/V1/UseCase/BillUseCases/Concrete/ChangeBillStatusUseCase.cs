using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.BillGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.BillUseCases.Interfaces;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.BillUseCases.Concrete
{
    public class ChangeBillStatusUseCase : IChangeBillStatusUseCase
    {
        private readonly IBillStatusGateway _billStatusGateway;

        public ChangeBillStatusUseCase(IBillStatusGateway billStatusGateway)
        {
            _billStatusGateway = billStatusGateway;
        }

        public async Task CheckAndSetBillStatus(long billId)
        {
            await _billStatusGateway.CheckAndSetBillStatus(billId).ConfigureAwait(false);
        }
    }
}
