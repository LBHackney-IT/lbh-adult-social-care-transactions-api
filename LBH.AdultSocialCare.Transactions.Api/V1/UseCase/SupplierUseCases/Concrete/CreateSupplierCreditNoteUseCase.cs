using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.SupplierDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.BillGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.SupplierGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.SupplierUseCases.Interfaces;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.SupplierUseCases.Concrete
{
    public class CreateSupplierCreditNoteUseCase : ICreateSupplierCreditNoteUseCase
    {
        private readonly ISupplierGateway _supplierGateway;
        private readonly IBillPaymentGateway _billPaymentGateway;

        public CreateSupplierCreditNoteUseCase(ISupplierGateway supplierGateway, IBillPaymentGateway billPaymentGateway)
        {
            _supplierGateway = supplierGateway;
            _billPaymentGateway = billPaymentGateway;
        }

        public async Task<long> CreateSupplierCreditNote(long billPaymentId)
        {
            var billPayments = _billPaymentGateway.GetBillPayment(billPaymentId);
            decimal amountOverPaid = 0;
            decimal amountRemaining = 0;

            switch (billPayments.Result.RemainingBalance < 0)
            {
                case true:
                    amountOverPaid = billPayments.Result.RemainingBalance * -1;
                    break;
                default:
                    amountRemaining = billPayments.Result.RemainingBalance;
                    break;
            }

            var newSupplierCreditNotesCreationDomain = new SupplierCreditNotesCreationDomain
            {
                AmountOverPaid = amountOverPaid,
                AmountRemaining = amountRemaining,
                BillPaymentFromId = billPaymentId,
                DatePaidForward = DateTimeOffset.Now
            };

            return await _supplierGateway.CreateCreditNotes(newSupplierCreditNotesCreationDomain.ToDb()).ConfigureAwait(false);
        }
    }
}
