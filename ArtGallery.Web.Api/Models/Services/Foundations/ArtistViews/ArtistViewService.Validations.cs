// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using ArtGallery.Web.Api.Models.Views.Foundations.ArtistViews;
using ArtGallery.Web.Api.Models.Views.Foundations.ArtistViews.Exceptions;

namespace ArtGallery.Web.Api.Models.Services.Foundations.ArtistViews
{
    public partial class ArtistViewService
    {
        private void ValidateArtistViewOnAdd(ArtistView artistView)
        {
            ValidateInput(artistView);
        }

        private void ValidateInput(ArtistView artistView)
        {
            if (artistView == null)
            {
                throw new NullArtistViewException();
            }
        }
    }
}
