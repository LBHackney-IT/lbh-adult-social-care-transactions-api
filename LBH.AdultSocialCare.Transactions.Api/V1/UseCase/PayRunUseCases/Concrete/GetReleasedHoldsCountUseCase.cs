using LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.PayRunBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.PayRunDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Extensions;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.InvoiceGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Concrete
{
    public class GetReleasedHoldsCountUseCase : IGetReleasedHoldsCountUseCase
    {
        private readonly IInvoiceGateway _invoiceGateway;

        public GetReleasedHoldsCountUseCase(IInvoiceGateway invoiceGateway)
        {
            _invoiceGateway = invoiceGateway;
        }

        public async Task<IEnumerable<ReleasedHoldsByTypeResponse>> Execute(DateTimeOffset? fromDate = null, DateTimeOffset? toDate = null)
        {
            var res = await _invoiceGateway
                .GetInvoicesCountUsingStatus((int) InvoiceStatusEnum.Released, fromDate, toDate)
                .ConfigureAwait(false);
            res = res.Select(x => new PayRunItemsPaymentsByTypeDomain { Name = GetNameEnum(x.Name), Value = x.Value });
            var typeCount = res.Select(x => new ReleasedHoldsByTypeResponse
            {
                Name = x.Name,
                Value = x.Value
            }).ToList();
            return typeCount;
        }

        private static string GetNameEnum(string name)
        {
            if (name.Equals(PayRunTypeEnum.ResidentialRecurring.ToDescription()))
            {
                return PayRunTypeEnum.ResidentialRecurring.ToString();
            }

            if (name.Equals(PayRunTypeEnum.DirectPayments.ToDescription()))
            {
                return PayRunTypeEnum.DirectPayments.ToString();
            }

            if (name.Equals(PayRunTypeEnum.HomeCare.ToDescription()))
            {
                return PayRunTypeEnum.HomeCare.ToString();
            }

            return name;
        }
    }
}
