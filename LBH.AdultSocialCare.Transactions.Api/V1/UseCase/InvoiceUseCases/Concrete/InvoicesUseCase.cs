using LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.InvoicesDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Exceptions.CustomExceptions;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.InvoiceGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.PackageTypeGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.SupplierGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.InvoiceUseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.InvoiceUseCases.Concrete
{
    public class InvoicesUseCase : IInvoicesUseCase
    {
        private readonly IInvoiceGateway _invoiceGateway;
        private readonly ISupplierGateway _supplierGateway;
        private readonly IPackageTypeGateway _packageTypeGateway;

        public InvoicesUseCase(IInvoiceGateway invoiceGateway, ISupplierGateway supplierGateway, IPackageTypeGateway packageTypeGateway)
        {
            _invoiceGateway = invoiceGateway;
            _supplierGateway = supplierGateway;
            _packageTypeGateway = packageTypeGateway;
        }

        public async Task<DisputedInvoiceFlatResponse> HoldInvoicePaymentUseCase(DisputedInvoiceForCreationDomain disputedInvoiceForCreationDomain)
        {
            var res = await _invoiceGateway.CreateDisputedInvoice(disputedInvoiceForCreationDomain.ToDb()).ConfigureAwait(false);
            return res.ToResponse();
        }

        public async Task<bool> ChangeInvoiceStatusUseCase(Guid invoiceId, int invoiceStatusId)
        {
            if (invoiceStatusId == (int) InvoiceStatusEnum.Held || invoiceStatusId == (int) InvoiceStatusEnum.Released)
            {
                throw new ApiException("Update action not allowed");
            }

            return await _invoiceGateway.ChangeInvoiceStatus(invoiceId, invoiceStatusId).ConfigureAwait(false);
        }

        public async Task<bool> ReleaseSingleInvoiceUseCase(Guid invoiceId)
        {
            return await _invoiceGateway.ChangeInvoiceStatus(invoiceId, (int) InvoiceStatusEnum.Released).ConfigureAwait(false);
        }

        public async Task<bool> ReleaseMultipleInvoicesUseCase(IEnumerable<Guid> invoiceIds)
        {
            foreach (var invoiceId in invoiceIds)
            {
                await ReleaseSingleInvoiceUseCase(invoiceId).ConfigureAwait(false);
            }

            return true;
        }

        public async Task<bool> ChangeInvoiceItemPaymentStatusUseCase(Guid payRunId, Guid invoiceItemId, int invoiceItemPaymentStatusId)
        {
            if (invoiceItemPaymentStatusId == (int) InvoiceItemPaymentStatusEnum.Held)
            {
                throw new ApiException("Update action not allowed");
            }

            return await _invoiceGateway
                .ChangeInvoiceItemPaymentStatus(payRunId, invoiceItemId, invoiceItemPaymentStatusId)
                .ConfigureAwait(false);
        }

        public async Task<InvoiceResponse> CreateInvoiceUseCase(InvoiceForCreationDomain invoiceForCreationDomain)
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

            var res = await _invoiceGateway.CreateInvoice(invoiceForCreationDomain.ToDb()).ConfigureAwait(false);

            return res.ToResponse();
        }
    }
}
