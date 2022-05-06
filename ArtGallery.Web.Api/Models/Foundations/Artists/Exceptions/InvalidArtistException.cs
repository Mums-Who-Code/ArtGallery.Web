// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace ArtGallery.Web.Api.Models.Foundations.Artists.Exceptions
{
    public class InvalidArtistException : Xeption
    {
        public InvalidArtistException()
            : base(message: "Invalid artist error occured, please fix the error and try again.")
        { }
    }
}
