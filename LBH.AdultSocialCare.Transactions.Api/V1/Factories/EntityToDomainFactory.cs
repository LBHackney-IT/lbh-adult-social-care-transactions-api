using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.BillsDomain;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.InvoicesDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Bills;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Invoices;

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

        #endregion Bill

        #region Invoices

        public static IEnumerable<InvoiceDomain> ToDomain(this IEnumerable<Invoice> invoices)
        {
            return _mapper.Map<IEnumerable<InvoiceDomain>>(invoices);
        }

        public static DisputedInvoiceFlatDomain ToDomain(this DisputedInvoice disputedInvoice)
        {
            return _mapper.Map<DisputedInvoiceFlatDomain>(disputedInvoice);
        }

        #endregion
    }
}
