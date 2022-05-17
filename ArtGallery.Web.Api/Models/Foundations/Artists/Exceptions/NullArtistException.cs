// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace ArtGallery.Web.Api.Models.Foundations.Artists.Exceptions
{
    public class NullArtistException : Xeption
    {
        public NullArtistException()
            : base(message: "Artist is null.")
        { }
    }
}
