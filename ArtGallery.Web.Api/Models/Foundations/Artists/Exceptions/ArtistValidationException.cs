// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace ArtGallery.Web.Api.Models.Foundations.Artists.Exceptions
{
    public class ArtistValidationException : Xeption
    {
        public ArtistValidationException(Xeption innerException)
            : base(message: "Artist validation error occurred, please fix the error and try again.",
                  innerException)
        { }
    }
}
