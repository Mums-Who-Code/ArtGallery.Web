// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace ArtGallery.Web.Api.Models.Views.Foundations.ArtistViews.Exceptions
{
    public class ArtistViewDependencyValidationException : Xeption
    {
        public ArtistViewDependencyValidationException(Xeption innerException)
           : base(message: "Artist view dependency validation error occurred, try again",
                 innerException)
        { }
    }
}
