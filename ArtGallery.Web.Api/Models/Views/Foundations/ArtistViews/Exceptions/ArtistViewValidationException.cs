// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace ArtGallery.Web.Api.Models.Views.Foundations.ArtistViews.Exceptions
{
    public class ArtistViewValidationException : Xeption
    {
        public ArtistViewValidationException(Xeption innerException)
            : base(message: "Student view validation error occurred, please try again.",
                  innerException)
        { }
    }
}
