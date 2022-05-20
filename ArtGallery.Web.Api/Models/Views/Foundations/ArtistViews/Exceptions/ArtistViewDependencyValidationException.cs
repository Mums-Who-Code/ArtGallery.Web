// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace ArtGallery.Web.Api.Models.Views.Foundations.ArtistViews.Exceptions
{
    public class ArtistViewDependencyValidationException : Xeption
    {
        public ArtistViewDependencyValidationException(Exception innerException)
           : base("Artist view dependency error occurred, try again", innerException)
        { }
    }
}
