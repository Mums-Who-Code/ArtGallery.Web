// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using ArtGallery.Web.Api.Models.Views.Foundations.ArtistViews;

namespace ArtGallery.Web.Api.Models.Services.Foundations.ArtistViews
{
    public interface IArtistViewService
    {
        ValueTask<ArtistView> AddArtistViewAsync(ArtistView artistView);
    }
}
