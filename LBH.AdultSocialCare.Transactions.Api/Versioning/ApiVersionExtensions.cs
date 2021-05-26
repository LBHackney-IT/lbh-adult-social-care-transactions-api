using Microsoft.AspNetCore.Mvc;

namespace LBH.AdultSocialCare.Transactions.Api.Versioning
{
    public static class ApiVersionExtensions
    {
        public static string GetFormattedApiVersion(this ApiVersion apiVersion)
        {
            return $"v{apiVersion.ToString()}";
        }
    }
}
