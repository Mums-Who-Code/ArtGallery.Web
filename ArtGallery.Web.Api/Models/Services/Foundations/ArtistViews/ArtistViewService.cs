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
    public partial class ArtistViewService : IArtistViewService
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

        public ValueTask<ArtistView> AddArtistViewAsync(ArtistView artistView) =>
        TryCatch(async () =>
        {
            ValidateArtistViewOnAdd(artistView);
            Artist artist = MapToArtist(artistView);
            Artist addedArtist = await this.artistService.AddArtistAsync(artist);

            return MapToArtistView(addedArtist);
        });

        private Artist MapToArtist(ArtistView artistView)
        {
            Guid currentlyLoggedInUserId = this.userService.GetCurrentlyLoggedInUser();
            DateTimeOffset currentDateTime = dateTimeBroker.GetCurrentDateTime();

            return new Artist
            {
                Id = artistView.Id,
                FirstName = artistView.FirstName,
                LastName = artistView.LastName,
                Status = ArtistStatus.Active,
                CreatedDate = currentDateTime,
                UpdatedDate = currentDateTime,
                CreatedBy = currentlyLoggedInUserId,
                UpdatedBy = currentlyLoggedInUserId,
            };
        }

        private ArtistView MapToArtistView(Artist artist)
        {
            return new ArtistView
            {
                Id = artist.Id,
                FirstName = artist.FirstName,
                LastName = artist.LastName,
                Status = ArtistStatusView.Active,
            };
        }
    }
}
