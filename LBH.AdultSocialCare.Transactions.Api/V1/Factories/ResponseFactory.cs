using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.BillBoundary.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.PayRunBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.SupplierBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.BillsDomain;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.PayRunDomains;
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

        #endregion

        #region PayRuns

        public static IEnumerable<PayRunSummaryResponse> ToResponse(this IEnumerable<PayRunSummaryDomain> payRunSummaryDomains)
        {
            return _mapper.Map<IEnumerable<PayRunSummaryResponse>>(payRunSummaryDomains);
        }

        #endregion

        #region Suppliers

        public static IEnumerable<SupplierMinimalResponse> ToResponse(this IEnumerable<SupplierMinimalDomain> supplierMinimalDomains)
        {
            return _mapper.Map<IEnumerable<SupplierMinimalResponse>>(supplierMinimalDomains);
        }

        #endregion
    }
}
