// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using ArtGallery.Web.Api.Brokers.DateTimes;
using ArtGallery.Web.Api.Brokers.Loggings;
using ArtGallery.Web.Api.Models.Foundations.Artists;
using ArtGallery.Web.Api.Models.Services.Foundations.Artists;
using ArtGallery.Web.Api.Models.Services.Foundations.Users;
using ArtGallery.Web.Api.Models.Views.Foundations.ArtistViews;

namespace ArtGallery.Web.Api.Models.Services.Foundations.ArtistViews
{
    public class ArtistViewService : IArtistViewService
    {
        private readonly IArtistService artistService;
        private readonly IUserService userService;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly ILoggingBroker loggingBroker;

        public ArtistViewService(
            IArtistService artistService,
            IUserService userService,
            IDateTimeBroker dateTimeBroker,
            ILoggingBroker loggingBroker)
        {
            this.artistService = artistService;
            this.userService = userService;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<ArtistView> AddArtistViewAsync(ArtistView artistView)
        {
            Artist artist = MapToArtist(artistView);
            await this.artistService.AddArtistAsync(artist);

            return artistView;
        }

        private Artist MapToArtist(ArtistView artistView)
        {
            Guid currentlyLoggedInUserId = this.userService.GetCurrentlyLoggedInUser();
            DateTimeOffset currentDateTime = dateTimeBroker.GetCurrentDateTime();

            return new Artist
            {
                Id = Guid.NewGuid(),
                FirstName = artistView.FirstName,
                LastName = artistView.LastName,
                Status = ArtistStatus.Active,
                CreatedDate = currentDateTime,
                UpdatedDate = currentDateTime,
                CreatedBy = currentlyLoggedInUserId,
                UpdatedBy = currentlyLoggedInUserId,
            };
        }
    }
}
