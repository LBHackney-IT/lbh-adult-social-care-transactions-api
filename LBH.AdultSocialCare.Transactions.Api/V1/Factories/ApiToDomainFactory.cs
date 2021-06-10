using AutoMapper;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.BillBoundary.Request;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.PayRunBoundaries.Request;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.BillsDomain;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.PayRunDomains;
using System.Collections.Generic;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Factories
{
    public static class ApiToDomainFactory
    {
        private static IMapper _mapper { get; set; }

        public static void Configure(IMapper mapper)
        {
            _mapper = mapper;
        }

        public static BillCreationDomain ToDomain(this BillCreationRequest billCreationRequest)
        {
            var res = _mapper.Map<BillCreationDomain>(billCreationRequest);
            return res;
        }

        #region ReleaseHeldInvoiceItems

        public static ReleaseHeldInvoiceItemDomain ToDomain(this ReleaseHeldInvoiceItemRequest releaseHeldInvoiceItemRequest)
        {
            var res = _mapper.Map<ReleaseHeldInvoiceItemDomain>(releaseHeldInvoiceItemRequest);
            return res;
        }

        public static IEnumerable<ReleaseHeldInvoiceItemDomain> ToDomain(this IEnumerable<ReleaseHeldInvoiceItemRequest> releaseHeldInvoiceItemRequests)
        {
            var res = _mapper.Map<IEnumerable<ReleaseHeldInvoiceItemDomain>>(releaseHeldInvoiceItemRequests);
            return res;
        }

        #endregion
    }
}
