using HttpServices.Concrete;
using HttpServices.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Security.Authentication;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureAdultSocialCareApiService(this IServiceCollection services,
            IConfiguration configuration)
            => services.AddHttpClient<IAdultSocialCareApiService, AdultSocialCareApiService>(client =>
            {
                client.BaseAddress = new Uri(configuration["HASCHttpClients:AdultSocialCareApiBaseUrl"]);
                client.DefaultRequestHeaders.Add("x-api-key",
                    configuration["HASCHttpClients:AdultSocialCareApiApiKey"]);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("User-Agent", "HASC API");
            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler()
            {
                ClientCertificateOptions = ClientCertificateOption.Automatic,
                SslProtocols = SslProtocols.Tls12,
                AllowAutoRedirect = false,
                UseDefaultCredentials = true
            });
    }
}
