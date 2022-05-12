// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace ArtGallery.Web.Api.Models.Foundations.Artists.Exceptions
{
    public class InvalidArtistException : Xeption
    {
        public InvalidArtistException()
            : base(message: "Invalid artist error occurred, please fix the error and try again.")
        { }
    }
}
