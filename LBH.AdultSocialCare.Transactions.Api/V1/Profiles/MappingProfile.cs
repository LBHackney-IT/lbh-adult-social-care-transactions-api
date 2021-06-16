using System;
using AutoMapper;
using LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.BillBoundary.Request;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.BillBoundary.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.DepartmentBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Request;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundary.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.PackageTypeBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.PayRunBoundaries.Request;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.PayRunBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.SupplierBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.SupplierBoundary.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.BillsDomain;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.DepartmentDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.InvoicesDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.LedgerDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.PackageTypeDomains;
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
            CreateMap<InvoiceItemMinimalDomain, InvoiceItemMinimalResponse>();
            CreateMap<InvoiceItemPaymentStatusDomain, InvoiceItemPaymentStatusResponse>();
            CreateMap<InvoiceDomain, InvoiceResponse>();
            CreateMap<DisputedInvoiceForCreationRequest, DisputedInvoiceForCreationDomain>();
            CreateMap<DisputedInvoiceForCreationDomain, DisputedInvoice>();
            CreateMap<DisputedInvoice, DisputedInvoiceFlatDomain>();
            CreateMap<DisputedInvoiceFlatDomain, DisputedInvoiceFlatResponse>();
            CreateMap<Invoice, PendingInvoicesDomain>();
            CreateMap<InvoiceItem, InvoiceItemDomain>();
            CreateMap<PendingInvoicesDomain, PendingInvoicesResponse>();
            CreateMap<InvoiceItemDomain, InvoiceItemResponse>();
            CreateMap<DisputedInvoice, DisputedInvoiceChat>();
            CreateMap<DisputedInvoiceChatDomain, DisputedInvoiceChatResponse>();
            CreateMap<InvoiceItemForCreationDomain, InvoiceItem>();
            CreateMap<InvoiceForCreationDomain, Invoice>();
            CreateMap<InvoiceForCreationRequest, InvoiceForCreationDomain>()
                .ForMember(ifc => ifc.InvoiceStatusId, opt => opt.MapFrom(b => (int) InvoiceStatusEnum.Draft))
                .ForMember(ifc => ifc.DateInvoiced, opt => opt.MapFrom(b => DateTimeOffset.Now));

            CreateMap<InvoiceItemForCreationRequest, InvoiceItemForCreationDomain>()
                .ForMember(ifc => ifc.InvoiceItemPaymentStatusId, opt => opt.MapFrom(b => (int)InvoiceItemPaymentStatusEnum.NotStarted));

            #endregion Invoices

            #region Payruns

            CreateMap<PayRunForCreationDomain, PayRun>();
            CreateMap<PayRunSummaryDomain, PayRunSummaryResponse>();
            CreateMap<PayRunFlatDomain, PayRunFlatResponse>();
            CreateMap<ReleaseHeldInvoiceItemRequest, ReleaseHeldInvoiceItemDomain>();
            CreateMap<PayRunInsightsDomain, PayRunInsightsResponse>();

            #endregion Payruns

            #region Suppliers

            CreateMap<SupplierMinimalDomain, SupplierMinimalResponse>();

            #endregion Suppliers

            #region PackageTypes

            CreateMap<PackageTypeDomain, PackageTypeResponse>();
            CreateMap<PackageType, PackageTypeDomain>();

            #endregion PackageTypes

            #region Departments

            CreateMap<Department, DepartmentDomain>();
            CreateMap<DepartmentDomain, DepartmentResponse>();

            #endregion Departments

            #region Supplier

            CreateMap<Supplier, SupplierDomain>();
            CreateMap<SupplierDomain, SupplierResponse>();
            CreateMap<SupplierTaxRate, SupplierTaxRateDomain>();
            CreateMap<SupplierTaxRateDomain, SupplierTaxRateResponse>();

            #endregion Supplier

            CreateMap<PackageType, PackageTypeDomain>();

            #region Ledger

            CreateMap<Ledger, LedgerDomain>();

            #endregion Ledger
        }
    }
}
