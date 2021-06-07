using AutoMapper;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.PayRunDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Exceptions.CustomExceptions;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.PayRunModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums;

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

        public async Task<IEnumerable<PayRunSummaryDomain>> GetPayRunSummaryList()
        {
            var payRunList = await _dbContext.PayRuns.Select(pr => new PayRunSummaryDomain
            {
                PayRunId = pr.PayRunId,
                PayRunNumber = pr.PayRunNumber,
                PayRunTypeId = pr.PayRunTypeId,
                PayRunTypeName = pr.PayRunSubType.SubTypeName,
                PayRunSubTypeId = pr.PayRunSubTypeId,
                PayRunSubTypeName = pr.PayRunSubType.SubTypeName,
                PayRunStatusId = pr.PayRunStatusId,
                PayRunStatusName = pr.PayRunStatus.StatusName,
                TotalAmountPaid = pr.PayRunItems.Where(pri => pri.InvoiceItem.InvoiceItemPaymentStatusId.Equals((int)InvoiceItemPaymentStatusEnum.Paid)).Sum(x => x.PaidAmount),
                TotalAmountHeld = pr.PayRunItems.Where(pri => pri.InvoiceItem.InvoiceItemPaymentStatusId.Equals((int) InvoiceItemPaymentStatusEnum.Held)).Sum(x => x.InvoiceItem.TotalPrice),
                DateFrom = pr.DateFrom,
                DateTo = pr.DateTo,
                DateCreated = pr.DateCreated
            }).ToListAsync().ConfigureAwait(false);
            return payRunList;
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
