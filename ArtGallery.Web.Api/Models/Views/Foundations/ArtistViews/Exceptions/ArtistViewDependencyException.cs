// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace ArtGallery.Web.Api.Models.Views.Foundations.ArtistViews.Exceptions
{
    public class ArtistViewDependencyException : Xeption
    {
        public ArtistViewDependencyException(Exception innerException)
            : base("Artist view dependency error occurred, contact support", innerException)
        { }
    }
}
