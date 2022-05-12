﻿// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using ArtGallery.Web.Api.Models.Foundations.Artists;
using ArtGallery.Web.Api.Models.Foundations.Artists.Exceptions;
using Moq;
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
    }
}
