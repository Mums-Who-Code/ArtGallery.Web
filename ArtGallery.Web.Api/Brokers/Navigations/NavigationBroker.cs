// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;

namespace ArtGallery.Web.Api.Brokers.Navigations
{
    public class NavigationBroker : INavigationBroker
    {
        private readonly NavigationManager navigationManager;

        public NavigationBroker(NavigationManager navigationManager) =>
            this.navigationManager = navigationManager;

        public void NavigateTo(string route) =>
            this.navigationManager.NavigateTo(route);
    }
}
