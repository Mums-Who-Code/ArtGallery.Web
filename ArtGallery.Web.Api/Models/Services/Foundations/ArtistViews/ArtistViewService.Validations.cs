// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using ArtGallery.Web.Api.Models.Views.Foundations.ArtistViews;
using ArtGallery.Web.Api.Models.Views.Foundations.ArtistViews.Exceptions;

namespace ArtGallery.Web.Api.Models.Services.Foundations.ArtistViews
{
    public partial class ArtistViewService
    {
        private void ValidateArtistViewOnAdd(ArtistView artistView)
        {
            ValidateInput(artistView);

            Validate(
               (Rule: IsInvalid(text: artistView.FirstName), Parameter: nameof(ArtistView.FirstName)),
               (Rule: IsInvalid(text: artistView.LastName), Parameter: nameof(ArtistView.LastName)),
               (Rule: IsInvalid(text: artistView.Email), Parameter: nameof(ArtistView.Email)),
               (Rule: IsInvalid(text: artistView.ContactNumber), Parameter: nameof(ArtistView.ContactNumber)),
               (Rule: IsInvalid(artistView.Status), Parameter: nameof(ArtistView.Status))
            );
        }

        private void ValidateInput(ArtistView artistView)
        {
            if (artistView == null)
            {
                throw new NullArtistViewException();
            }
        }

        private static dynamic IsInvalid(string text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
            Message = "Text is required."
        };

        private static dynamic IsInvalid(ArtistStatusView status) => new
        {
            Condition = status != ArtistStatusView.Active,
            Message = "Value is invalid."
        };

        private void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidArtistViewException = new InvalidArtistViewException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidArtistViewException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidArtistViewException.ThrowIfContainsErrors();
        }
    }
}
