// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using ArtGallery.Web.Api.Models.Configurations;
using RESTFulSense.Clients;

namespace ArtGallery.Web.Api.Brokers.Apis
{
    public class ApiBroker : IApiBroker
    {
        private readonly IRESTFulApiFactoryClient apiClient;
        private readonly HttpClient httpClient;

        public ApiBroker(HttpClient httpClient,
            IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.apiClient = GetApiClient(configuration);
        }

        private async ValueTask<T> GetAsync<T>(String relativeUrl) =>
            await this.apiClient.GetContentAsync<T>(relativeUrl);

        private async ValueTask<T> PostAsync<T>(String relativeUrl, T Caontent) =>
            await this.apiClient.PostContentAsync<T>(relativeUrl, Caontent);

        private async ValueTask<T> PutAsync<T>(String relativeUrl, T Caontent) =>
            await this.apiClient.PutContentAsync<T>(relativeUrl, Caontent);

        private async ValueTask DeleteAsync<T>(String relativeUrl) =>
            await this.apiClient.DeleteContentAsync<T>(relativeUrl);

        private IRESTFulApiFactoryClient GetApiClient(IConfiguration configuration)
        {
            LocalConfigurations localConfigurations =
               configuration.Get<LocalConfigurations>();

            string apiBaseUrl = localConfigurations.ApiConfigurations.Url;
            this.httpClient.BaseAddress = new Uri(apiBaseUrl);

            return new RESTFulApiFactoryClient(this.httpClient);
        }
    }
}
