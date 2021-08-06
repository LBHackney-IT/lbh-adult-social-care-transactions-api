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
        private readonly IBillGateway _billGateway;

        public CreateSupplierCreditNoteUseCase(ISupplierGateway supplierGateway,
            IBillPaymentGateway billPaymentGateway,
            IBillGateway billGateway)
        {
            _supplierGateway = supplierGateway;
            _billPaymentGateway = billPaymentGateway;
            _billGateway = billGateway;
        }

        public async Task CreateSupplierCreditNote(long billId)
        {
            var bill = await _billGateway.GetBillAsync(billId).ConfigureAwait(false);
            var billPayment = await _billPaymentGateway.GetBillPaymentByBillId(billId).ConfigureAwait(false);
            decimal amountOverPaid = 0;
            decimal amountRemaining = 0;
            var billBalance = bill.TotalBilled - billPayment.PaidAmount;

            if (billBalance > 0)
                amountRemaining = billBalance;
            else if (billBalance < 0) amountOverPaid = billBalance * -1;
            else
                return;

            var newSupplierCreditNotesCreationDomain = new SupplierCreditNotesCreationDomain
            {
                AmountOverPaid = amountOverPaid,
                AmountRemaining = amountRemaining,
                BillPaymentFromId = billPayment.BillPaymentId,
                DatePaidForward = DateTimeOffset.Now
            };

            await _supplierGateway.CreateCreditNotes(newSupplierCreditNotesCreationDomain.ToDb()).ConfigureAwait(false);
        }
    }
}
