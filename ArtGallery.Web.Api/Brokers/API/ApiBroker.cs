// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using RESTFulSense.Clients;

namespace ArtGallery.Web.Api.Brokers.API
{
    public class ApiBroker : IApiBroker
    {
        private readonly IRESTFulApiFactoryClient apiClient;

        public ApiBroker(IRESTFulApiFactoryClient apiClient) =>
            this.apiClient = apiClient;

        private async ValueTask<T> GetAsync<T>(String relativeUrl) =>
            await this.apiClient.GetContentAsync<T>(relativeUrl);

        private async ValueTask<T> PostAsync<T>(String relativeUrl, T Caontent) =>
            await this.apiClient.PostContentAsync<T>(relativeUrl, Caontent);

        private async ValueTask<T> PutAsync<T>(String relativeUrl, T Caontent) =>
            await this.apiClient.PutContentAsync<T>(relativeUrl, Caontent);

        private async ValueTask DeleteAsync<T>(String relativeUrl) =>
            await this.apiClient.DeleteContentAsync<T>(relativeUrl);
    }
}
