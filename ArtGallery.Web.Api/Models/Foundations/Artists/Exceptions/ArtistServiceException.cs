// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace ArtGallery.Web.Api.Models.Foundations.Artists.Exceptions
{
    public class ArtistServiceException : Xeption
    {
        public ArtistServiceException(Xeption innerException)
            : base(message: "Artist service error occurred, please contact support.",
                  innerException)
        { }
    }
}
