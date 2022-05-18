// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using ArtGallery.Web.Api.Brokers.DateTimes;
using ArtGallery.Web.Api.Brokers.Loggings;
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

        public ValueTask<ArtistView> AddArtistViewAsync(ArtistView artistView)
        {
            throw new NotImplementedException();
        }
    }
}
