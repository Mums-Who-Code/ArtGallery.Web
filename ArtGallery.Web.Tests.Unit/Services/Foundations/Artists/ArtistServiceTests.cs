// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Linq.Expressions;
using ArtGallery.Web.Api.Brokers.Apis;
using ArtGallery.Web.Api.Brokers.Loggings;
using ArtGallery.Web.Api.Models.Foundations.Artists;
using ArtGallery.Web.Api.Models.Services.Foundations.Artists;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;

namespace ArtGallery.Web.Tests.Unit.Services.Foundations.Artists
{
    public partial class ArtistServiceTests
    {
        private readonly Mock<IApiBroker> apiBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IArtistService artistService;

        public ArtistServiceTests()
        {
            this.apiBrokerMock = new Mock<IApiBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.artistService = new ArtistService(
                this.apiBrokerMock.Object,
                this.loggingBrokerMock.Object);
        }

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedExeption)
        {
            return actualException =>
                actualException.Message == expectedExeption.Message
                && actualException.InnerException.Message == expectedExeption.InnerException.Message
                && (actualException.InnerException as Xeption).DataEquals(expectedExeption.InnerException.Data);
        }

        private static string GetRandomEmail() =>
          new EmailAddresses().GetValue().ToString();

        public static Artist CreateRandomArtist() =>
            CreateArtistFiller().Create();

        private static Filler<Artist> CreateArtistFiller()
        {
            var filler = new Filler<Artist>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(DateTimeOffset.UtcNow)
                .OnProperty(artist => artist.Email).Use(GetRandomEmail());

            return filler;
        }
    }
}
