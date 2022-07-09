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
            catch (ArtistDependencyException artistDependencyException)
            {
                throw CreateAndLogDependencyException(artistDependencyException);
            }
            catch (ArtistServiceException artistServiceException)
            {
                throw CreateAndLogDependencyException(artistServiceException);
            }
            catch (Exception serviceException)
            {
                var failedArtistViewServiceException =
                    new FailedArtistViewServiceException(serviceException);

                throw CreateAndLogServiceException(failedArtistViewServiceException);
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
            var artistViewDependencyValidationException = new ArtistViewDependencyValidationException(exception);
            this.loggingBroker.LogError(artistViewDependencyValidationException);

            return artistViewDependencyValidationException;
        }

        private ArtistViewDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var artistViewDependencyException = new ArtistViewDependencyException(exception);
            this.loggingBroker.LogError(artistViewDependencyException);

            return artistViewDependencyException;
        }

        private ArtistViewServiceException CreateAndLogServiceException(Xeption exception)
        {
            var artistViewServiceException = new ArtistViewServiceException(exception);
            this.loggingBroker.LogError(artistViewServiceException);

            return artistViewServiceException;
        }
    }
}
