namespace LBH.AdultSocialCare.Transactions.Api.V1.Extensions
{
    public static class CalculationExtensions
    {
        public static decimal CalculatePercentageChange(decimal previous, decimal current)
        {
            if (previous == 0)
                return 0;

            if (current == 0)
                return -100;

            var change = ((current - previous) / previous) * 100;

            return change;
        }
    }
}
