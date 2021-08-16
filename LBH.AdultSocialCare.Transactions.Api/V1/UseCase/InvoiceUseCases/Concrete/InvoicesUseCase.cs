using LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.InvoicesDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Exceptions.CustomExceptions;
using LBH.AdultSocialCare.Transactions.Api.V1.Extensions;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.InvoiceGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.PackageTypeGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.PayRunGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.SupplierGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.RequestExtensions;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.InvoiceUseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.InvoiceUseCases.Concrete
{
    public class InvoicesUseCase : IInvoicesUseCase
    {
        private readonly IInvoiceGateway _invoiceGateway;
        private readonly ISupplierGateway _supplierGateway;
        private readonly IPackageTypeGateway _packageTypeGateway;
        private readonly IPayRunGateway _payRunGateway;

        public InvoicesUseCase(IInvoiceGateway invoiceGateway, ISupplierGateway supplierGateway, IPackageTypeGateway packageTypeGateway, IPayRunGateway payRunGateway)
        {
            _invoiceGateway = invoiceGateway;
            _supplierGateway = supplierGateway;
            _packageTypeGateway = packageTypeGateway;
            _payRunGateway = payRunGateway;
        }

        public async Task<DisputedInvoiceFlatResponse> HoldInvoicePaymentUseCase(Guid payRunId, Guid invoiceId, DisputedInvoiceForCreationDomain disputedInvoiceForCreationDomain)
        {
            // Check pay run exists and has correct status
            var payRun = await _payRunGateway.CheckPayRunExists(payRunId).ConfigureAwait(false);

            switch (payRun.PayRunStatusId)
            {
                case (int) PayRunStatusesEnum.SubmittedForApproval:
                    throw new ApiException($"Pay run with id {payRunId} has already been submitted for approval");
                case (int) PayRunStatusesEnum.Approved:
                    throw new ApiException($"Pay run with id {payRunId} has already been approved. Invoice status cannot be changed");
            }

            // Get pay run item with invoice id
            var payRunItem = await _payRunGateway.GetPayRunItemUsingInvoiceId(payRunId, invoiceId).ConfigureAwait(false);

            // Get invoice and check has correct status
            var invoice = await _payRunGateway.GetSingleInvoiceInPayRun(payRunId, payRunItem.InvoiceId).ConfigureAwait(false);

            switch (invoice.InvoiceStatusId)
            {
                case (int) InvoiceStatusEnum.Held:
                    throw new ApiException(
                        $"Invoice with id {payRunId} has already been held");
                case (int) InvoiceStatusEnum.Released:
                    throw new ApiException($"Released invoice with id {payRunId} will be added to the next pay run");
            }

            disputedInvoiceForCreationDomain.InvoiceId = payRunItem.InvoiceId;
            disputedInvoiceForCreationDomain.InvoiceItemId = payRunItem.InvoiceItemId;
            disputedInvoiceForCreationDomain.PayRunItemId = payRunItem.PayRunItemId;
            var res = await _invoiceGateway.CreateDisputedInvoice(disputedInvoiceForCreationDomain.ToDb()).ConfigureAwait(false);
            return res.ToResponse();
        }

        public async Task<DisputedInvoiceChatResponse> CreateDisputedInvoiceChatUseCase(
            DisputedInvoiceChatForCreationDomain disputedInvoiceChatForCreationDomain)
        {
            // Check disputed invoice exists
            var payRun = await _payRunGateway.CheckDisputedInvoiceExists(disputedInvoiceChatForCreationDomain.PayRunId,
                disputedInvoiceChatForCreationDomain.PayRunItemId).ConfigureAwait(false);

            // Create disputed invoice chat
            var res = await _payRunGateway
                .CreateDisputedInvoiceChat(disputedInvoiceChatForCreationDomain.ToDb(payRun.DisputedInvoiceId))
                .ConfigureAwait(false);

            return res?.ToResponse();
        }

        public async Task<InvoiceResponse> CreateInvoiceUseCase(InvoiceForCreationDomain invoiceForCreationDomain)
        {
            await CalculateInvoicePrice(invoiceForCreationDomain).ConfigureAwait(false);

            var res = await _invoiceGateway.CreateInvoice(invoiceForCreationDomain.ToDb()).ConfigureAwait(false);

            return res.ToResponse();
        }

        public async Task<IEnumerable<InvoiceResponse>> BatchCreateInvoicesUseCase(IEnumerable<InvoiceForCreationDomain> invoicesForCreationDomain)
        {
            if (invoicesForCreationDomain.Any(i => i.InvoiceItems.IsNullOrEmpty<InvoiceItemForCreationDomain>()))
            {
                throw new ApiException("Invoice cannot be created without invoice items", (int) HttpStatusCode.UnprocessableEntity);
            }

            foreach (var invoice in invoicesForCreationDomain)
            {
                await CalculateInvoicePrice(invoice).ConfigureAwait(false);
            }

            var invoiceResponses = await _invoiceGateway.BatchCreateInvoices(invoicesForCreationDomain.ToDb()).ConfigureAwait(false);
            return invoiceResponses.ToResponse();
        }

        public async Task<IEnumerable<InvoiceResponse>> GetInvoicesFlatInPayRunUseCase(Guid payRunId, InvoiceListParameters parameters)
        {
            var res = await _invoiceGateway.GetInvoicesFlatInPayRunAsync(payRunId, parameters).ConfigureAwait(false);
            return res.ToResponse();
        }

        private async Task CalculateInvoicePrice(InvoiceForCreationDomain invoiceForCreationDomain)
        {
            // Check if package type is valid
            _packageTypeGateway.IsValidPackageType(invoiceForCreationDomain.PackageTypeId);

            var supplier = await _supplierGateway.CheckSupplierExists(invoiceForCreationDomain.SupplierId).ConfigureAwait(false);
            var supplierTaxRate = await _supplierGateway.GetLatestSupplierTaxRate(supplier.SupplierId).ConfigureAwait(false);

            invoiceForCreationDomain.SupplierVATPercent = 0;
            if (supplierTaxRate != null)
            {
                invoiceForCreationDomain.SupplierVATPercent = supplierTaxRate.VATPercentage;
            }

            invoiceForCreationDomain.TotalAmount = new decimal(0.0);
            foreach (var invoiceItem in invoiceForCreationDomain.InvoiceItems)
            {
                invoiceItem.SubTotal = invoiceItem.PricePerUnit * invoiceItem.Quantity;
                invoiceItem.VatAmount = invoiceItem.SubTotal *
                                        (decimal) invoiceForCreationDomain.SupplierVATPercent;
                invoiceItem.TotalPrice = invoiceItem.SubTotal + invoiceItem.VatAmount;
                invoiceForCreationDomain.TotalAmount += invoiceItem.TotalPrice;
            }
        }
    }
}
