// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using ArtGallery.Web.Api.Models.Foundations.Artists;

namespace ArtGallery.Web.Api.Brokers.Apis
{
    public partial interface IApiBroker
    {
        ValueTask<Artist> PostArtistAsync(Artist artist);
    }
}
