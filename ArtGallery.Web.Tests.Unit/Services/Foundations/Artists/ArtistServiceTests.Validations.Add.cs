// -----------------------------------------------------------------------
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

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnAddIfArtistIsInvalidAndLogItAsync(
             string invalidText)
        {
            //given
            var invalidArtist = new Artist
            {
                FirstName = invalidText,
                LastName = invalidText,
                Email = invalidText,
                ContactNumber = invalidText,
                Status = ArtistStatus.InActive
            };

            var invalidArtistException = new InvalidArtistException();

            invalidArtistException.AddData(
                key: nameof(Artist.Id),
                values: "Id is required.");

            invalidArtistException.AddData(
                key: nameof(Artist.FirstName),
                values: "Text is required.");

            invalidArtistException.AddData(
                key: nameof(Artist.LastName),
                values: "Text is required.");

            invalidArtistException.AddData(
                key: nameof(Artist.Email),
                values: "Text is required.");

            invalidArtistException.AddData(
                key: nameof(Artist.ContactNumber),
                values: "Text is required.");

            invalidArtistException.AddData(
                key: nameof(Artist.Status),
                values: "Value is invalid.");

            invalidArtistException.AddData(
               key: nameof(Artist.CreatedBy),
               values: "Id is required.");

            invalidArtistException.AddData(
               key: nameof(Artist.CreatedDate),
               values: "Date is required.");

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
