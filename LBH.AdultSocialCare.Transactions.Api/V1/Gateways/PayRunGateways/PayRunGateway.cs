using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.PayRunDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Exceptions.CustomExceptions;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.PayRunModels;
using Microsoft.EntityFrameworkCore;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Gateways.PayRunGateways
{
    public class PayRunGateway : IPayRunGateway
    {
        private readonly DatabaseContext _dbContext;
        private readonly IMapper _mapper;

        public PayRunGateway(DatabaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<DateTimeOffset> GetDateOfLastPayRun(int payRunTypeId, int? payRunSubTypeId = null)
        {
            var lastPayRun = await _dbContext.PayRuns.Where(pr =>
                    pr.PayRunTypeId.Equals(payRunTypeId) &&
                    (pr.PayRunSubTypeId.Equals(payRunSubTypeId) || payRunSubTypeId.Equals(null)))
                .OrderByDescending(pr => pr.DateTo)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);

            return lastPayRun?.DateTo ?? DateTimeOffset.Now.AddDays(-28);
        }

        public async Task<Guid> CreateNewPayRun(PayRun payRunForCreation)
        {
            var entry = await _dbContext.PayRuns.AddAsync(payRunForCreation).ConfigureAwait(false);
            try
            {
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);

                return entry.Entity.PayRunId;
            }
            catch (Exception)
            {
                throw new DbSaveFailedException("Could not save pay run to database");
            }
        }
    }
}
