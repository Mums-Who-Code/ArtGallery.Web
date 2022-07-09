// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

namespace ArtGallery.Web.Api.Models.Services.Foundations.Users
{
    public interface IUserService
    {
        Guid GetCurrentlyLoggedInUser();
    }
}
