// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using ArtGallery.Web.Api.Models.Foundations.Artists;
using ArtGallery.Web.Api.Models.Foundations.Artists.Exceptions;
using RESTFulSense.Exceptions;
using Xeptions;

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
            catch (InvalidArtistException invalidArtistException)
            {
                throw CreateAndLogValidationException(invalidArtistException);
            }
            catch (HttpRequestException httpRequestException)
            {
                var failedArtistDependencyException =
                    new FailedArtistDependencyException(httpRequestException);

                throw CreateAndLogCriticalDependencyException(failedArtistDependencyException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var failedArtistDependencyException =
                    new FailedArtistDependencyException(httpResponseUnauthorizedException);

                throw CreateAndLogCriticalDependencyException(failedArtistDependencyException);
            }
            catch(HttpResponseException httpResponseException)
            {
                var failedArtistDependencyException =
                    new FailedArtistDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedArtistDependencyException);
            }
        }

        private ArtistDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var artistDependencyException = new ArtistDependencyException(exception);
            this.loggingBroker.LogError(artistDependencyException);

            return artistDependencyException;
        }

        private ArtistDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var artistDependencyException = new ArtistDependencyException(exception);
            this.loggingBroker.LogCritical(artistDependencyException);

            return artistDependencyException;
        }

        private ArtistValidationException CreateAndLogValidationException(Xeption exception)
        {
            var artistValidationException = new ArtistValidationException(exception);
            this.loggingBroker.LogError(artistValidationException);

            return artistValidationException;
        }
    }
}
