using AutoMapper;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.BillBoundary.Request;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.BillBoundary.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundary.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.SupplierBoundary.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.BillsDomain;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.InvoicesDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.PayRunDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.SupplierDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Bills;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Invoices;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.PayRunModels;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Suppliers;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Bill

            CreateMap<Bill, BillDomain>();
            CreateMap<BillDomain, Bill>();
            CreateMap<BillDomain, BillResponse>();
            CreateMap<BillCreationDomain, BillCreationRequest>();
            CreateMap<BillCreationRequest, BillCreationDomain>();
            CreateMap<BillCreationDomain, Bill>();
            CreateMap<Bill, BillCreationDomain>();
            CreateMap<BillStatus, BillStatusDomain>();
            CreateMap<BillStatusDomain, BillStatus>();
            CreateMap<BillStatusDomain, BillStatusResponse>();
            CreateMap<BillItemCreationDomain, BillItem>();
            CreateMap<BillItem, BillItemCreationDomain>();
            CreateMap<BillItemDomain, BillItemResponse>();
            CreateMap<BillItemCreationRequest, BillItemCreationDomain>();

            #endregion Bill

            #region Invoices

            CreateMap<Invoice, InvoiceDomain>();
            CreateMap<InvoiceItem, InvoiceItemMinimalDomain>();
            CreateMap<Invoice, PendingInvoicesDomain>();
            CreateMap<InvoiceItem, InvoiceItemDomain>();
            CreateMap<PendingInvoicesDomain, PendingInvoicesResponse>();
            CreateMap<InvoiceItemDomain, InvoiceItemResponse>();

            #endregion Invoices

            #region Payruns

            CreateMap<PayRunForCreationDomain, PayRun>();

            #endregion Invoices

            #region Supplier

            CreateMap<Supplier, SupplierDomain>();
            CreateMap<SupplierDomain, SupplierResponse>();

            #endregion

            CreateMap<PackageType, PackageTypeDomain>();
        }
    }
}
