// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using ArtGallery.Web.Api.Models.Views.Foundations.ArtistViews;
using ArtGallery.Web.Api.Models.Views.Foundations.ArtistViews.Exceptions;
using Xeptions;

namespace ArtGallery.Web.Api.Models.Services.Foundations.ArtistViews
{
    public partial class ArtistViewService
    {
        private delegate ValueTask<ArtistView> ReturningArtistViewFunction();

        private async ValueTask<ArtistView> TryCatch(ReturningArtistViewFunction returningArtistViewFunction)
        {
            try
            {
                return await returningArtistViewFunction();
            }
            catch (NullArtistViewException nullArtistViewException)
            {
                throw CreateAndLogValidationException(nullArtistViewException);
            }
        }

        private ArtistViewValidationException CreateAndLogValidationException(Xeption exception)
        {
            var artistViewValidationException = new ArtistViewValidationException(exception);
            this.loggingBroker.LogError(artistViewValidationException);

            return artistViewValidationException;
        }
    }
}
