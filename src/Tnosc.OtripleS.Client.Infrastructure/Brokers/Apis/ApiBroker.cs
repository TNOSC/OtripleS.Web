// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RESTFulSense.Clients;
using Tnosc.OtripleS.Client.Application.Brokers.Apis;

namespace Tnosc.OtripleS.Client.Infrastructure.Brokers.Apis;

internal sealed partial class ApiBroker : IApiBroker
{
    private readonly IRESTFulApiFactoryClient _apiClient;
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;

    public ApiBroker(
        IConfiguration configuration,
        IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient();
        _configuration = configuration;
        _apiClient = GetApiClient();
    }

    private async ValueTask<T> GetAsync<T>(string relativeUrl) =>
        await _apiClient.GetContentAsync<T>(relativeUrl);

    private async ValueTask<T> PostAsync<T>(string relativeUrl, T content) =>
        await _apiClient.PostContentAsync<T>(relativeUrl, content);

    private async ValueTask<T> PutAsync<T>(string relativeUrl, T content) =>
        await _apiClient.PutContentAsync<T>(relativeUrl, content);

    private async ValueTask<T> DeleteAsync<T>(string relativeUrl) =>
        await _apiClient.DeleteContentAsync<T>(relativeUrl);

    private IRESTFulApiFactoryClient GetApiClient()
    {
        string apiBaseUrl =
            _configuration.GetValue<string>(
                key: "OtriplesApiBaseAddress")!;

        _httpClient.BaseAddress =
            new Uri(uriString: apiBaseUrl);

        return new RESTFulApiFactoryClient(httpClient: _httpClient);
    }
}
