// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using ArtGallery.Web.Api.Models.Foundations.Artists;

namespace ArtGallery.Web.Api.Models.Services.Foundations.Artists
{
    public interface IArtistService
    {
        ValueTask<Artist> AddArtistAsync(Artist artist);
    }
}
