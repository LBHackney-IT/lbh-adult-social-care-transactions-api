using System;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Boundary.PayRunBoundaries.Response
{
    public class PayRunInsightsResponse
    {
        public Guid PayRunId { get; set; }
        public decimal TotalAmount { get; set; }
        public float PercentageIncreaseFromLastCycle { get; set; }
        public int TotalSuppliers { get; set; }
        public int TotalServiceUsers { get; set; }
        public int HoldsCount { get; set; }
        public decimal HoldsTotalAmount { get; set; }
    }
}
