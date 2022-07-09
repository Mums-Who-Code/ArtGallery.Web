// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using ArtGallery.Web.Api.Models.Foundations.Artists;
using ArtGallery.Web.Api.Models.Views.Foundations.ArtistViews;
using ArtGallery.Web.Api.Models.Views.Foundations.ArtistViews.Exceptions;
using Moq;
using Xeptions;
using Xunit;

namespace ArtGallery.Web.Tests.Unit.Services.Views.ArtistViews
{
    public partial class ArtistViewServiceTests
    {
        [Theory]
        [MemberData(nameof(DependencyValidationExceptions))]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfValidationErrorOccuredAndLogItAsync(
           Xeption artistValidationException)
        {
            //given
            ArtistView someArtistView = CreateRandomArtistView();

            var expectedDependencyValidationException =
                new ArtistViewDependencyValidationException(artistValidationException);

            this.dateTimeBrokerMock.Setup(service =>
                service.GetCurrentDateTime())
                    .Throws(artistValidationException);

            //when
            ValueTask<ArtistView> addArtistViewTask =
                this.artistViewService.AddArtistViewAsync(someArtistView);

            //then
            await Assert.ThrowsAsync<ArtistViewDependencyValidationException>(() =>
                addArtistViewTask.AsTask());

            this.userServiceMock.Verify(service =>
               service.GetCurrentlyLoggedInUser(),
                   Times.Once);

            this.dateTimeBrokerMock.Verify(service =>
                service.GetCurrentDateTime(),
                    Times.Once);

            this.artistServiceMock.Verify(service =>
               service.AddArtistAsync(It.IsAny<Artist>()),
                   Times.Never);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedDependencyValidationException))),
                        Times.Once);

            this.userServiceMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.artistServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionOnAddIfDependencyErrorOccuredAndLogItAsync(
            Xeption artistServiceDependencyException)
        {
            //given
            ArtistView someArtistView = CreateRandomArtistView();

            var expectedDependencyException =
                new ArtistViewDependencyException(artistServiceDependencyException);

            this.dateTimeBrokerMock.Setup(service =>
                service.GetCurrentDateTime())
                    .Throws(artistServiceDependencyException);

            //when
            ValueTask<ArtistView> addArtistViewTask =
                this.artistViewService.AddArtistViewAsync(someArtistView);

            //then
            await Assert.ThrowsAsync<ArtistViewDependencyException>(() =>
                addArtistViewTask.AsTask());

            this.userServiceMock.Verify(service =>
                service.GetCurrentlyLoggedInUser(),
                    Times.Once);

            this.dateTimeBrokerMock.Verify(service =>
                service.GetCurrentDateTime(),
                    Times.Once);

            this.artistServiceMock.Verify(service =>
               service.AddArtistAsync(It.IsAny<Artist>()),
                   Times.Never);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedDependencyException))),
                        Times.Once);

            this.userServiceMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.artistServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccuredAndLogItAsync()
        {
            //given
            ArtistView someArtistView = CreateRandomArtistView();
            var serviceException = new Exception();

            var failedArtistViewServiceException =
                new FailedArtistViewServiceException(serviceException);

            var artistViewServiceException =
                new ArtistViewServiceException(failedArtistViewServiceException);

            this.dateTimeBrokerMock.Setup(service =>
                service.GetCurrentDateTime())
                    .Throws(serviceException);

            //when
            ValueTask<ArtistView> addArtistViewTask =
                this.artistViewService.AddArtistViewAsync(someArtistView);

            //then
            await Assert.ThrowsAsync<ArtistViewServiceException>(() =>
                addArtistViewTask.AsTask());

            this.userServiceMock.Verify(service =>
                service.GetCurrentlyLoggedInUser(),
                    Times.Once);

            this.dateTimeBrokerMock.Verify(service =>
                service.GetCurrentDateTime(),
                    Times.Once);

            this.artistServiceMock.Verify(service =>
               service.AddArtistAsync(It.IsAny<Artist>()),
                   Times.Never);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    artistViewServiceException))),
                        Times.Once);

            this.userServiceMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.artistServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
