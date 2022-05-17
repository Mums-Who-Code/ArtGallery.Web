// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Collections;
using Xeptions;

namespace ArtGallery.Web.Api.Models.Foundations.Artists.Exceptions
{
    public class InvalidArtistException : Xeption
    {
        public InvalidArtistException()
            : base(message: "Invalid artist error occurred, please fix the error and try again.")
        { }

        public InvalidArtistException(Exception innerException)
            : base(message: "Invalid artist error occurred, please fix the error and try again.",
                  innerException)
        { }

        public InvalidArtistException(Exception innerException, IDictionary data)
            : base(message: "Invalid artist error occurred, please fix the error and try again.",
                  innerException,
                  data)
        { }
    }
}
