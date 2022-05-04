// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace ArtGallery.Web.Api.Models.Foundations.Artists.Exceptions
{
    public class ArtistValidationException : Xeption
    {
        public ArtistValidationException(Xeption innerException)
            : base(message: "Artist validation error occurred, fix the errors and try again.",
                  innerException)
        { }
    }
}
