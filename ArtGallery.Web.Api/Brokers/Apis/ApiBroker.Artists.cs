// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using ArtGallery.Web.Api.Models.Foundations.Artists;

namespace ArtGallery.Web.Api.Brokers.Apis
{
    public partial class ApiBroker : IApiBroker
    {
        private const string ArtistsRelativeUrl = "api/artists";

        public async ValueTask<Artist> PostArtistAsync(Artist artist) =>
            await this.PostAsync(ArtistsRelativeUrl, artist);
    }
}
