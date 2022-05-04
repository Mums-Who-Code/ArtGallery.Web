// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace ArtGallery.Web.Api.Models.Foundations.Artists.Exceptions
{
    public class InvalidArtistException : Xeption
    {
        public InvalidArtistException(string parameterName, object parameterValue)
            : base(message: "Invalid artist error occured, " +
                  $"Parameter Name: {parameterName}" +
                  $"Parameter Value: {parameterValue}")
        { }
    }
}
