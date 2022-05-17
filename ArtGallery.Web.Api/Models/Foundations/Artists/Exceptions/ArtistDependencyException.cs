// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace ArtGallery.Web.Api.Models.Foundations.Artists.Exceptions
{
    public class ArtistDependencyException : Xeption
    {
        public ArtistDependencyException(Xeption innerException)
            : base(message: "Artist dependency error occurred, please contact support.",
                  innerException)
        { }
    }
}
