using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.BillsDomain;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.LedgerDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.BillGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.LedgerGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.BillUseCases.Interfaces;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.SupplierUseCases.Interfaces;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.BillUseCases.Concrete
{
    public class PaySupplierBillUseCase : IPaySupplierBillUseCase
    {
        private readonly IBillGateway _billGateway;
        private readonly IBillPaymentGateway _billPaymentGateway;
        private readonly ILedgerGateway _ledgerGateway;
        private readonly IBillStatusGateway _billStatusGateway;
        private readonly ICreateSupplierCreditNoteUseCase _createSupplierCreditNoteUseCase;

        public PaySupplierBillUseCase(IBillGateway billGateway,
            IBillPaymentGateway billPaymentGateway,
            ILedgerGateway ledgerGateway,
            IBillStatusGateway billStatusGateway,
            ICreateSupplierCreditNoteUseCase createSupplierCreditNoteUseCase)
        {
            _billGateway = billGateway;
            _billPaymentGateway = billPaymentGateway;
            _ledgerGateway = ledgerGateway;
            _billStatusGateway = billStatusGateway;
            _createSupplierCreditNoteUseCase = createSupplierCreditNoteUseCase;
        }

        public async Task<bool> PaySupplierBill(IEnumerable<long> billIds)
        {
            foreach (var billId in billIds)
            {
                //get bill item
                var bill = await _billGateway.GetBillAsync(billId).ConfigureAwait(false);

                //create bill payment
                //Todo if user wants to pay a part of bill?
                var newBillPayment = new BillPaymentDomain
                {
                    BillId = billId,
                    PaidAmount = bill.TotalBilled,
                    RemainingBalance = 0
                };

                var billPaymentEntity = newBillPayment.ToDb();
                var billPaymentId = await _billPaymentGateway.CreateBillPayment(billPaymentEntity).ConfigureAwait(false);
                var billPayment = await _billPaymentGateway.GetBillPayment(billPaymentId).ConfigureAwait(false);

                //record ledger as money out
                var newLedger = new LedgerDomain()
                {
                    DateEntered = DateTimeOffset.Now,
                    MoneyOut = billPayment.PaidAmount,
                    BillPaymentId = billPaymentId
                };
                var ledgerEntity = newLedger.ToDb();
                await _ledgerGateway.CreateLedger(ledgerEntity).ConfigureAwait(false);

                //change status of bill as paid
                await _billStatusGateway.CheckAndSetBillStatus(billId).ConfigureAwait(false);

                //create credit note if balance not equal
                await _createSupplierCreditNoteUseCase.CreateSupplierCreditNote(billId).ConfigureAwait(false);
            }

            return true;
        }
    }
}
