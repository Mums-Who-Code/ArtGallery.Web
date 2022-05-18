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
    }
}
