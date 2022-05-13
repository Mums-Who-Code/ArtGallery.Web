// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace ArtGallery.Web.Api.Models.Foundations.Artists.Exceptions
{
    public class ArtistDependencyValidationException : Xeption
    {
        public ArtistDependencyValidationException(Xeption innerException)
            : base(message: "Artist dependency validation error occurred, please contact support.",
                  innerException)
        { }
    }
}
