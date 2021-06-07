using LBH.AdultSocialCare.Transactions.Api.V1.Domain.PayRunDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.PayRunModels;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.RequestExtensions;
using System;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Gateways.PayRunGateways
{
    public interface IPayRunGateway
    {
        Task<DateTimeOffset> GetDateOfLastPayRun(int payRunTypeId, int? payRunSubTypeId = null);

        Task<PagedList<PayRunSummaryDomain>> GetPayRunSummaryList(PayRunSummaryListParameters parameters);

        Task<Guid> CreateNewPayRun(PayRun payRunForCreation);
    }
}
