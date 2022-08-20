// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

namespace ArtGallery.Web.Api.Models.Services.Foundations.Users
{
    public class UserService : IUserService
    {
        public Guid GetCurrentlyLoggedInUser() => Guid.NewGuid();
    }
}
