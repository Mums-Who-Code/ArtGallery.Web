// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using ArtGallery.Web.Api.Models.Foundations.Artists;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace ArtGallery.Web.Tests.Unit.Services.Foundations.Artists
{
    public partial class ArtistServiceTests
    {
        [Fact]
        public async Task ShouldAddAtristAsync()
        {
            //given
            Artist randomArtist = CreateRandomArtist();
            Artist inputArtist = randomArtist;
            Artist retrieveArtist = inputArtist;
            Artist expectedArtist = retrieveArtist.DeepClone();

            this.apiBrokerMock.Setup(broker =>
                broker.PostArtistAsync(inputArtist))
                    .ReturnsAsync(expectedArtist);

            //when
            Artist actualArtist =
                await this.artistService.AddArtistAsync(inputArtist);

            //then
            actualArtist.Should().BeEquivalentTo(expectedArtist);

            this.apiBrokerMock.Verify(broker =>
                broker.PostArtistAsync(inputArtist),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
