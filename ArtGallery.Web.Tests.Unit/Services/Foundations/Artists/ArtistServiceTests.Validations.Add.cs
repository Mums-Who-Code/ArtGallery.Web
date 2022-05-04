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
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfArtistIsNullAndLogItAsync()
        {
            //given
            Artist nullArtist = null;
            var nullArtistException = new NullArtistException();

            var expectedArtistValidationException =
                new ArtistValidationException(
                    nullArtistException);

            //when
            ValueTask<Artist> addArtistTask =
                this.artistService.AddArtistAsync(nullArtist);

            //then
            await Assert.ThrowsAsync<ArtistValidationException>(() =>
                addArtistTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedArtistValidationException))),
                        Times.Once);

            this.apiBrokerMock.Verify(broker =>
                broker.PostArtistAsync(It.IsAny<Artist>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.apiBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfArtistIsInvalidAndLogItAsync()
        {
            //given
            Guid invalidID = Guid.Empty;
            Artist randomArtist = CreateRandomArtist();
            Artist invalidArtist = randomArtist;
            invalidArtist.Id = invalidID;

            var invalidArtistException =
                new InvalidArtistException(
                    parameterName: nameof(Artist.Id),
                    parameterValue: invalidArtist.Id);

            var expectedArtistValidationException =
                new ArtistValidationException(invalidArtistException);

            //when
            ValueTask<Artist> addArtistTask =
                this.artistService.AddArtistAsync(invalidArtist);

            //then
            await Assert.ThrowsAsync<ArtistValidationException>(() =>
                addArtistTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedArtistValidationException))),
                        Times.Once);

            this.apiBrokerMock.Verify(broker =>
                broker.PostArtistAsync(It.IsAny<Artist>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.apiBrokerMock.VerifyNoOtherCalls();
        }
    }
}
