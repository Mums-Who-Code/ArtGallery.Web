// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using ArtGallery.Web.Api.Models.Foundations.Artists.Exceptions;
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
            catch (InvalidArtistViewException invalidArtistViewException)
            {
                throw CreateAndLogValidationException(invalidArtistViewException);
            }
            catch (ArtistValidationException artistValidationException)
            {
                throw CreateAndLogDependencyValidationException(artistValidationException);
            }
            catch (ArtistDependencyValidationException artistDependencyValidationException)
            {
                throw CreateAndLogDependencyValidationException(artistDependencyValidationException);
            }
        }

        private ArtistViewValidationException CreateAndLogValidationException(Xeption exception)
        {
            var artistViewValidationException = new ArtistViewValidationException(exception);
            this.loggingBroker.LogError(artistViewValidationException);

            return artistViewValidationException;
        }

        private ArtistViewDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var artistViewDependencyValidationException = new ArtistViewDependencyValidationException(exception.InnerException);
            this.loggingBroker.LogError(artistViewDependencyValidationException);

            return artistViewDependencyValidationException;
        }
    }
}
