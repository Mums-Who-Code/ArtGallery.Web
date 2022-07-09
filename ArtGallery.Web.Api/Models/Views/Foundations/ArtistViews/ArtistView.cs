// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

namespace ArtGallery.Web.Api.Models.Views.Foundations.ArtistViews
{
    public class ArtistView
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public ArtistStatusView Status { get; set; }
    }
}
