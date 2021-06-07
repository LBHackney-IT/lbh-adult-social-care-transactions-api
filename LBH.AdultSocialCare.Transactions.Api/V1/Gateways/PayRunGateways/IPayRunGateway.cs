using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.PayRunModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.PayRunDomains;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Gateways.PayRunGateways
{
    public interface IPayRunGateway
    {
        Task<DateTimeOffset> GetDateOfLastPayRun(int payRunTypeId, int? payRunSubTypeId = null);
        Task<IEnumerable<PayRunSummaryDomain>> GetPayRunSummaryList();

        Task<Guid> CreateNewPayRun(PayRun payRunForCreation);
    }
}
