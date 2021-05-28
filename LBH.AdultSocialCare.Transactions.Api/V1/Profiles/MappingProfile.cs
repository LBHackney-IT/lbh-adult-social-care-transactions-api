using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.BillBoundary.Request;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.BillBoundary.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.BillsDomain;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Bills;

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
        }
    }
}