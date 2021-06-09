using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.BillBoundary.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.BillsDomain;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.BillGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.BillUseCases.Interfaces;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.BillUseCases.Concrete
{
    public class CreateSupplierBillUseCase : ICreateSupplierBillUseCase
    {
        private readonly IBillGateway _billGateway;

        public CreateSupplierBillUseCase(IBillGateway billGateway)
        {
            _billGateway = billGateway;
        }

        public async Task<BillResponse> ExecuteAsync(BillCreationDomain billCreationDomains)
        {
            var billEntity = billCreationDomains.ToDb();
            var id = await _billGateway.CreateBillAsync(billEntity).ConfigureAwait(false);
            var bill = await _billGateway.GetBillAsync(id).ConfigureAwait(false);
            return bill.ToResponse();
        }
    }
}
