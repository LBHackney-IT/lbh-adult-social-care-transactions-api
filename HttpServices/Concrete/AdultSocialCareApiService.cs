using HttpServices.Contracts;
using HttpServices.Models;
using HttpServices.Models.Response;
using Infrastructure.Domain.InvoicesDomains;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpServices.Concrete
{
    public class AdultSocialCareApiService : IAdultSocialCareApiService
    {
        private readonly HttpClient _client;
        private readonly IRestClient _restClient;
        private readonly string _baseUrl;
        private readonly string _apiKey;

        public AdultSocialCareApiService(HttpClient client, IRestClient restClient, IOptions<AdultSocialCareApiOptions> options)
        {
            _client = client;
            _restClient = restClient;
            _baseUrl = options.Value.AdultSocialCareApiBaseUrl.ToString();
            _apiKey = options.Value.AdultSocialCareApiApiKey;
        }

        public async Task<GenericSuccessResponse> ResetInvoiceCreatedUpTo(IEnumerable<InvoiceForResetDomain> invoiceForResetDomains)
        {
            try
            {
                var res = await _restClient
                    .Post<GenericSuccessResponse>($"{_baseUrl}api/v1/transactions/pay-runs/invoice-date-reset", invoiceForResetDomains, "Failed reset invoice dates", _apiKey)
                    .ConfigureAwait(false);

                return res;
            }
            catch (Exception e)
            {
                return new GenericSuccessResponse() { IsSuccess = false, Message = "Failed to reset invoice dates" };
            }
        }
    }
}
