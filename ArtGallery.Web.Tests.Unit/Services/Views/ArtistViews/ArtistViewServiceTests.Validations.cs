// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using ArtGallery.Web.Api.Models.Foundations.Artists;
using ArtGallery.Web.Api.Models.Views.Foundations.ArtistViews;
using ArtGallery.Web.Api.Models.Views.Foundations.ArtistViews.Exceptions;
using Moq;
using Xunit;

namespace ArtGallery.Web.Tests.Unit.Services.Views.ArtistViews
{
    public partial class ArtistViewServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfArtistViewIsNullAndLogItAsync()
        {
            //given
            ArtistView nullArtistView = null;
            var nullArtistException = new NullArtistViewException();

            var expectedArtistViewValidationException =
                new ArtistViewValidationException(nullArtistException);

            //when
            ValueTask<ArtistView> addArtistViewTask =
                this.artistViewService.AddArtistViewAsync(nullArtistView);

            //then
            await Assert.ThrowsAsync<ArtistViewValidationException>(() =>
                addArtistViewTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedArtistViewValidationException))),
                        Times.Once);

            this.userServiceMock.Verify(service =>
                service.GetCurrentlyLoggedInUser(),
                    Times.Never);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Never);

            this.artistServiceMock.Verify(service =>
                service.AddArtistAsync(It.IsAny<Artist>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.userServiceMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.artistServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public async Task ShouldThrowValidationExceptionOnAddIfArtistViewIsInvalidAndLogItAsync(
            string invalidText)
        {
            //given
            ArtistView invalidArtistView = new ArtistView
            {
                FirstName = invalidText,
                LastName = invalidText,
                Email = invalidText,
                ContactNumber = invalidText,
                Status = ArtistStatusView.InActive
            };

            var invalidArtistViewException = new InvalidArtistViewException();

            invalidArtistViewException.AddData(
               key: nameof(ArtistView.Id),
               values: "Id is required.");

            invalidArtistViewException.AddData(
                key: nameof(ArtistView.FirstName),
                values: "Text is required.");

            invalidArtistViewException.AddData(
                key: nameof(ArtistView.LastName),
                values: "Text is required.");

            invalidArtistViewException.AddData(
                key: nameof(ArtistView.Email),
                values: "Text is required.");

            invalidArtistViewException.AddData(
                key: nameof(ArtistView.ContactNumber),
                values: "Text is required.");

            var expectedArtistViewValidationException =
                new ArtistViewValidationException(invalidArtistViewException);

            //when
            ValueTask<ArtistView> addArtistViewTask =
                this.artistViewService.AddArtistViewAsync(invalidArtistView);

            //then
            await Assert.ThrowsAsync<ArtistViewValidationException>(() =>
                addArtistViewTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedArtistViewValidationException))),
                        Times.Once);

            this.userServiceMock.Verify(service =>
                service.GetCurrentlyLoggedInUser(),
                    Times.Never);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Never);

            this.artistServiceMock.Verify(service =>
                service.AddArtistAsync(It.IsAny<Artist>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.userServiceMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.artistServiceMock.VerifyNoOtherCalls();
        }
    }
}
