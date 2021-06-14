using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.BillBoundary.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundary.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.SupplierBoundary.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.BillsDomain;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.InvoicesDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.SupplierDomains;

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

        public static BillStatusResponse ToResponse(this BillStatusDomain billStatusDomain)
        {
            return _mapper.Map<BillStatusResponse>(billStatusDomain);
        }

        #endregion

        #region Invoice

        public static IEnumerable<PendingInvoicesResponse> ToResponse(this IEnumerable<PendingInvoicesDomain> pendingInvoicesDomains)
        {
            return _mapper.Map<IEnumerable<PendingInvoicesResponse>>(pendingInvoicesDomains);
        }

        #endregion

        #region Supplier

        public static IEnumerable<SupplierResponse> ToResponse(this IEnumerable<SupplierDomain> supplierDomain)
        {
            return _mapper.Map<IEnumerable<SupplierResponse>>(supplierDomain);
        }

        #endregion
    }
}
