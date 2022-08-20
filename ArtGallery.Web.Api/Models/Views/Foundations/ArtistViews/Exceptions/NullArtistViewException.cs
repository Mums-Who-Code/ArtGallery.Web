// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace ArtGallery.Web.Api.Models.Views.Foundations.ArtistViews.Exceptions
{
    public class NullArtistViewException : Xeption
    {
        public NullArtistViewException()
            : base(message: "Artist is null.")
        { }
    }
}
