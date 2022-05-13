// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace ArtGallery.Web.Api.Models.Foundations.Artists.Exceptions
{
    public class FailedArtistServiceException : Xeption
    {
        public FailedArtistServiceException(Exception innerException)
            : base(message: "Failed artist service error occurred, please contact support.",
                  innerException)
        { }
    }
}
