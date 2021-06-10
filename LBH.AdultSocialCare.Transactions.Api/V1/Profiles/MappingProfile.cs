using AutoMapper;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.BillBoundary.Request;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.BillBoundary.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.PackageTypeBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.PayRunBoundaries.Request;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.PayRunBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.SupplierBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.BillsDomain;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.InvoicesDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.PackageTypeDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.PayRunDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.SupplierDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Bills;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Invoices;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.PayRunModels;

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
            CreateMap<BillResponse, BillDomain>();
            CreateMap<BillCreationDomain, BillCreationRequest>();
            CreateMap<BillCreationRequest, BillCreationDomain>();
            CreateMap<BillCreationDomain, Bill>();
            CreateMap<Bill, BillCreationDomain>();
            CreateMap<BillStatus, BillStatusDomain>();
            CreateMap<BillStatusDomain, BillStatus>();
            CreateMap<BillStatusDomain, BillStatusResponse>();

            #endregion Bill

            #region Invoices

            CreateMap<Invoice, InvoiceDomain>();
            CreateMap<InvoiceItem, InvoiceItemMinimalDomain>();
            CreateMap<InvoiceItemMinimalDomain, InvoiceItemMinimalResponse>();
            CreateMap<InvoiceItemPaymentStatusDomain, InvoiceItemPaymentStatusResponse>();
            CreateMap<InvoiceDomain, InvoiceResponse>();

            #endregion Invoices

            #region Payruns

            CreateMap<PayRunForCreationDomain, PayRun>();
            CreateMap<PayRunSummaryDomain, PayRunSummaryResponse>();
            CreateMap<PayRunFlatDomain, PayRunFlatResponse>();
            CreateMap<ReleaseHeldInvoiceItemRequest, ReleaseHeldInvoiceItemDomain>();

            #endregion Payruns

            #region Suppliers

            CreateMap<SupplierMinimalDomain, SupplierMinimalResponse>();

            #endregion Suppliers

            #region PackageTypes

            CreateMap<PackageTypeDomain, PackageTypeResponse>();

            #endregion PackageTypes
        }
    }
}
