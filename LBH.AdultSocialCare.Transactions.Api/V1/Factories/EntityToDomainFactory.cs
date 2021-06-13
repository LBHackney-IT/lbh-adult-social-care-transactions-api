using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.BillsDomain;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.InvoicesDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.SupplierDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Bills;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Invoices;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Suppliers;

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

        public static IEnumerable<PendingInvoicesDomain> ToDomain(this List<Invoice> invoices)
        {
            return _mapper.Map<IEnumerable<PendingInvoicesDomain>>(invoices);
        }

        #endregion

        #region Supplier

        public static IEnumerable<SupplierDomain> ToDomain(this List<Supplier> supplierEntities)
        {
            return _mapper.Map<IEnumerable<SupplierDomain>>(supplierEntities);
        }

        public static IEnumerable<SupplierTaxRateDomain> ToDomain(this List<SupplierTaxRate> supplierTaxRatesEntities)
        {
            return _mapper.Map<IEnumerable<SupplierTaxRateDomain>>(supplierTaxRatesEntities);
        }

        #endregion
    }
}
