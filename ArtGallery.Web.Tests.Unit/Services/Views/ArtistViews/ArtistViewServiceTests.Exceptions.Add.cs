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

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.userServiceMock.VerifyNoOtherCalls();
            this.artistServiceMock.VerifyNoOtherCalls();
        }
    }
}
