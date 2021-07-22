using System;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Boundary.PayRunBoundaries.Request
{
    public class PayRunForCreationRequest
    {
        public DateTimeOffset DateTo { get; set; } = DateTimeOffset.Now;
    }
}
