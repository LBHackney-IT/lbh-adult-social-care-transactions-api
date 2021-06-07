using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.BillsDomain;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.PayRunDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Bills;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.PayRunModels;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Factories
{
    public static class DomainToEntityFactory
    {
        private static IMapper _mapper { get; set; }

        public static void Configure(IMapper mapper)
        {
            _mapper = mapper;
        }

        #region Bill

        public static Bill ToDb(this BillCreationDomain billCreationDomain)
        {
            return _mapper.Map<Bill>(billCreationDomain);
        }

        #endregion

        #region PayRuns

        public static PayRun ToDb(this PayRunForCreationDomain payRunForCreationDomain)
        {
            var payRunForCreation = _mapper.Map<PayRun>(payRunForCreationDomain);
            payRunForCreation.PayRunItems = payRunForCreationDomain.InvoiceItems.Select(ii => new PayRunItem
            {
                PayRunId = payRunForCreation.PayRunId,
                InvoiceItemId = ii.InvoiceItemId,
                PaidAmount = 0,
                RemainingBalance = ii.TotalPrice
            }).ToList();
            payRunForCreation.PayRunStatusId = (int) PayRunStatusesEnum.Draft;
            return payRunForCreation;
        }


        #endregion
    }
}
