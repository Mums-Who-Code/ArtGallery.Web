// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Collections;
using ArtGallery.Web.Api.Models.Foundations.Artists;
using ArtGallery.Web.Api.Models.Foundations.Artists.Exceptions;
using Moq;
using RESTFulSense.Exceptions;
using Xunit;

namespace ArtGallery.Web.Tests.Unit.Services.Foundations.Artists
{
    public partial class ArtistServiceTests
    {
        [Theory]
        [MemberData(nameof(CriticalDependencyException))]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfDependencyErrorOccursAndLogItAsync(
            Exception criticalDependencyException)
        {
            //given
            Artist someArtist = CreateRandomArtist();

            var failedArtistDependencyException =
                new FailedArtistDependencyException(criticalDependencyException);

            var expectedArtistDependencyException =
                new ArtistDependencyException(failedArtistDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.PostArtistAsync(It.IsAny<Artist>()))
                    .ThrowsAsync(criticalDependencyException);

            //when
            ValueTask<Artist> addArtistTask =
                this.artistService.AddArtistAsync(someArtist);

            //then
            await Assert.ThrowsAsync<ArtistDependencyException>(() =>
                addArtistTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedArtistDependencyException))),
                        Times.Once);

            this.apiBrokerMock.Verify(broker =>
                broker.PostArtistAsync(It.IsAny<Artist>()),
                    Times.Once);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.apiBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(ApiDependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionOnAddIfErrorOccursAndLogItAsync(
                Exception apiDependencyException)
        {
            //given
            Artist someArtist = CreateRandomArtist();

            var failedArtistDependencyException =
                new FailedArtistDependencyException(apiDependencyException);

            var expectedArtistDependencyException =
                new ArtistDependencyException(failedArtistDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.PostArtistAsync(It.IsAny<Artist>()))
                    .ThrowsAsync(apiDependencyException);

            //when
            ValueTask<Artist> addArtistTask =
                this.artistService.AddArtistAsync(someArtist);

            //then
            await Assert.ThrowsAsync<ArtistDependencyException>(() =>
                addArtistTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedArtistDependencyException))),
                        Times.Once);

            this.apiBrokerMock.Verify(broker =>
                broker.PostArtistAsync(It.IsAny<Artist>()),
                    Times.Once);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.apiBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfBadRequestAndLogItAsync()
        {
            //given
            IDictionary randomDictionary = CreateRandomDictionary();
            IDictionary exceptionData = randomDictionary;
            string randomMessage = GetRandomMesaage();
            string responseMessage = randomMessage;
            var httpResponseMessage = new HttpResponseMessage();
            Artist someArtist = CreateRandomArtist();

            var httpResponseBadRequestException =
                new HttpResponseBadRequestException(
                    httpResponseMessage,
                    responseMessage);

            httpResponseBadRequestException.AddData(exceptionData);

            this.apiBrokerMock.Setup(broker =>
                broker.PostArtistAsync(It.IsAny<Artist>()))
                    .ThrowsAsync(httpResponseBadRequestException);

            var invalidArtistException =
                new InvalidArtistException(
                    httpResponseBadRequestException,
                        exceptionData);

            var expeectedArtistDependencyError =
                new ArtistDependencyValidationException(invalidArtistException);

            //when
            ValueTask<Artist> addArtistTask =
                this.artistService.AddArtistAsync(someArtist);

            //then
            await Assert.ThrowsAsync<ArtistDependencyValidationException>(() =>
                addArtistTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.PostArtistAsync(It.IsAny<Artist>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expeectedArtistDependencyError))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyValidationExceptions))]
        public async Task ShouldThrowDepedencyValidationExceptionOnAddIfValidationErrorOccursAndLogItAsync(
           Exception dependencyValidaionException)
        {
            //given
            Artist someArtist = CreateRandomArtist();

            var invalidArtistException =
                new InvalidArtistException(dependencyValidaionException);

            this.apiBrokerMock.Setup(broker =>
                broker.PostArtistAsync(It.IsAny<Artist>()))
                    .ThrowsAsync(dependencyValidaionException);

            var expectedArtistDependencyException =
                new ArtistDependencyValidationException(invalidArtistException);

            //when
            ValueTask<Artist> addArtistTask =
                this.artistService.AddArtistAsync(someArtist);

            //then
            await Assert.ThrowsAsync<ArtistDependencyValidationException>(() =>
                addArtistTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.PostArtistAsync(It.IsAny<Artist>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedArtistDependencyException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
