using AutoMapper;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.BillsDomain;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.InvoicesDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.SupplierDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Bills;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Invoices;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Suppliers;
using System.Collections.Generic;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.LedgerDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.SupplierReturnDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.SupplierReturns;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Factories
{
    public static class EntityToDomainFactory
    {
        private static IMapper _mapper { get; set; }

        public static void Configure(IMapper mapper)
        {
            _mapper = mapper;
        }

        #region Bill

        public static BillDomain ToDomain(this Bill billEntity)
        {
            return _mapper.Map<BillDomain>(billEntity);
        }

        public static IEnumerable<BillDomain> ToDomain(this List<Bill> billEntity)
        {
            return _mapper.Map<IEnumerable<BillDomain>>(billEntity);
        }

        public static BillFileDomain ToDomain(this BillFile billFileEntity)
        {
            return _mapper.Map<BillFileDomain>(billFileEntity);
        }

        public static BillItemDomain ToDomain(this BillItem billItemEntity)
        {
            return _mapper.Map<BillItemDomain>(billItemEntity);
        }

        public static IEnumerable<BillStatusDomain> ToDomain(this List<BillStatus> billStatusEntity)
        {
            return _mapper.Map<IEnumerable<BillStatusDomain>>(billStatusEntity);
        }

        public static IEnumerable<BillItemDomain> ToDomain(this List<BillItem> billItemEntity)
        {
            return _mapper.Map<IEnumerable<BillItemDomain>>(billItemEntity);
        }
        public static BillPaymentDomain ToDomain(this BillPayment billPayment)
        {
            return _mapper.Map<BillPaymentDomain>(billPayment);
        }

        #endregion Bill

        #region Invoice

        public static IEnumerable<PendingInvoicesDomain> ToPendingInvoiceDomain(this List<Invoice> invoices)
        {
            return _mapper.Map<IEnumerable<PendingInvoicesDomain>>(invoices);
        }

        #endregion Invoice

        #region Supplier

        public static IEnumerable<SupplierDomain> ToDomain(this List<Supplier> supplierEntities)
        {
            return _mapper.Map<IEnumerable<SupplierDomain>>(supplierEntities);
        }

        public static IEnumerable<SupplierTaxRateDomain> ToDomain(this List<SupplierTaxRate> supplierTaxRatesEntities)
        {
            return _mapper.Map<IEnumerable<SupplierTaxRateDomain>>(supplierTaxRatesEntities);
        }

        public static SupplierTaxRateDomain ToDomain(this SupplierTaxRate supplierTaxRatesEntity)
        {
            return _mapper.Map<SupplierTaxRateDomain>(supplierTaxRatesEntity);
        }

        #endregion Supplier

        #region Invoices

        public static IEnumerable<InvoiceDomain> ToInvoiceDomain(this IEnumerable<Invoice> invoices)
        {
            return _mapper.Map<IEnumerable<InvoiceDomain>>(invoices);
        }

        public static InvoiceDomain ToInvoiceDomain(this Invoice invoice)
        {
            return _mapper.Map<InvoiceDomain>(invoice);
        }

        public static DisputedInvoiceFlatDomain ToDomain(this DisputedInvoice disputedInvoice)
        {
            return _mapper.Map<DisputedInvoiceFlatDomain>(disputedInvoice);
        }

        #endregion Invoices

        #region Ledger

        public static LedgerDomain ToDomain(this Ledger ledgerEntity)
        {
            return _mapper.Map<LedgerDomain>(ledgerEntity);
        }

        #endregion

        #region SupplierReturn

        public static IEnumerable<SupplierReturnItemDisputeConversationDomain> ToDomain
            (this List<SupplierReturnItemDisputeConversation> supplierReturnItemDisputeConversations)
        {
            return _mapper.Map<IEnumerable<SupplierReturnItemDisputeConversationDomain>>(supplierReturnItemDisputeConversations);
        }

        public static SupplierReturnDomain ToDomain(this SupplierReturn supplierReturn)
        {
            return _mapper.Map<SupplierReturnDomain>(supplierReturn);
        }

        #endregion SupplierReturn
    }
}
