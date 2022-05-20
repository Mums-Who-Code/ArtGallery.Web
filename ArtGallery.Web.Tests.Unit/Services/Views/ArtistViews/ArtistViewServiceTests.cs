// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Linq.Expressions;
using ArtGallery.Web.Api.Brokers.DateTimes;
using ArtGallery.Web.Api.Brokers.Loggings;
using ArtGallery.Web.Api.Models.Foundations.Artists;
using ArtGallery.Web.Api.Models.Services.Foundations.Artists;
using ArtGallery.Web.Api.Models.Services.Foundations.ArtistViews;
using ArtGallery.Web.Api.Models.Services.Foundations.Users;
using ArtGallery.Web.Api.Models.Views.Foundations.ArtistViews;
using KellermanSoftware.CompareNetObjects;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;

namespace ArtGallery.Web.Tests.Unit.Services.Views.ArtistViews
{
    public partial class ArtistViewServiceTests
    {
        private readonly Mock<IArtistService> artistServiceMock;
        private readonly Mock<IUserService> userServiceMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IArtistViewService artistViewService;
        private readonly ICompareLogic compareLogic;

        public ArtistViewServiceTests()
        {
            this.artistServiceMock = new Mock<IArtistService>();
            this.userServiceMock = new Mock<IUserService>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();
            var compareConfig = new ComparisonConfig();
            compareConfig.IgnoreProperty<Artist>(artist => artist.Id);
            this.compareLogic = new CompareLogic(compareConfig);

            this.artistViewService = new ArtistViewService(
                artistService: this.artistServiceMock.Object,
                userService: this.userServiceMock.Object,
                dateTimeBroker: this.dateTimeBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static dynamic CreateRandomArtistViewProperties(
            DateTimeOffset auditDates,
            Guid auditIds)
        {
            return new
            {
                Id = Guid.NewGuid(),
                FirstName = GetRandomFirstName(),
                LastName = GetRandomLastName(),
                Status = ArtistStatus.Active,
                Email = GetRandomEmail(),
                ContactNumber = GetRandomContactNumber(),
                CreatedDate = auditDates,
                UpdatedDate = auditDates,
                CreatedBy = auditIds,
                UpdatedBy = auditIds
            };
        }

        private Expression<Func<Artist, bool>> SameArtistAs(Artist expectedArtist)
        {
            return actualArtist => this.compareLogic.Compare(actualArtist, expectedArtist).AreEqual;
        }

        private static string GetRandomFirstName() =>
            new RealNames(NameStyle.FirstName).GetValue();

        private static string GetRandomLastName() =>
            new RealNames(NameStyle.LastName).GetValue();

        private static DateTimeOffset GetRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException)
        {
            return actualException =>
                actualException.Message == expectedException.Message
                && actualException.InnerException.Message == expectedException.InnerException.Message
                && (actualException.InnerException as Xeption).DataEquals(expectedException.InnerException.Data);
        }

        private static string GetRandomContactNumber() =>
            new LongRange(min: 1000000000, max: 9999999999).GetValue().ToString();

        private static string GetRandomEmail() =>
            new EmailAddresses().GetValue().ToString();

        private static ArtistView CreateRandomArtistView() =>
            CreateArtistViewFiller(dateTime: GetRandomDateTime()).Create();
        private static Filler<ArtistView> CreateArtistViewFiller(DateTimeOffset dateTime)
        {
            var filler = new Filler<ArtistView>();
            Guid id = Guid.NewGuid();

            filler.Setup()
                .OnType<Guid>().Use(id)
                .OnType<DateTimeOffset>().Use(dateTime)
                .OnProperty(artistView => artistView.Status).Use(ArtistStatusView.Active)
                .OnProperty(artistView => artistView.ContactNumber).Use(GetRandomContactNumber())
                .OnProperty(artistView => artistView.Email).Use(GetRandomEmail());

            return filler;
        }
    }
}
