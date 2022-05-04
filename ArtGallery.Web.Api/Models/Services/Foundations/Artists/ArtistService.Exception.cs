// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using ArtGallery.Web.Api.Models.Foundations.Artists;
using ArtGallery.Web.Api.Models.Foundations.Artists.Exceptions;

namespace ArtGallery.Web.Api.Models.Services.Foundations.Artists
{
    public partial class ArtistService
    {
        private delegate ValueTask<Artist> ReturningArtistFunction();

        private async ValueTask<Artist> TryCatch(ReturningArtistFunction returningArtistFunction)
        {
            try
            {
                return await returningArtistFunction();
            }
            catch (NullArtistException nullArtistException)
            {
                throw CreateAndLogValidationException(nullArtistException);
            }
        }

        private Exception CreateAndLogValidationException(NullArtistException nullArtistException)
        {
            var artistValidationException = new ArtistValidationException(nullArtistException);
            this.loggingBroker.LogError(artistValidationException);

            return artistValidationException;
        }
    }
}
