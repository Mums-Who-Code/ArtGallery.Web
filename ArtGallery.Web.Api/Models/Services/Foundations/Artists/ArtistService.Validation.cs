// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using ArtGallery.Web.Api.Models.Foundations.Artists;
using ArtGallery.Web.Api.Models.Foundations.Artists.Exceptions;

namespace ArtGallery.Web.Api.Models.Services.Foundations.Artists
{
    public partial class ArtistService
    {
        public void ValidateArtist(Artist artist)
        {
            ValidateIfArtistIsNotNull(artist);

            Validate(
                (Rule: IsInvalid(artist.Id), Parameter: nameof(Artist.Id)),
                (Rule: IsInvalid(text: artist.FirstName), Parameter: nameof(Artist.FirstName)),
                (Rule: IsInvalid(text: artist.LastName), Parameter: nameof(Artist.LastName)),
                (Rule: IsInvalid(text: artist.Email), Parameter: nameof(Artist.Email)),
                (Rule: IsInvalid(text: artist.ContactNumber), Parameter: nameof(Artist.ContactNumber)),
                (Rule: IsInvalid(status: artist.Status), Parameter: nameof(Artist.Status)),
                (Rule: IsInvalid(id: artist.CreatedBy), Parameter: nameof(Artist.CreatedBy)),
                (Rule: IsInvalid(date: artist.CreatedDate), Parameter: nameof(Artist.CreatedDate)));
        }

        private void ValidateIfArtistIsNotNull(Artist artist)
        {
            if (artist is null)
            {
                throw new NullArtistException();
            }
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required."
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
            Message = "Text is required."
        };

        private static dynamic IsInvalid(ArtistStatus status) => new
        {
            Condition = status != ArtistStatus.Active,
            Message = "Value is invalid."
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required."
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidArtistException = new InvalidArtistException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidArtistException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidArtistException.ThrowIfContainsErrors();
        }
    }
}
