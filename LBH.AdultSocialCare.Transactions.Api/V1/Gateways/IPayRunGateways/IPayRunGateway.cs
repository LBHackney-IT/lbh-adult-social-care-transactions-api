using LBH.AdultSocialCare.Transactions.Api.V1.Domain.PayRunDomains;
using System;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Gateways.IPayRunGateways
{
    public interface IPayRunGateway
    {
        Task<DateTimeOffset> GetDateOfLastPayRun(int payRunTypeId, int? payRunSubTypeId = null);

        Task<Guid> CreateNewPayRun(PayRunForCreationDomain payRunForCreationDomain);
    }
}
