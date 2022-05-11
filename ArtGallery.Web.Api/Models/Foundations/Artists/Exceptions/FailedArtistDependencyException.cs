// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace ArtGallery.Web.Api.Models.Foundations.Artists.Exceptions
{
    public class FailedArtistDependencyException : Xeption
    {
        public FailedArtistDependencyException(Exception innerException)
          : base(message: "Failed artist dependency error occured.", innerException)
        { }
    }
}
