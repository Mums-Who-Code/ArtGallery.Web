// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using ArtGallery.Web.Api.Models.Views.Foundations.ArtistViews;
using ArtGallery.Web.Api.Models.Views.Foundations.ArtistViews.Exceptions;
using Moq;
using Xunit;

namespace ArtGallery.Web.Tests.Unit.Services.Views.ArtistViews
{
    public partial class ArtistViewServiceTests
    {
        [Theory]
        [MemberData(nameof(DependencyValidationExceptions))]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfValidationErrorOccuredAndLogItAsync(
           Exception artistValidationException)
        {
            //given
            ArtistView someArtistView = CreateRandomArtistView();

            var expectedDependencyValidationException =
                new ArtistViewDependencyValidationException(artistValidationException.InnerException);

            this.dateTimeBrokerMock.Setup(service =>
                service.GetCurrentDateTime())
                    .Throws(artistValidationException);

            //when
            ValueTask<ArtistView> addArtistViewTask =
                this.artistViewService.AddArtistViewAsync(someArtistView);

            //then
            await Assert.ThrowsAsync<ArtistViewDependencyValidationException>(() =>
                addArtistViewTask.AsTask());

            this.dateTimeBrokerMock.Verify(service =>
                service.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedDependencyValidationException))),
                        Times.Once);

            this.artistServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionOnAddIfDependencyErrorOccuredAndLogItAsync(
            Exception artistServiceDependencyException)
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

            this.dateTimeBrokerMock.Verify(service =>
                service.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedDependencyException))),
                        Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
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

            this.dateTimeBrokerMock.Verify(service =>
                service.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    artistViewServiceException))),
                        Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
