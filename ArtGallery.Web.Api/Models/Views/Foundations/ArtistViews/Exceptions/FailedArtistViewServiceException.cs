// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace ArtGallery.Web.Api.Models.Views.Foundations.ArtistViews.Exceptions
{
    public class FailedArtistViewServiceException : Xeption
    {
        public FailedArtistViewServiceException(Exception innerException)
           : base(message: "Failed artist view service error occurred.", innerException)
        { }
    }
}
