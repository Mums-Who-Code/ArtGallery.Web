﻿// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using ArtGallery.Web.Api.Brokers.Apis;
using ArtGallery.Web.Api.Brokers.Loggings;
using ArtGallery.Web.Api.Models.Foundations.Artists;
using ArtGallery.Web.Api.Models.Services.Foundations.Artists;
using Moq;
using RESTFulSense.Exceptions;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

namespace ArtGallery.Web.Tests.Unit.Services.Foundations.Artists
{
    public partial class ArtistServiceTests
    {
        private readonly Mock<IApiBroker> apiBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IArtistService artistService;

        public ArtistServiceTests()
        {
            this.apiBrokerMock = new Mock<IApiBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.artistService = new ArtistService(
                this.apiBrokerMock.Object,
                this.loggingBrokerMock.Object);
        }

        public static TheoryData CriticalDependencyException()
        {
            string someMessage = GetRandomMesaage();
            var httpResponseMessage = new HttpResponseMessage();
            var httpRequestException = new HttpRequestException();

            var httpReponseUrlNotFoundException =
                new HttpResponseUrlNotFoundException(
                   httpResponseMessage,
                   someMessage);

            var httpResponseUnauthorizedException =
                new HttpResponseUnauthorizedException(
                    httpResponseMessage,
                    someMessage);

            return new TheoryData<Exception>
            {
                httpRequestException,
                httpReponseUrlNotFoundException,
                httpResponseUnauthorizedException
            };
        }

        public static TheoryData ApiDependencyExceptions()
        {
            var responseMessage = new HttpResponseMessage();
            string exceptionMessage = GetRandomMesaage();

            var httpResponseException =
                new HttpResponseException(
                    httpResponseMessage: responseMessage,
                    message: exceptionMessage);

            var httpResponseInternalServerErrorException =
               new HttpResponseInternalServerErrorException(
                   responseMessage: responseMessage,
                   message: exceptionMessage);

            return new TheoryData<Exception>
            {
                httpResponseException,
                httpResponseInternalServerErrorException
            };
        }

        public static TheoryData DependencyValidationExceptions()
        {
            var responseMessage = new HttpResponseMessage();
            string exceptionMessage = GetRandomMesaage();

            var httpResponseConflictException =
                new HttpResponseConflictException(
                    responseMessage: responseMessage,
                    message: exceptionMessage);

            var httpResponseFailedDependencyException =
                new HttpResponseFailedDependencyException(
                    responseMessage: responseMessage,
                    message: exceptionMessage);

            return new TheoryData<Exception>()
            {
                httpResponseConflictException,
                httpResponseFailedDependencyException
            };
        }

        private static string GetRandomMesaage() =>
            new MnemonicString(wordCount: GetRandomNumer()).GetValue();

        private static int GetRandomNumer() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedExeption)
        {
            return actualException =>
                actualException.Message == expectedExeption.Message
                && actualException.InnerException.Message == expectedExeption.InnerException.Message
                && (actualException.InnerException as Xeption).DataEquals(expectedExeption.InnerException.Data);
        }

        private static string GetRandomEmail() =>
            new EmailAddresses().GetValue().ToString();

        private static SqlException GetSqlException() =>
            (SqlException)FormatterServices.GetUninitializedObject(typeof(SqlException));

        private static DateTimeOffset GetRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        public static Artist CreateRandomArtist() =>
            CreateArtistFiller(dateTime: GetRandomDateTime()).Create();

        private static Dictionary<string, List<string>> CreateRandomDictionary() =>
            new Filler<Dictionary<string, List<string>>>().Create();

        private static Filler<Artist> CreateArtistFiller(DateTimeOffset dateTime)
        {
            var filler = new Filler<Artist>();
            Guid id = Guid.NewGuid();

            filler.Setup()
                .OnType<Guid>().Use(id)
                .OnType<DateTimeOffset>().Use(dateTime)
                .OnProperty(artist => artist.Status).Use(ArtistStatus.Active)
                .OnProperty(artist => artist.Email).Use(GetRandomEmail());

            return filler;
        }
    }
}
