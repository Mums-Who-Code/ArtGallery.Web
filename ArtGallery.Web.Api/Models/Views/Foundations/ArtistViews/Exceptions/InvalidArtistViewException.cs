// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace ArtGallery.Web.Api.Models.Views.Foundations.ArtistViews.Exceptions
{
    public class InvalidArtistViewException : Xeption
    {
        public InvalidArtistViewException()
            : base(message: "Invalid artist view error occurred, fix errors and try again.")
        { }

        public InvalidArtistViewException(string parameterName, object parameterValue)
            : base(message: "Invalid artist view error occurred." +
                  $"parameter name: {parameterName}," +
                  $"parameter value: {parameterValue}")
        { }
    }
}
