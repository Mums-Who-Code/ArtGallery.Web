// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using ArtGallery.Web.Api.Models.Foundations.Artists;
using ArtGallery.Web.Api.Models.Views.Foundations.ArtistViews;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace ArtGallery.Web.Tests.Unit.Services.Views.ArtistViews
{
    public partial class ArtistViewServiceTests
    {
        [Fact]
        public async Task ShouldAddArtistViewAsync()
        {
            //given
            Guid currentLoggedInUserId = Guid.NewGuid();
            DateTimeOffset randomDateTime = GetRandomDateTime();

            dynamic randomArtistViewProperties =
                CreateRandomArtistViewProperties(
                    auditDates: randomDateTime,
                    auditIds: currentLoggedInUserId);

            var randomArtistView = new ArtistView
            {
                Id = randomArtistViewProperties.Id,
                FirstName = randomArtistViewProperties.FirstName,
                LastName = randomArtistViewProperties.LastName,
                Email = randomArtistViewProperties.Email,
                ContactNumber = randomArtistViewProperties.ContactNumber
            };

            var inputArtistView = randomArtistView;
            var expectedArtistView = inputArtistView.DeepClone();

            var randomArtist = new Artist
            {
                Id = randomArtistViewProperties.Id,
                FirstName = randomArtistViewProperties.FirstName,
                LastName = randomArtistViewProperties.LastName,
                Status = randomArtistViewProperties.Status,
                Email = randomArtistViewProperties.Email,
                ContactNumber = randomArtistViewProperties.ContactNumber,
                CreatedDate = randomArtistViewProperties.CreatedDate,
                UpdatedDate = randomArtistViewProperties.UpdatedDate,
                CreatedBy = randomArtistViewProperties.CreatedBy,
                UpdatedBy = randomArtistViewProperties.UpdatedBy
            };

            Artist expectedInputArtist = randomArtist;
            Artist persistedArtist = expectedInputArtist.DeepClone();

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Returns(randomDateTime);

            this.userServiceMock.Setup(service =>
                service.GetCurrentlyLoggedInUser())
                    .Returns(currentLoggedInUserId);

            this.artistServiceMock.Setup(service =>
                service.AddArtistAsync(It.Is(
                    SameArtistAs(expectedInputArtist))))
                        .ReturnsAsync(persistedArtist);
            //when
            ArtistView actualArtistView =
                await this.artistViewService
                    .AddArtistViewAsync(inputArtistView);
            //then
            actualArtistView.Should().BeEquivalentTo(expectedArtistView);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.userServiceMock.Verify(service =>
                service.GetCurrentlyLoggedInUser(),
                    Times.Once);

            this.artistServiceMock.Verify(service =>
                service.AddArtistAsync(It.Is(
                    SameArtistAs(expectedInputArtist))),
                        Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.userServiceMock.VerifyNoOtherCalls();
            this.artistServiceMock.VerifyNoOtherCalls();
        }
    }
}
