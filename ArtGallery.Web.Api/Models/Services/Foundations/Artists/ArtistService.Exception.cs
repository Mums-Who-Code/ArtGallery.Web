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
            catch (HttpResponseBadRequestException httpResponseBadRequestException)
            {
                var invalidArtistException =
                    new InvalidArtistException(
                        httpResponseBadRequestException,
                        httpResponseBadRequestException.Data);

                throw CreateAndLogDependencyValidationException(invalidArtistException);
            }
            catch(HttpResponseConflictException httpResponseConflictException)
            {
                var invalidArtistException =
                    new InvalidArtistException(httpResponseConflictException);

                throw CreateAndLogDependencyValidationException(invalidArtistException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedArtistDependencyException =
                    new FailedArtistDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedArtistDependencyException);
            }
            catch (Exception serviceException)
            {
                var failedArtistServiceException =
                    new FailedArtistServiceException(serviceException);

                throw CreateAndLogServiceException(failedArtistServiceException);
            }
        }

        private ArtistServiceException CreateAndLogServiceException(Xeption exception)
        {
            var artistServiceException = new ArtistServiceException(exception);
            this.loggingBroker.LogError(artistServiceException);

            return artistServiceException;
        }

        private ArtistDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var artistDependencyValidationException =
                new ArtistDependencyValidationException(exception);
            this.loggingBroker.LogError(artistDependencyValidationException);

            return artistDependencyValidationException;
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
