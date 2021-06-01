using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.BillBoundary.Request;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.BillsDomain;

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
    }
}
