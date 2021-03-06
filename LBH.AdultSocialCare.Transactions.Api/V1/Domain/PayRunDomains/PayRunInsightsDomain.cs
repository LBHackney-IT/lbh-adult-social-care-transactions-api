using System;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Domain.PayRunDomains
{
    public class PayRunInsightsDomain
    {
        public Guid PayRunId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal AmountDifferenceFromLastCycle { get; set; }
        public decimal PercentageIncreaseFromLastCycle { get; set; }
        public int TotalSuppliers { get; set; }
        public int TotalServiceUsers { get; set; }
        public int HoldsCount { get; set; }
        public decimal HoldsTotalAmount { get; set; }
    }
}
