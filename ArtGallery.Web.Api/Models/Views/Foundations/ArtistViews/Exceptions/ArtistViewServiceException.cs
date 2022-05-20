// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace ArtGallery.Web.Api.Models.Views.Foundations.ArtistViews.Exceptions
{
    public class ArtistViewServiceException : Xeption
    {
        public ArtistViewServiceException(Xeption innerException)
            : base(message: "Artist view service error occurred, contact support.", innerException) 
        { }
    }
}
