using AutoMapper;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.BillBoundary.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.DepartmentBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundary.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.PackageTypeBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.PayRunBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.SupplierBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.SupplierBoundary.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.DepartmentDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.InvoicesDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.PackageTypeDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.PayRunDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.SupplierDomains;
using System.Collections.Generic;
using System.Linq;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.BillsDomain;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Factories
{
    public static class ResponseFactory
    {
        private static IMapper _mapper { get; set; }

        public static void Configure(IMapper mapper)
        {
            _mapper = mapper;
        }

        //TODO: Map the fields in the domain object(s) to fields in the response object(s).
        // More information on this can be found here https://github.com/LBHackney-IT/lbh-base-api/wiki/Factory-object-mappings
        public static ResponseObject ToResponse(this Entity domain)
        {
            return new ResponseObject();
        }

        public static List<ResponseObject> ToResponse(this IEnumerable<Entity> domainList)
        {
            return domainList.Select(domain => domain.ToResponse()).ToList();
        }

        #region Bill

        public static BillResponse ToResponse(this BillDomain billDomain)
        {
            return _mapper.Map<BillResponse>(billDomain);
        }

        public static IEnumerable<BillItemResponse> ToResponse(this IEnumerable<BillItemDomain> billItemDomains)
        {
            return _mapper.Map<IEnumerable<BillItemResponse>>(billItemDomains);
        }

        public static IEnumerable<BillResponse> ToResponse(this IEnumerable<BillDomain> billDomains)
        {
            return _mapper.Map<IEnumerable<BillResponse>>(billDomains);
        }

    #endregion Bill

        #region PayRuns

    public static IEnumerable<PayRunSummaryResponse> ToResponse(this IEnumerable<PayRunSummaryDomain> payRunSummaryDomains)
        {
            return _mapper.Map<IEnumerable<PayRunSummaryResponse>>(payRunSummaryDomains);
        }

        public static PayRunFlatResponse ToResponse(this PayRunFlatDomain payRunFlat)
        {
            return _mapper.Map<PayRunFlatResponse>(payRunFlat);
        }

        public static PayRunInsightsResponse ToResponse(this PayRunInsightsDomain payRunInsightsDomain)
        {
            return _mapper.Map<PayRunInsightsResponse>(payRunInsightsDomain);
        }

        #endregion PayRuns

        #region Suppliers

        public static IEnumerable<SupplierMinimalResponse> ToResponse(this IEnumerable<SupplierMinimalDomain> supplierMinimalDomains)
        {
            return _mapper.Map<IEnumerable<SupplierMinimalResponse>>(supplierMinimalDomains);
        }

        #endregion Suppliers

        #region PackageTypes

        public static IEnumerable<PackageTypeResponse> ToResponse(this IEnumerable<PackageTypeDomain> packageTypeDomains)
        {
            return _mapper.Map<IEnumerable<PackageTypeResponse>>(packageTypeDomains);
        }

        #endregion PackageTypes

        #region InvoiceItems

        public static IEnumerable<InvoiceItemMinimalResponse> ToResponse(this IEnumerable<InvoiceItemMinimalDomain> invoiceItemMinimalDomains)
        {
            return _mapper.Map<IEnumerable<InvoiceItemMinimalResponse>>(invoiceItemMinimalDomains);
        }

        public static IEnumerable<InvoiceItemPaymentStatusResponse> ToResponse(this IEnumerable<InvoiceItemPaymentStatusDomain> invoiceItemPaymentStatusDomains)
        {
            return _mapper.Map<IEnumerable<InvoiceItemPaymentStatusResponse>>(invoiceItemPaymentStatusDomains);
        }

        public static IEnumerable<InvoiceResponse> ToResponse(this IEnumerable<InvoiceDomain> invoiceDomains)
        {
            return _mapper.Map<IEnumerable<InvoiceResponse>>(invoiceDomains);
        }

        #endregion InvoiceItems

        #region Departments

        public static IEnumerable<DepartmentResponse> ToResponse(this IEnumerable<DepartmentDomain> departmentDomains)
        {
            return _mapper.Map<IEnumerable<DepartmentResponse>>(departmentDomains);
        }

        #endregion Departments

        #region Invoices

        public static DisputedInvoiceFlatResponse ToResponse(this DisputedInvoiceFlatDomain disputedInvoiceFlatDomain)
        {
            return _mapper.Map<DisputedInvoiceFlatResponse>(disputedInvoiceFlatDomain);
        }

        #endregion Invoices

        public static BillStatusResponse ToResponse(this BillStatusDomain billStatusDomain)
        {
            return _mapper.Map<BillStatusResponse>(billStatusDomain);
        }

        #region Invoice

        public static IEnumerable<PendingInvoicesResponse> ToResponse(this IEnumerable<PendingInvoicesDomain> pendingInvoicesDomains)
        {
            return _mapper.Map<IEnumerable<PendingInvoicesResponse>>(pendingInvoicesDomains);
        }

        #endregion Invoice

        #region Supplier

        public static IEnumerable<SupplierResponse> ToResponse(this IEnumerable<SupplierDomain> supplierDomain)
        {
            return _mapper.Map<IEnumerable<SupplierResponse>>(supplierDomain);
        }

        public static IEnumerable<SupplierTaxRateResponse> ToResponse(this IEnumerable<SupplierTaxRateDomain> supplierTaxRateDomain)
        {
            return _mapper.Map<IEnumerable<SupplierTaxRateResponse>>(supplierTaxRateDomain);
        }

        #endregion Supplier
    }
}
